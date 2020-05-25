#include "stdafx.h"
#include "MonotoneDecomposition.h"

using namespace pd;

MonotoneDecomposition::MonotoneDecomposition()
{
	er_.x_nodes	= nullptr;
	er_.y_nodes    = nullptr;
	er_.trapezoids = nullptr;
	er_.diagonals  = nullptr;
	er_.num_diagonals = 0;
}

MonotoneDecomposition::~MonotoneDecomposition()
{
}

ExecutionResult_GreedyOrSeidel MonotoneDecomposition::Decompose(int algorithm, SimplePolygon *sp, int max_depth_traverse)
{
	// Decompose polygon trapezoidally
	sp_ = sp;
	k_ = max_depth_traverse;
	td_.set_polygon(sp);
	td_.Decompose();
	td_.TraverseQueryStructureTree();

	// Monotone Decomposition 

	if (algorithm == 0) {
		//DecomposePreProcess();
		GreedyMonotoneDecompose();
	}
	else if (algorithm == 1)
	{
		SeidelMonotoneDecompose();
	}

	DecomposePostProcess();

	// Return results
	return er_;
}

void MonotoneDecomposition::GreedyMonotoneDecompose()
{
	if (k_ <= 0)
		return;

	// Handle merge vertices
	for (int i = 0; i < sp_->num_merges; i++)
		HandleMergeVertex(sp_->merges[i]);

	// Handle split vertices
	for (int i = 0; i < sp_->num_splits; i++) {
		int split_index = sp_->splits[i];
		if (sp_->vertices_tags[split_index].dont_process == false)
			HandleSplitVertex(split_index);
	}

}

void MonotoneDecomposition::HandleMergeVertex(int vertex)
{
	bool not_found = true;
	float l, r; // left and right endpoints of visible part of current trapezoid
	float trap_bottom_left_x, trap_bottom_right_x;
	Vertex l_guide, r_guide, v;
	Qsn *current_trap, *first_trap;
	QsnTag_Trapezoid* current_trap_tag, *first_trap_tag;
	LineSegment* ls;
	int target_vertex;

	v = sp_->vertices[vertex];
	ls = td_.get_line_segments();
	first_trap = current_trap = td_.LocateVertex(vertex, VT_MERGE);  // sp_->vertices_tags[vertex].adjecent_trapezoid;
	first_trap_tag = current_trap_tag = GetTag_T(current_trap);


	l_guide = td_.GetTrapezoidBottomLeft(current_trap_tag);
	r_guide = td_.GetTrapezoidBottomRight(current_trap_tag);

	for (int i = 0; (not_found) && (i < k_) && (current_trap); i++)
	{
		current_trap_tag = GetTag_T(current_trap);

		l = LineIntersection(v, l_guide, current_trap_tag->y2).x;
		r = LineIntersection(v, r_guide, current_trap_tag->y2).x;

		trap_bottom_left_x = td_.GetTrapezoidBottomLeft(current_trap_tag).x;
		trap_bottom_right_x = td_.GetTrapezoidBottomRight(current_trap_tag).x;

		if ( (l >= trap_bottom_right_x) || (r <= trap_bottom_left_x)) {
			// Bottom edge (LANG=FA : ghaeede paaine zozanaghe) of This trapezoid is not visible from 'vertex'
			break; // Exit loop
		}

		if (l < trap_bottom_left_x) {
			l_guide = td_.GetTrapezoidBottomLeft(current_trap_tag);
			l = l_guide.x;
		}

		if (r > trap_bottom_right_x) {
			r_guide = td_.GetTrapezoidBottomRight(current_trap_tag);
			r = r_guide.x;
		}

		// Check for split vertex and go downward
		if (current_trap_tag->bottom_left_trapezoid == current_trap_tag->bottom_right_trapezoid) {
			current_trap = current_trap_tag->bottom_left_trapezoid;
		}
		else {
			// Split vertex found
			Vertex split = ls[GetTag_T(current_trap_tag->bottom_left_trapezoid)->right_edge].upper;
			int split_index = ls[GetTag_T(current_trap_tag->bottom_left_trapezoid)->right_edge].upper_index;

			if (l < split.x && split.x < r) {
				// A split vertex found!
				not_found = false;
				target_vertex = split_index;
				sp_->vertices_tags[split_index].dont_process = true; // Found split vertex dont need processing any more
			}
			else if (r < split.x) {
				current_trap = current_trap_tag->bottom_left_trapezoid;
			}
			else {
				current_trap = current_trap_tag->bottom_right_trapezoid;
			}
		}
	}

	if (not_found) {
		if (ls[first_trap_tag->left_edge].lower.y == first_trap_tag->y2) {
			target_vertex = ls[first_trap_tag->left_edge].lower_index;
		}
		else {
			target_vertex = ls[first_trap_tag->right_edge].lower_index;
		}

	}

	// Add diagonal
	td_.AddDiagonal(vertex, target_vertex);
	er_.num_diagonals++;
}

void MonotoneDecomposition::HandleSplitVertex(int vertex)
{
	Qsn* trap;
	QsnTag_Trapezoid* trap_tag;
	LineSegment* ls;
	int target_vertex;

	ls = td_.get_line_segments();
	trap = td_.LocateVertex(vertex, VT_SPLIT);  //sp_->vertices_tags[vertex].adjecent_trapezoid;
	trap_tag = GetTag_T(trap);

	if (ls[trap_tag->left_edge].upper.y == trap_tag->y1) {
		target_vertex = ls[trap_tag->left_edge].upper_index;
	}
	else {
		target_vertex = ls[trap_tag->right_edge].upper_index;
	}

	// Add diagonal
	td_.AddDiagonal(vertex, target_vertex);
	er_.num_diagonals++;
}

void MonotoneDecomposition::SeidelMonotoneDecompose()
{
	// Find trapezoids that are inside polygon and add diagonals
	std::list<pd::Qsn*>::iterator it;
	std::list<pd::Qsn*> *trap_nodes;
	QsnTag_Trapezoid *tag;
	LineSegment *line_segments;
	SimplePolygon *sp;
	int top_vertex_index;	// Index of the point that defines top edge of current trapezoid
	int bottom_vertex_index;	// Index of the point that defines bottom edge of current trapezoid

	sp = td_.get_polygon();
	line_segments = td_.get_line_segments();
	trap_nodes = td_.get_trapezoids();

	for (it = trap_nodes->begin(); it != trap_nodes->end(); it++) {
		tag = GetTag_T(*it);
		if (td_.IsInternalTrapezoid(tag)) {

			LineSegment& left_edge = line_segments[tag->left_edge];
			LineSegment& right_edge = line_segments[tag->right_edge];

			// find top_vertex_index
			if (tag->y1 == left_edge.upper.y)
				top_vertex_index = left_edge.upper_index;
			else if (tag->y1 == right_edge.upper.y)
				top_vertex_index = right_edge.upper_index;
			else 
				top_vertex_index =  line_segments[GetTag_T(tag->top_left_trapezoid)->right_edge].lower_index;

			// find bottom_vertex_index
			if (tag->y2 == left_edge.lower.y)
				bottom_vertex_index = left_edge.lower_index;
			else if (tag->y2 == right_edge.lower.y)
				bottom_vertex_index = right_edge.lower_index;
			else 
				bottom_vertex_index =  line_segments[GetTag_T(tag->bottom_left_trapezoid)->right_edge].upper_index;

			// Add diagonal ?
			int delta = abs(top_vertex_index - bottom_vertex_index);
			if ((delta > 1) && (delta != sp_->num_vertices -1)) {
				td_.AddDiagonal(top_vertex_index, bottom_vertex_index);
				er_.num_diagonals++;
			}
		}
	}
}

void MonotoneDecomposition::DecomposePreProcess()
{
	//
	// find adjecent trapezoids of split and merge vertices
	//
	std::list<pd::Qsn*>::iterator it;
	std::list<pd::Qsn*> *trap_nodes;
	QsnTag_Trapezoid *trap_tag;
	LineSegment *line_segments;
	SimplePolygon *sp;

	sp = td_.get_polygon();
	line_segments = td_.get_line_segments();
	trap_nodes = td_.get_trapezoids();

	for (it = trap_nodes->begin(); it != trap_nodes->end(); it++) {
		trap_tag = GetTag_T(*it);

		// Check trapezoid for adjacent merge vertex
		if (trap_tag->top_left_trapezoid != trap_tag->top_right_trapezoid) {
			int trap_upper_vertex = line_segments[GetTag_T(trap_tag->top_left_trapezoid)->right_edge].lower_index;
			if (sp->vertices_tags[trap_upper_vertex].type == VT_MERGE)
				sp->vertices_tags[trap_upper_vertex].adjecent_trapezoid = (*it);
		}

		// Check trapezoid for adjacent split vertex
		if (trap_tag->bottom_left_trapezoid != trap_tag->bottom_right_trapezoid) {
			int trap_lower_vertex = line_segments[GetTag_T(trap_tag->bottom_left_trapezoid)->right_edge].upper_index;
			if (sp->vertices_tags[trap_lower_vertex].type == VT_SPLIT)
				sp->vertices_tags[trap_lower_vertex].adjecent_trapezoid = (*it);
		}
	}
}

void MonotoneDecomposition::DecomposePostProcess()
{
	//
	// Step 1: Set some fields of 'er'
	//
	std::list<pd::Qsn*>::iterator it;
	std::list<pd::Qsn*> *x_nodes, *y_nodes, *trap_nodes;
	int i;

	td_.TraverseQueryStructureTree();

	er_.diagonals = td_.get_line_segments() + 2 + sp_->num_vertices;

	x_nodes = td_.get_x_nodes();
	y_nodes = td_.get_y_nodes();
	trap_nodes = td_.get_trapezoids();

	er_.vertices_tags = sp_->vertices_tags;

	er_.num_mergs = sp_->num_merges;
	er_.num_splits = sp_->num_splits;

	er_.num_x_nodes = td_.get_num_x_nodes();
	er_.num_y_nodes = td_.get_num_y_nodes();
	er_.num_trapezoids = td_.get_num_trapezoids();

	er_.x_nodes = new XNode[er_.num_x_nodes];
	er_.y_nodes = new YNode[er_.num_y_nodes];
	er_.trapezoids = new Trapezoid[er_.num_trapezoids];

	for (i = 0, it = x_nodes->begin(); it != x_nodes->end(); it++, i++) {
		er_.x_nodes[i].id  = (*it)->id;
		er_.x_nodes[i].left  = GetTag_X((*it))->left_child->id;
		er_.x_nodes[i].right = GetTag_X((*it))->right_child->id;
	}

	for (i = 0, it = y_nodes->begin(); it != y_nodes->end(); it++, i++) {
		er_.y_nodes[i].id  = (*it)->id;
		er_.y_nodes[i].bottom = GetTag_Y((*it))->left_child->id;
		er_.y_nodes[i].top    = GetTag_Y((*it))->right_child->id;
	}

	QsnTag_Trapezoid *tag;
	LineSegment *ls = td_.get_line_segments();

	for (i = 0, it = trap_nodes->begin(); it != trap_nodes->end(); it++, i++) {
		tag = GetTag_T(*it);
		er_.trapezoids[i].id  = (*it)->id;
		er_.trapezoids[i].left  = tag->left_edge;
		er_.trapezoids[i].right = tag->right_edge;
		er_.trapezoids[i].top_left	  = td_.GetTrapezoidTopLeft(tag);
		er_.trapezoids[i].bottom_left  = td_.GetTrapezoidBottomLeft(tag);
		er_.trapezoids[i].top_right	  = td_.GetTrapezoidTopRight(tag);
		er_.trapezoids[i].bottom_right = td_.GetTrapezoidBottomRight(tag);
		er_.trapezoids[i].top_left_trap     = (tag->top_left_trapezoid    ? tag->top_left_trapezoid->id : -1);
		er_.trapezoids[i].top_right_trap    = (tag->top_right_trapezoid   ? tag->top_right_trapezoid->id : -1);
		er_.trapezoids[i].bottom_left_trap  = (tag->bottom_left_trapezoid ? tag->bottom_left_trapezoid->id : -1);
		er_.trapezoids[i].bottom_right_trap = (tag->bottom_right_trapezoid? tag->bottom_right_trapezoid->id : -1);
	}
}

void MonotoneDecomposition::set_bounding_box(pd::Rectangle *bounding_box)
{
	td_.set_bounding_box(bounding_box);
}

void MonotoneDecomposition::FreeMemory()
{
	if (er_.trapezoids != nullptr) {
		td_.FreeMemory();
		Qsn::ResetIdCounter();
		delete[] er_.x_nodes;
		delete[] er_.y_nodes;
		delete[] er_.trapezoids;
	}

	er_.num_diagonals = 0;
	er_.x_nodes	   = nullptr;
	er_.y_nodes    = nullptr;
	er_.trapezoids = nullptr;
	er_.diagonals  = nullptr;
}

