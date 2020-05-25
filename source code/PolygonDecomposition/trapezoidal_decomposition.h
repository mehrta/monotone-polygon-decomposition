#pragma once
#include "simple_polygon.h"

namespace pd
{
	enum QueryStructureNodeType { QSNT_X, QSNT_Y, QSNT_TRAPEZOID };

	struct Trapezoid
	{
		int id;
		pd::Point top_left, top_right, bottom_left, bottom_right;  // vertices of the trapezoid
		int top_left_trap, top_right_trap, bottom_left_trap, bottom_right_trap;  // ID of adjecent trapezoids
		int left, right;
	};

	struct XNode
	{
		int id;
		int left;
		int right;
	};

	struct YNode
	{
		int id;
		int top;
		int bottom;
	};

	// forward declartion
	struct QsnTag;
	//

	// Query Structure Node (QSN)
	struct Qsn
	{
		QueryStructureNodeType type;	// Type of this node
		QsnTag  *tag;	// A tag that discribes this node
		int id;	// ID of this node (not used by algorithm)
		int visit_count; // how many times this node visited

		Qsn()
		{
			id = (++id_counter_);
			visit_count = 0;
		}

		static void ResetIdCounter()
		{
			id_counter_ = 0;
		}

	private:
		static int id_counter_;
	};

	struct QsnTag // Query Structure Node Tag
	{ };

	struct QsnTag_X : QsnTag
	{
		int value;			// Index of line segment associated to this node
		Qsn *left_child;	// Query point is located in the left of the line segment
		Qsn *right_child;	// Query point is located in the right of the line segment
	};

	struct QsnTag_Y : QsnTag
	{
		float value; // Y-coordinate of a point that is associated to this node
		Qsn *left_child;  // Y-Cordinate of the Query point is smaller than this->value
		Qsn *right_child; // Y-Cordinate of the Query point is greater than this->value
	};

	struct QsnTag_Trapezoid : QsnTag
	{
		float y1; // Y-Cordinate of the higher horizontal edge of the trapezoid
		float y2; // Y-Cordinate of the lower horizontal edge of the trapezoid
		int left_edge;		// Index of left line segment of the trapezoid
		int right_edge;		// Index of right line segment of the trapezoid
		Qsn *bottom_left_trapezoid;	// Bottom-Left adjacent trapezoid
		Qsn *bottom_right_trapezoid; // Bottom-Right adjacent trapezoid
		Qsn *top_left_trapezoid;		// Top-Left adjacent trapezoid
		Qsn *top_right_trapezoid;	// Top-Right adjacent trapezoid

		QsnTag_Trapezoid()
		{
			bottom_left_trapezoid		=
				bottom_right_trapezoid	=
				top_left_trapezoid		=
				top_right_trapezoid		= nullptr;
		}
	};

	class TrapezoidalDecomposition
	{
	public:
		TrapezoidalDecomposition(void);
		~TrapezoidalDecomposition(void);

		// Decompose simple polygon to trapezoidal components (using Seidel's algorithm)
		// and returns the root of the query structure
		Qsn* Decompose();

		// Sets polygon that should be decompose
		void set_polygon(SimplePolygon *sp);

		// 
		void set_bounding_box(Rectangle *bounding_box);

		//
		void TraverseQueryStructureTree();

		void AddDiagonal(int v1, int v2);

		// Release all allocated memory
		void FreeMemory();

		// Locates a split or merge vertex
		Qsn* LocateVertex(int vertex, VERTEX_TYPE vertex_type);

		Point GetTrapezoidTopLeft(QsnTag_Trapezoid* tag);
		Point GetTrapezoidTopRight(QsnTag_Trapezoid* tag);
		Point GetTrapezoidBottomLeft(QsnTag_Trapezoid* tag);
		Point GetTrapezoidBottomRight(QsnTag_Trapezoid* tag);
		bool  IsInternalTrapezoid(QsnTag_Trapezoid* tag);
		std::list<Qsn*> *get_trapezoids();
		std::list<Qsn*> *get_x_nodes();
		std::list<Qsn*> *get_y_nodes();
		LineSegment*	 get_line_segments();
		SimplePolygon* get_polygon();
		int get_num_x_nodes();
		int get_num_y_nodes();
		int get_num_trapezoids();
		int get_num_line_segments();
		void print(Qsn *node);
		void print();

	private:
		SimplePolygon *sp_;
		Rectangle* bounding_box_;

		LineSegment *line_segments_;	// Line segments (edges of polygon + diagonals)
		int num_line_segments_;			// Number of line segments
		Qsn *root_; // Root of the query structure (produced by Seidel's algorithm)
		std::list<Qsn*> trapezoids_;
		std::list<Qsn*> x_nodes_;
		std::list<Qsn*> y_nodes_;
		int num_x_nodes_;
		int num_y_nodes_;
		int num_trapezoids_;
		int traverse_counter_; // how many times Query Structure Nodes is traversed

		// Should be called before decomposition
		void Initialize();

		// Generates a random permutation
		static void GenerateRandomPermutation (int size, int *buffer);

		// Locates trapezoid that contains upper point of line_segment 
		Qsn* LocateUpperVertex(int line_segment);


		// Adds a line segment to the query structure
		void AddLineSegment(int line_segment);

		// Get next bottom (adjecent) trapezoid that should be divided verticaly
		Qsn* GetNextTrapezoid(Qsn *current_trapezoid, int line_segment);

		// Divides a trapezoid horizontally and returns upper trapezoid
		Qsn* DivideTrapezoidHorizontally(Qsn *trapezoid, float y_value);

		// Divides a trapezoid vertically
		void DivideTrapezoidVertically(Qsn *trap, int line_segment, Qsn **left_trap, Qsn **right_trap);

		//
		void UpdateAdjacentVertexTag (Qsn* trap);
		void TraverseQueryStructureTree(Qsn *node); 
	};

	inline QsnTag_Trapezoid* GetTag_T(Qsn *trapezoid)
	{
		return static_cast<QsnTag_Trapezoid*>(trapezoid->tag);
	}

	inline QsnTag_X* GetTag_X(Qsn *x_node)
	{
		return static_cast<QsnTag_X*>(x_node->tag);
	}

	inline QsnTag_Y* GetTag_Y(Qsn *y_node)
	{
		return static_cast<QsnTag_Y*>(y_node->tag);
	}

	inline void CreateTrapezoid(float y1, float y2, Qsn **new_trap, QsnTag_Trapezoid **tag_new_trap)
	{
		Qsn *trap = new Qsn();
		QsnTag_Trapezoid *tag = new QsnTag_Trapezoid();

		tag->y1 = y1;
		tag->y2 = y2;
		trap->type = QSNT_TRAPEZOID;
		trap->tag = tag;

		(*new_trap) = trap;
		(*tag_new_trap) = tag;
	}
}