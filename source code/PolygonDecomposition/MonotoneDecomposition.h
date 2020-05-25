#pragma once
#include "trapezoidal_decomposition.h"

namespace pd
{
	// Execution result for Tajedini's greedy algorithm or Seidel's algorithm
	struct ExecutionResult_GreedyOrSeidel
	{
		int y_monotone_components; 
		int num_mergs;
		int num_splits;
		int num_diagonals;
		int num_trapezoids;
		int num_x_nodes;
		int num_y_nodes;
		LineSegment *diagonals;
		Trapezoid *trapezoids;
		XNode *x_nodes;
		YNode *y_nodes;
		VertexTag* vertices_tags;
	};

	// Greedy Monotone Decomposition
	class MonotoneDecomposition
	{
	public:
		MonotoneDecomposition();

		ExecutionResult_GreedyOrSeidel Decompose(int algorithm, SimplePolygon *sp, int max_depth_traverse);
		void FreeMemory();
		void set_bounding_box(Rectangle *bounding_box);
		~MonotoneDecomposition(void);

	private:
		void DecomposePreProcess();		// Preprocessing for greedy algorithm
		void GreedyMonotoneDecompose(); // Executes Tajedini's greedy algorithm
		void SeidelMonotoneDecompose(); // Executes Seidel's algorithm
		void DecomposePostProcess();

		void HandleMergeVertex(int vertex);
		void HandleSplitVertex(int vertex);

		SimplePolygon* sp_;
		TrapezoidalDecomposition td_;
		ExecutionResult_GreedyOrSeidel er_;
		int k_; // Maximum depth traverse
	};

}