#include "stdafx.h"
#include "trapezoidal_decomposition.h"

using namespace pd;

int Qsn::id_counter_ = 0;

TrapezoidalDecomposition::TrapezoidalDecomposition(void)
{
	line_segments_ = nullptr;
	num_trapezoids_ = 0;
	num_x_nodes_ = 0;
	num_y_nodes_ = 0;
	traverse_counter_ = 0;
}

TrapezoidalDecomposition::~TrapezoidalDecomposition(void)
{
	FreeMemory();
}

void TrapezoidalDecomposition::set_polygon(SimplePolygon *sp)
{
	sp_ = sp;
}

SimplePolygon* TrapezoidalDecomposition::get_polygon()
{
	return sp_;
}

void TrapezoidalDecomposition::Initialize()
{
	// Deallocated the memory (if method "Decompose()" was called before)  
	FreeMemory();

	//
	// Create line segments of the polygon
	//

	line_segments_ = new LineSegment[2 + sp_->num_vertices*2]; // allocate (maximum) space for line segments of polygon and diagonals

	num_line_segments_ = sp_->num_vertices + 2; // num_line_segments_ will grow after decomposition

	// Put left and right (vertical) line segments of the bounding box in 'line_segments_'
	line_segments_[0].lower = Vertex(bounding_box_->left,  bounding_box_->bottom);
	line_segments_[0].upper = Vertex(bounding_box_->left,  bounding_box_->top);
	line_segments_[1].lower = Vertex(bounding_box_->right, bounding_box_->bottom + 1);
	line_segments_[1].upper = Vertex(bounding_box_->right, bounding_box_->top - 1);

	for (int i = 0; i < sp_->num_vertices; i++) {
		int next = (i+1) % sp_->num_vertices;

		if (sp_->vertices[i].y > sp_->vertices[next].y) {
			line_segments_[i+2].upper_index = i;
			line_segments_[i+2].lower_index = next;
			line_segments_[i+2].lower = sp_->vertices[next];
			line_segments_[i+2].upper = sp_->vertices[i];
		}
		else {
			line_segments_[i+2].upper_index = next;
			line_segments_[i+2].lower_index = i;
			line_segments_[i+2].lower = sp_->vertices[i];
			line_segments_[i+2].upper = sp_->vertices[next];
		}
	}

	// Initialize tree structure (create root node)
	Qsn *root_trap;
	QsnTag_Trapezoid *tag;

	CreateTrapezoid(bounding_box_->top, bounding_box_->bottom, &root_, &tag);

	tag->left_edge  = 0;
	tag->right_edge = 1;
	tag->top_left_trapezoid			=
		tag->top_right_trapezoid	=
		tag->bottom_left_trapezoid	=
		tag->bottom_right_trapezoid = nullptr;
}

Qsn* TrapezoidalDecomposition::GetNextTrapezoid(Qsn *current_trapezoid, int ls)
{
	QsnTag_Trapezoid *tag_current_trap, *tag_bottom_left_trap;
	Qsn *next_trap;
	int edge;

	// this trapezoid has 2 bottom adjecents
	tag_current_trap = GetTag_T(current_trapezoid);

	tag_bottom_left_trap = GetTag_T(tag_current_trap->bottom_left_trapezoid);
	edge = tag_bottom_left_trap->right_edge;

	// calculate cross product(s)
	float cp1_z;

	cp1_z = CrossPproduct_Z(
		line_segments_[ls].upper,
		line_segments_[edge].upper,
		line_segments_[ls].lower);

	if(cp1_z > 0) {
		next_trap = tag_current_trap->bottom_left_trapezoid;
	}
	else { // cp1_z < 0 (NOTE: cp2_z could not be equals to zero)
		next_trap = tag_current_trap->bottom_right_trapezoid;
	}

	return next_trap;
}

Qsn* TrapezoidalDecomposition::LocateUpperVertex(int line_segment)
{
	Qsn *current_node = root_;
	LineSegment &ls = line_segments_[line_segment];

	// Traverse the query structer tree

	while (current_node->type != QSNT_TRAPEZOID) {
		if (current_node->type == QSNT_Y) {
			// Y-Node
			QsnTag_Y *tag;
			tag = GetTag_Y(current_node);

			if (ls.upper.y > tag->value)
				current_node = tag->right_child;
			else
				current_node = tag->left_child;
		}
		else if (current_node->type == QSNT_X) {
			// X-Node
			QsnTag_X *tag = GetTag_X(current_node);
			LineSegment &associated_line = line_segments_[tag->value]; 

			float cp_z = CrossPproduct_Z(
				associated_line.lower,
				associated_line.upper,
				ls.upper);

			if (cp_z > 0) {
				// upper_vertex is located to the right of asscociated line segment(tag->value)
				current_node = tag->right_child;
			}
			else if (cp_z < 0) {
				// upper_vertex is located to the left of asscociated line segment(tag->value)
				current_node = tag->left_child; 
			}
			else 
			{
				float cp2_z = CrossPproduct_Z(
					associated_line.lower,
					associated_line.upper,
					ls.lower);

				if (cp2_z > 0)
					current_node = tag->right_child;
				else
					current_node = tag->left_child; 
			}
		}
	}

	return current_node;
}

Qsn* TrapezoidalDecomposition::LocateVertex(int vertex, VERTEX_TYPE vertex_type)
{
	Qsn *current_node = root_;
	Point& v = sp_->vertices[vertex];

	// Traverse the query structer tree

	while (current_node->type != QSNT_TRAPEZOID) {
		if (current_node->type == QSNT_Y) {
			// Y-Node
			QsnTag_Y *tag;
			tag = GetTag_Y(current_node);

			if (v.y > tag->value) {
				current_node = tag->right_child;
			}
			else if (v.y < tag->value) {
				current_node = tag->left_child;
			}
			else
			{
				if (vertex_type == VT_SPLIT)
					current_node = tag->right_child;
				else // vertex_type == VT_MERGE
					current_node = tag->left_child;
			}
		}
		else if (current_node->type == QSNT_X) {
			// X-Node
			QsnTag_X *tag = GetTag_X(current_node);
			LineSegment &associated_line = line_segments_[tag->value]; 

			float cp_z = CrossPproduct_Z(associated_line.lower,associated_line.upper,v);

			if (cp_z > 0)
				// v is located on the right of asscociated line segment(tag->value)
					current_node = tag->right_child;
			else 
				// v is located on the left of asscociated line segment(tag->value)
				current_node = tag->left_child; 
		}
	}

	return current_node;
}


Qsn* TrapezoidalDecomposition::Decompose()
{
	// Delete previous allocations - Initialize tree structure - Create line segments
	Initialize();

	// generate a random permutation
	int *permutation = new int[sp_->num_vertices];
	GenerateRandomPermutation(sp_->num_vertices, permutation);

	// Add line segments one by one (randomly)
	for (int i = 0; i < sp_->num_vertices; i++)
		AddLineSegment(2 + permutation[i]);

	//
	delete[] permutation;
	return root_;
}

void TrapezoidalDecomposition::GenerateRandomPermutation(int size, int *buffer)
{
	int i, rnd1, rnd2, tmp;
	srand(GetTickCount());

	for (i = 0; i < size; i++)
		buffer[i] = i;

	// Make array randomized 
	//for (i = 0; i < size; i++)
	//{
	//	rnd1 = rand() % size;
	//	rnd2 = rand() % size;

	//	tmp = buffer[rnd1];
	//	buffer[rnd1] = buffer[rnd2];
	//	buffer[rnd2] = tmp;
	//}
}

void TrapezoidalDecomposition::AddLineSegment(int line_segment)
{
	LineSegment &ls = line_segments_[line_segment];;
	//Vertex lower_vertex, upper_vertex;
	Qsn *upper_vertex_trap; // Trapezoid that upper vertex of the line segment is located in it
	bool is_upper_vertex_inside_trap;  // Is upper point located inside (first) trapezoid or on the boundary of trapezoid
	Qsn *current_trap; // current trap (in itterations)
	QsnTag_Trapezoid *tag_upper_vertex_trap;


	// Locate trapezoid that contains upper_point
	upper_vertex_trap = LocateUpperVertex(line_segment);
	tag_upper_vertex_trap = GetTag_T(upper_vertex_trap);

	// Is upper point located inside trapezoid?
	is_upper_vertex_inside_trap = (ls.upper.y < tag_upper_vertex_trap->y1);

	if (is_upper_vertex_inside_trap) {
		// Split upper_point_trap horizontally and set current_trap to (new) lower trapezoid 
		current_trap = DivideTrapezoidHorizontally(upper_vertex_trap, ls.upper.y);
		current_trap = GetTag_T(current_trap)->bottom_left_trapezoid;
	}
	else {
		current_trap = upper_vertex_trap;
	}

	// 
	// Traverse adjecent trapezoids (downward) and divide them vertically
	//
	Qsn * next_trap, *left_trap, *right_trap;
	QsnTag_Trapezoid *tag_current_trap;
	tag_current_trap = GetTag_T(current_trap);

	do {
		//
		// Is lower_point located in the current_trap
		//

		if (ls.lower.y < tag_current_trap->y1 && ls.lower.y > tag_current_trap->y2) {
			current_trap = DivideTrapezoidHorizontally(current_trap, ls.lower.y);
			tag_current_trap = GetTag_T(current_trap);
		}

		// Go to next trapezoid
		Qsn *bottom_left  =  tag_current_trap->bottom_left_trapezoid;
		Qsn *bottom_right =  tag_current_trap->bottom_right_trapezoid;

		if (bottom_left == bottom_right) {
			next_trap = bottom_left;
			DivideTrapezoidVertically(current_trap, line_segment, &left_trap, &right_trap);
		}
		else {
			next_trap = GetNextTrapezoid(current_trap, line_segment);

			DivideTrapezoidVertically(current_trap, line_segment, &left_trap, &right_trap);

			if (next_trap == bottom_right) {
				// Set bottom-left links of 'left_trap' 
				GetTag_T(left_trap)->bottom_left_trapezoid = bottom_left;
				GetTag_T(bottom_left)->top_left_trapezoid  = left_trap;
				GetTag_T(bottom_left)->top_right_trapezoid = left_trap;
			}
			else {
				// Set bottom-right links of 'right_trap' 
				GetTag_T(right_trap)->bottom_right_trapezoid = bottom_right;
				GetTag_T(bottom_right)->top_left_trapezoid  = right_trap;
				GetTag_T(bottom_right)->top_right_trapezoid = right_trap;
			}
		}

		//UpdateAdjacentVertexTag(left_trap);
		//UpdateAdjacentVertexTag(right_trap);

		// Update variables
		current_trap = next_trap;
		if (current_trap)
			tag_current_trap = GetTag_T(current_trap);

	} while ( current_trap && (tag_current_trap->y1 > ls.lower.y));
}

void TrapezoidalDecomposition::AddDiagonal(int v1, int v2)
{
	LineSegment ls;

	if (sp_->vertices[v1].y < sp_->vertices[v2].y) {
		ls.lower_index = v1;
		ls.upper_index = v2;
		ls.lower = sp_->vertices[v1];
		ls.upper = sp_->vertices[v2];
	}
	else {
		ls.lower_index = v2;
		ls.upper_index = v1;
		ls.lower = sp_->vertices[v2];
		ls.upper = sp_->vertices[v1];
	}

	line_segments_[num_line_segments_] = ls;
	AddLineSegment(num_line_segments_);
	num_line_segments_++;
}

Qsn* TrapezoidalDecomposition::DivideTrapezoidHorizontally(Qsn *trap, float y_value)
{
	Qsn *upper_trap, *lower_trap;
	QsnTag_Trapezoid *upper_trap_tag, *lower_trap_tag, *trap_tag;

	trap_tag = GetTag_T(trap);

	// Create two new trapezoids
	upper_trap = new Qsn();
	lower_trap = new Qsn();
	lower_trap_tag = new QsnTag_Trapezoid();
	upper_trap_tag = new QsnTag_Trapezoid();

	// Set lower_trap
	lower_trap->type = QSNT_TRAPEZOID;
	lower_trap->tag = lower_trap_tag;
	lower_trap_tag->top_left_trapezoid		= upper_trap;
	lower_trap_tag->top_right_trapezoid		= upper_trap;
	lower_trap_tag->bottom_left_trapezoid	= trap_tag->bottom_left_trapezoid;
	lower_trap_tag->bottom_right_trapezoid	= trap_tag->bottom_right_trapezoid;
	lower_trap_tag->left_edge				= trap_tag->left_edge;
	lower_trap_tag->right_edge				= trap_tag->right_edge;
	lower_trap_tag->y1						= y_value;
	lower_trap_tag->y2						= trap_tag->y2;

	// Set upper_trap
	upper_trap->type = QSNT_TRAPEZOID;
	upper_trap->tag = upper_trap_tag;
	upper_trap_tag->top_left_trapezoid		= trap_tag->top_left_trapezoid;
	upper_trap_tag->top_right_trapezoid		= trap_tag->top_right_trapezoid;
	upper_trap_tag->bottom_left_trapezoid	= lower_trap;
	upper_trap_tag->bottom_right_trapezoid	= lower_trap;
	upper_trap_tag->left_edge				= trap_tag->left_edge;
	upper_trap_tag->right_edge				= trap_tag->right_edge;
	upper_trap_tag->y1						= trap_tag->y1;
	upper_trap_tag->y2						= y_value;

	// Set bottom trapezoid(s) of 'lower_trap'
	if (trap_tag->bottom_left_trapezoid != nullptr) {
		QsnTag_Trapezoid *bottom_left_trap_tag, *bottom_right_trap_tag;

		bottom_left_trap_tag =	GetTag_T(trap_tag->bottom_left_trapezoid);
		bottom_right_trap_tag = GetTag_T(trap_tag->bottom_right_trapezoid);

		if (bottom_left_trap_tag->top_left_trapezoid->id == trap->id)
			bottom_left_trap_tag->top_left_trapezoid = lower_trap;

		if (bottom_left_trap_tag->top_right_trapezoid->id == trap->id)
			bottom_left_trap_tag->top_right_trapezoid = lower_trap;

		if (bottom_right_trap_tag->top_left_trapezoid->id == trap->id)
			bottom_right_trap_tag->top_left_trapezoid = lower_trap;

		if (bottom_right_trap_tag->top_right_trapezoid->id == trap->id)
			bottom_right_trap_tag->top_right_trapezoid = lower_trap;
	}

	// Set top trapezoid of 'upper_trapezoid'
	if (trap_tag->top_left_trapezoid != nullptr) {
		QsnTag_Trapezoid *top_left_trap_tag, *top_right_trap_tag;


		if (trap_tag->top_left_trapezoid->type == QSNT_TRAPEZOID) {
			top_left_trap_tag =	GetTag_T(trap_tag->top_left_trapezoid);

			if (top_left_trap_tag->bottom_left_trapezoid->id == trap->id)
				top_left_trap_tag->bottom_left_trapezoid = upper_trap;

			if (top_left_trap_tag->bottom_right_trapezoid->id == trap->id )
				top_left_trap_tag->bottom_right_trapezoid  = upper_trap;
		}

		if (trap_tag->top_right_trapezoid->type == QSNT_TRAPEZOID) {
			top_right_trap_tag = GetTag_T(trap_tag->top_right_trapezoid);

			if (top_right_trap_tag->bottom_left_trapezoid->id == trap->id)
				top_right_trap_tag->bottom_left_trapezoid  = upper_trap;

			if (top_right_trap_tag->bottom_right_trapezoid->id == trap->id)
				top_right_trap_tag->bottom_right_trapezoid = upper_trap;
		}
	}


	// Change the type of node 'trap' to Y-node
	QsnTag_Y *tag_y;

	tag_y = new QsnTag_Y();
	tag_y->value = y_value;
	tag_y->right_child = upper_trap;
	tag_y->left_child = lower_trap;

	trap->type = QSNT_Y;
	trap->tag  = tag_y;

	// Delete trap_tag from memory
	delete trap_tag;

	// Return upper trapezoid
	return upper_trap;
}

void TrapezoidalDecomposition::DivideTrapezoidVertically(Qsn *trap, int line_segment, Qsn **left_trap, Qsn **right_trap)
{
	QsnTag_Trapezoid *tag_trap = GetTag_T(trap);
	LineSegment &ls = line_segments_[line_segment];
	QsnTag_Trapezoid *tag_left_trap, *tag_right_trap;
	int left_edge_index, right_edge_index;
	float &y1 = tag_trap->y1;
	float &y2 = tag_trap->y2;

	left_edge_index  = tag_trap->left_edge;
	right_edge_index = tag_trap->right_edge;
	LineSegment &left_edge  = line_segments_[left_edge_index]; 
	LineSegment &right_edge = line_segments_[right_edge_index]; 

	//
	// Set upper pointers (links)
	//
	if(tag_trap->y1 == ls.upper.y) {
		//
		// upper point of "ls" is located on the top boundry of the trapezoid
		//
		CreateTrapezoid(y1, y2, left_trap, &tag_left_trap);
		CreateTrapezoid(y1, y2, right_trap, &tag_right_trap);
		tag_left_trap->left_edge	= left_edge_index;
		tag_left_trap->right_edge	= line_segment;
		tag_right_trap->left_edge	= line_segment;
		tag_right_trap->right_edge	= right_edge_index;

		if ((left_edge.upper.y == right_edge.upper.y) && (ls.upper.y == left_edge.upper.y)) {
			// 'left_edge' and 'right_edge' and 'ls' have common upper endpoints

			tag_left_trap->top_left_trapezoid	= nullptr;
			tag_left_trap->top_right_trapezoid	= nullptr;
			tag_right_trap->top_left_trapezoid	= nullptr;
			tag_right_trap->top_right_trapezoid = nullptr;
		} 
		else if (left_edge.upper.y == ls.upper.y) {
			// 'left_edge' and 'ls' have common upper endpoint

			tag_left_trap->top_left_trapezoid	= nullptr;
			tag_left_trap->top_right_trapezoid	= nullptr;
			tag_right_trap->top_left_trapezoid	= tag_trap->top_left_trapezoid;
			tag_right_trap->top_right_trapezoid = tag_trap->top_left_trapezoid;

			QsnTag_Trapezoid* tag_top_trap = GetTag_T(tag_trap->top_left_trapezoid);
			if (tag_top_trap->bottom_right_trapezoid == trap)
				tag_top_trap->bottom_right_trapezoid = *right_trap;
			if (tag_top_trap->bottom_left_trapezoid == trap)
				tag_top_trap->bottom_left_trapezoid = *right_trap;

		}
		else if (right_edge.upper.y == ls.upper.y)
		{
			// 'right_edge' and 'ls' have common upper endpoint
			tag_left_trap->top_left_trapezoid	= tag_trap->top_left_trapezoid;
			tag_left_trap->top_right_trapezoid	= tag_trap->top_left_trapezoid;
			tag_right_trap->top_left_trapezoid	= nullptr;
			tag_right_trap->top_right_trapezoid = nullptr;

			QsnTag_Trapezoid* tag_top_trap = GetTag_T(tag_trap->top_left_trapezoid);
			if (tag_top_trap->bottom_right_trapezoid == trap)
				tag_top_trap->bottom_right_trapezoid = *left_trap;
			if (tag_top_trap->bottom_left_trapezoid == trap)
				tag_top_trap->bottom_left_trapezoid = *left_trap;
		}
		else {
			// 'ls' is located bettwen 'left_edge' and 'right_edge'
			tag_left_trap->top_left_trapezoid	= tag_trap->top_left_trapezoid;
			tag_left_trap->top_right_trapezoid	= tag_trap->top_left_trapezoid;
			tag_right_trap->top_left_trapezoid	= tag_trap->top_right_trapezoid;
			tag_right_trap->top_right_trapezoid = tag_trap->top_right_trapezoid;

			if (tag_trap->top_left_trapezoid == tag_trap->top_right_trapezoid)
			{
				GetTag_T(tag_trap->top_left_trapezoid)->bottom_left_trapezoid = *left_trap;
				GetTag_T(tag_trap->top_left_trapezoid)->bottom_right_trapezoid = *right_trap;
			}
			else {
				GetTag_T(tag_trap->top_left_trapezoid)->bottom_left_trapezoid = *left_trap;
				GetTag_T(tag_trap->top_left_trapezoid)->bottom_right_trapezoid = *left_trap;
				GetTag_T(tag_trap->top_right_trapezoid)->bottom_left_trapezoid = *right_trap;
				GetTag_T(tag_trap->top_right_trapezoid)->bottom_right_trapezoid = *right_trap;
			}
		}

	}
	else {
		//
		// 'ls' comes from upper trapezoid of 'trap'
		//
		Qsn *previous_left_trap, *previous_right_trap;
		Qsn *previous_trap_x;  // it is a X node now, not a trapezoid
		QsnTag_Trapezoid *tag_previous_left_trap, *tag_previous_right_trap;
		QsnTag_X *tag_previous_x_node;

		if (left_edge.upper.y == tag_trap->y1) {
			// 'trap' HAD 1 upper trapezoid (it divided vertically before)

			tag_previous_x_node		= GetTag_X(tag_trap->top_left_trapezoid);
			previous_left_trap		= tag_previous_x_node->left_child;
			previous_right_trap		= tag_previous_x_node->right_child;
			tag_previous_left_trap	= GetTag_T(previous_left_trap);
			tag_previous_right_trap = GetTag_T(previous_right_trap);

			// Create and set left trapezoid
			CreateTrapezoid(y1, y2, left_trap, &tag_left_trap);
			tag_left_trap->left_edge = left_edge_index;
			tag_left_trap->right_edge = line_segment;
			tag_left_trap->top_left_trapezoid  = previous_left_trap;
			tag_left_trap->top_right_trapezoid = previous_left_trap;

			//tag_previous_left_trap->bottom_right_trapezoid = *left_trap;
			if ( line_segments_[tag_previous_left_trap->left_edge].lower_index == left_edge.upper_index) {
				tag_previous_left_trap->bottom_left_trapezoid = *left_trap;
				tag_previous_left_trap->bottom_right_trapezoid = *left_trap;
			}
			else {
				tag_previous_left_trap->bottom_right_trapezoid = *left_trap;
			}

			// Merge 'right_trap' with 'previous_right_trap'
			*right_trap = previous_right_trap;
			tag_previous_right_trap->y2 = y2;
		}
		else if (right_edge.upper.y == tag_trap->y1) {
			// 'trap' HAD 1 upper trapezoid (it divided vertically before)

			tag_previous_x_node		= GetTag_X(tag_trap->top_left_trapezoid);
			previous_left_trap		= tag_previous_x_node->left_child;
			previous_right_trap		= tag_previous_x_node->right_child;
			tag_previous_left_trap	= GetTag_T(previous_left_trap);
			tag_previous_right_trap = GetTag_T(previous_right_trap);

			// Create and set right trapezoid
			CreateTrapezoid(y1, y2, right_trap, &tag_right_trap);
			tag_right_trap->left_edge	= line_segment;
			tag_right_trap->right_edge	= right_edge_index;
			tag_right_trap->top_left_trapezoid  = previous_right_trap;
			tag_right_trap->top_right_trapezoid = previous_right_trap;

			if ( line_segments_[tag_previous_right_trap->right_edge].lower_index == right_edge.upper_index) {
				tag_previous_right_trap->bottom_left_trapezoid = *right_trap;
				tag_previous_right_trap->bottom_right_trapezoid = *right_trap;
			}
			else {
				tag_previous_right_trap->bottom_left_trapezoid = *right_trap;
			}

			// Merge 'left_trap' with 'previous_left_trap'
			*left_trap = previous_left_trap;
			tag_previous_left_trap->y2 = y2;
		}
		else {
			// 'trap' has 2 upper trapezoid (see documentation, state 3)
			// one of them is divided vertically (it is a x-node now)
			Qsn *previous_left_node, *previous_right_node;

			previous_left_node = tag_trap->top_left_trapezoid;
			previous_right_node = tag_trap->top_right_trapezoid;

			if (previous_left_node->type == QSNT_X) {
				// previous_left_node is divided vertically
				Qsn *previous_left_left, *previous_left_right;
				QsnTag_Trapezoid *tag_previous_left_left, *tag_previous_left_right ;

				tag_previous_x_node = GetTag_X(previous_left_node);
				previous_left_left = tag_previous_x_node->left_child;
				previous_left_right = tag_previous_x_node->right_child;
				tag_previous_left_left = GetTag_T(previous_left_left);
				tag_previous_left_right = GetTag_T(previous_left_right);

				// Create right trapezoid
				CreateTrapezoid(y1, y2, right_trap, &tag_right_trap);
				tag_right_trap->left_edge = line_segment;
				tag_right_trap->right_edge = right_edge_index;
				tag_right_trap->top_left_trapezoid = previous_left_right;
				tag_right_trap->top_right_trapezoid = previous_right_node;

				tag_previous_left_right->bottom_left_trapezoid		= 
					tag_previous_left_right->bottom_right_trapezoid = *right_trap;

				tag_previous_right_trap = GetTag_T(tag_trap->top_right_trapezoid);
				tag_previous_right_trap->bottom_left_trapezoid		=
					tag_previous_right_trap->bottom_right_trapezoid = *right_trap;

				// Merge left trapezoid
				*left_trap = previous_left_left;
				tag_previous_left_left->y2 = tag_trap->y2;
			}
			else {
				// previous_right_node is divided vertically
				Qsn *previous_right_left, *previous_right_right;
				QsnTag_Trapezoid *tag_previous_right_left, *tag_previous_right_right ;

				tag_previous_x_node = GetTag_X(previous_right_node);
				previous_right_left = tag_previous_x_node->left_child;
				previous_right_right = tag_previous_x_node->right_child;
				tag_previous_right_left = GetTag_T(previous_right_left);
				tag_previous_right_right = GetTag_T(previous_right_right);

				// Create left trapezoid
				CreateTrapezoid(y1, y2, left_trap, &tag_left_trap);
				tag_left_trap->left_edge = left_edge_index;
				tag_left_trap->right_edge = line_segment;
				tag_left_trap->top_left_trapezoid = previous_left_node;
				tag_left_trap->top_right_trapezoid = previous_right_left;

				tag_previous_right_left->bottom_left_trapezoid		= 
					tag_previous_right_left->bottom_right_trapezoid = *left_trap;

				tag_previous_left_trap = GetTag_T(tag_trap->top_left_trapezoid);
				tag_previous_left_trap->bottom_left_trapezoid		=
					tag_previous_left_trap->bottom_right_trapezoid = *left_trap;

				// Merge right trapezoid
				*right_trap = previous_right_right;
				tag_previous_right_right->y2 = tag_trap->y2;
			}

		}
	}

	//
	// Set lower pointers
	// (if this trapezoid is the LAST trapezoid that we divide vertically)
	//
	tag_left_trap = GetTag_T(*left_trap);
	tag_right_trap = GetTag_T(*right_trap);

	if (ls.lower.y == tag_trap->y2) {
		if ((left_edge.lower.y == right_edge.lower.y) && (ls.lower.y == left_edge.lower.y))
		{
			tag_left_trap->bottom_left_trapezoid	= nullptr;
			tag_left_trap->bottom_right_trapezoid	= nullptr;
			tag_right_trap->bottom_left_trapezoid	= nullptr;
			tag_right_trap->bottom_right_trapezoid	= nullptr;
		}
		else if(left_edge.lower.y == ls.lower.y) {
			tag_left_trap->bottom_left_trapezoid	= nullptr;
			tag_left_trap->bottom_right_trapezoid	= nullptr;
			tag_right_trap->bottom_left_trapezoid	= tag_trap->bottom_left_trapezoid;
			tag_right_trap->bottom_right_trapezoid	= tag_trap->bottom_left_trapezoid;

			QsnTag_Trapezoid* tag;
			tag = GetTag_T(tag_trap->bottom_left_trapezoid);
			if (tag->top_left_trapezoid == tag->top_right_trapezoid)
				tag->top_right_trapezoid = tag->top_left_trapezoid = *right_trap;
			else
				tag->top_right_trapezoid = *right_trap;
		}
		else if(right_edge.lower.y == ls.lower.y) {
			tag_left_trap->bottom_left_trapezoid	= tag_trap->bottom_left_trapezoid;
			tag_left_trap->bottom_right_trapezoid	= tag_trap->bottom_left_trapezoid;
			tag_right_trap->bottom_left_trapezoid	= nullptr;
			tag_right_trap->bottom_right_trapezoid	= nullptr;

			QsnTag_Trapezoid* tag;
			tag = GetTag_T(tag_trap->bottom_left_trapezoid);
			if (tag->top_left_trapezoid == tag->top_right_trapezoid)
				tag->top_left_trapezoid = tag->top_right_trapezoid = *left_trap;
			else
				tag->top_left_trapezoid = *left_trap;
		}
		else {
			tag_left_trap->bottom_left_trapezoid	= tag_trap->bottom_left_trapezoid;
			tag_left_trap->bottom_right_trapezoid	= tag_trap->bottom_left_trapezoid;
			tag_right_trap->bottom_left_trapezoid	= tag_trap->bottom_right_trapezoid;
			tag_right_trap->bottom_right_trapezoid	= tag_trap->bottom_right_trapezoid;

			if (tag_trap->bottom_left_trapezoid == tag_trap->bottom_right_trapezoid) {
				GetTag_T(tag_trap->bottom_left_trapezoid)->top_left_trapezoid  = *left_trap;
				GetTag_T(tag_trap->bottom_left_trapezoid)->top_right_trapezoid = *right_trap;
			}
			else {
				GetTag_T(tag_trap->bottom_left_trapezoid)->top_left_trapezoid  = *left_trap;
				GetTag_T(tag_trap->bottom_left_trapezoid)->top_right_trapezoid = *left_trap;

				GetTag_T(tag_trap->bottom_right_trapezoid)->top_left_trapezoid  = *right_trap;
				GetTag_T(tag_trap->bottom_right_trapezoid)->top_right_trapezoid = *right_trap;
			}
		}
	}

	//
	// Change type of 'trap'
	//
	QsnTag_X *tag_x		= new QsnTag_X();
	tag_x->left_child   = *left_trap;
	tag_x->right_child  = *right_trap;
	tag_x->value = line_segment;

	trap->type = QSNT_X;
	delete trap->tag;
	trap->tag = tag_x;
}

void TrapezoidalDecomposition::FreeMemory()
{
	// Clean up (previous execution)
	if (line_segments_){
		delete[] line_segments_;

		// Deallocate query structure memory
		std::list<Qsn*>::iterator it;

		// Delete X nodes
		for (it = x_nodes_.begin(); it != x_nodes_.end(); it++) {
			delete (*it)->tag;
			delete (*it);
		}

		// Delete Y nodes
		for (it = y_nodes_.begin(); it != y_nodes_.end(); it++) {
			delete (*it)->tag;
			delete (*it);
		}

		// Delete trapezoids
		for (it = trapezoids_.begin(); it != trapezoids_.end(); it++) {
			delete (*it)->tag;
			delete (*it);
		}

		x_nodes_.clear();
		y_nodes_.clear();
		trapezoids_.clear();
		num_y_nodes_	= 0;
		num_x_nodes_    = 0;
		num_trapezoids_ = 0;
		traverse_counter_ = 0;
		root_ = nullptr;
		line_segments_ = nullptr;
	}
}

void TrapezoidalDecomposition::TraverseQueryStructureTree(Qsn *node)
{
	if (node->type == QSNT_X) {
		if (node->visit_count != traverse_counter_) {
			x_nodes_.push_back(node);
			num_x_nodes_++;
			node->visit_count = traverse_counter_;

			QsnTag_X *tag = GetTag_X(node);
			TraverseQueryStructureTree(tag->left_child);
			TraverseQueryStructureTree(tag->right_child);
		}
	}
	else if (node->type == QSNT_Y) {
		if (node->visit_count != traverse_counter_) {
			y_nodes_.push_back(node);
			num_y_nodes_++;
			node->visit_count = traverse_counter_;

			QsnTag_Y *tag= GetTag_Y(node);
			TraverseQueryStructureTree(tag->left_child);
			TraverseQueryStructureTree(tag->right_child);
		}
	}
	else {
		if (node->visit_count != traverse_counter_) {
			trapezoids_.push_back(node);
			num_trapezoids_++;
			node->visit_count = traverse_counter_;
		}
	}

}

void TrapezoidalDecomposition::TraverseQueryStructureTree()
{
	x_nodes_.clear();
	y_nodes_.clear();
	trapezoids_.clear();

	num_x_nodes_ = 0;
	num_y_nodes_ = 0;
	num_trapezoids_ = 0;

	traverse_counter_++;
	TraverseQueryStructureTree(root_);
}

std::list<Qsn*> *TrapezoidalDecomposition::get_trapezoids()
{
	return &trapezoids_;
}

std::list<Qsn*> *TrapezoidalDecomposition::get_x_nodes()
{
	return &x_nodes_;
}

std::list<Qsn*> *TrapezoidalDecomposition::get_y_nodes()
{
	return &y_nodes_;
}

int TrapezoidalDecomposition::get_num_x_nodes()
{
	return num_x_nodes_;
}

int TrapezoidalDecomposition::get_num_y_nodes()
{
	return num_y_nodes_;
}

int TrapezoidalDecomposition::get_num_trapezoids()
{
	return num_trapezoids_;
}

int TrapezoidalDecomposition::get_num_line_segments()
{
	return num_line_segments_;
}

LineSegment* TrapezoidalDecomposition::get_line_segments()
{
	return line_segments_;
}

void TrapezoidalDecomposition::set_bounding_box(pd::Rectangle *bounding_box)
{
	bounding_box_ = bounding_box;
}

void TrapezoidalDecomposition::UpdateAdjacentVertexTag (Qsn* trap)
{
	QsnTag_Trapezoid* tag = GetTag_T(trap);
	int i;

	if ( tag->top_left_trapezoid && tag->top_right_trapezoid &&
		(tag->top_left_trapezoid != tag->top_right_trapezoid)) {
			i = line_segments_[GetTag_T(tag->top_left_trapezoid)->right_edge].lower_index;
			if (sp_->vertices_tags[i].type == VT_MERGE)
				sp_->vertices_tags[i].adjecent_trapezoid = trap;
	}

	if (tag->bottom_left_trapezoid && tag->bottom_right_trapezoid && 
		(tag->bottom_left_trapezoid != tag->bottom_right_trapezoid)) {
			i = line_segments_[GetTag_T(tag->bottom_left_trapezoid)->right_edge].upper_index;
			if (sp_->vertices_tags[i].type == VT_SPLIT)
				sp_->vertices_tags[i].adjecent_trapezoid = trap;
	}
}

Point TrapezoidalDecomposition::GetTrapezoidTopLeft(QsnTag_Trapezoid* tag)
{
	return LineIntersection(line_segments_ [tag->left_edge].lower, line_segments_[tag->left_edge].upper, tag->y1);
}

Point TrapezoidalDecomposition::GetTrapezoidTopRight(QsnTag_Trapezoid* tag)
{
	return LineIntersection(line_segments_[tag->right_edge].lower, line_segments_[tag->right_edge].upper, tag->y1);
}

Point TrapezoidalDecomposition::GetTrapezoidBottomLeft(QsnTag_Trapezoid* tag)
{
	return LineIntersection(line_segments_[tag->left_edge].lower, line_segments_[tag->left_edge].upper, tag->y2);
}

Point TrapezoidalDecomposition::GetTrapezoidBottomRight(QsnTag_Trapezoid* tag)
{
	return LineIntersection(line_segments_[tag->right_edge].lower, line_segments_[tag->right_edge].upper, tag->y2);
}

bool TrapezoidalDecomposition::IsInternalTrapezoid(QsnTag_Trapezoid* tag)
{
	bool is_internal;

	if ((tag->left_edge == 0) || (tag->right_edge == 1)){
		is_internal = false;
	}
	else {
		LineSegment& left_edge = line_segments_[tag->left_edge];
		if ((left_edge.upper_index == left_edge.lower_index + 1) ||
			((left_edge.lower_index == sp_->num_vertices -1) && left_edge.upper_index == 0))
			is_internal = true;
		else
			is_internal = false;
	}

	return is_internal;
}

void TrapezoidalDecomposition::print(Qsn *node)
{
	if (node->type == QSNT_TRAPEZOID) {
		QsnTag_Trapezoid *tag;
		tag = GetTag_T(node);

		std::cout<<"TRAP ID:   --- "		<< node->id<< " ---" << std::endl ;
		std::cout<<"left : "			<< tag->left_edge<< std::endl;
		std::cout<<"right: "		<< tag->right_edge<< std::endl;
		std::cout<<"top-left: "		<< ((tag->top_left_trapezoid == nullptr) ? -1  : tag->top_left_trapezoid->id)<< std::endl;
		std::cout<<"top-right: "	<< ((tag->top_right_trapezoid == nullptr) ? -1 : tag->top_right_trapezoid->id)<< std::endl;
		std::cout<<"bottom-left: "  << ((tag->bottom_left_trapezoid == nullptr) ? -1 : tag->bottom_left_trapezoid->id)<< std::endl;
		std::cout<<"bottom-right: " << ((tag->bottom_right_trapezoid == nullptr) ? -1 : tag->bottom_right_trapezoid->id)<< std::endl;
		std::cout<<"-------"		<< std::endl<< std::endl;
	} 
	else if (node->type == QSNT_X) {
		QsnTag_X *tag;
		tag = GetTag_X(node);

		std::cout<<"X-Ndoe ID: --- "	<< node->id<< " ---" << std::endl ;
		std::cout<<"left : "			<< tag->left_child->id << std::endl;
		std::cout<<"right: "		<< tag->right_child->id << std::endl;
		std::cout<<"-------"		<< std::endl<< std::endl;

		print(tag->left_child);
		print(tag->right_child);

	} else {
		QsnTag_Y *tag;
		tag = GetTag_Y(node);

		std::cout<<"Y-Node ID: --- "	<< node->id<< " ---" << std::endl ;
		std::cout<<"top: "		<< tag->right_child->id << std::endl;
		std::cout<<"bottom: "			<< tag->left_child->id<< std::endl;
		std::cout<<"-------"		<< std::endl<< std::endl;

		print(tag->left_child);
		print(tag->right_child);
	}
}

void TrapezoidalDecomposition::print()
{
	system("cls");
	print(root_);
}

