//
// Exported functions (from dll) are declared in this file
// 
#pragma once
#include "MonotoneDecomposition.h"
#define DllExport __declspec(dllexport)
using namespace pd;

namespace pd_interface
{
	// Global variables (in dll)
	struct DllData {
		struct pd::Rectangle bounding_box;
		MonotoneDecomposition md;
	};

	// Dll exported functions
	extern "C" {
		DllExport void SetBoundingBox(float top, float bottom, float left, float right);
		DllExport ExecutionResult_GreedyOrSeidel Decompose_GreedyOrSeidel(int algorithm, Vertex *vertices, int num_vertices, int max_depth_traverse);
		DllExport void FreeMemory_Greedy();
	}
}