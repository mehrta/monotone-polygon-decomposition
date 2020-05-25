#include "stdafx.h"
#include "Interface.h"

using namespace pd;
using namespace pd_interface;

DllData dll_data;

void pd_interface::SetBoundingBox(float top, float bottom, float left, float right)
{
	dll_data.bounding_box.top		= top;
	dll_data.bounding_box.bottom	= bottom;
	dll_data.bounding_box.left		= left;
	dll_data.bounding_box.right		= right;
	dll_data.md.set_bounding_box(&dll_data.bounding_box);
}

ExecutionResult_GreedyOrSeidel pd_interface::Decompose_GreedyOrSeidel(int algorithm, Vertex *vertices, int num_vertices, int max_depth_traverse)
{
	ExecutionResult_GreedyOrSeidel er;
	SimplePolygon sp;

	sp.set_vertices(vertices, num_vertices);
	er = dll_data.md.Decompose(algorithm, &sp, max_depth_traverse);

	return er;
}

void pd_interface::FreeMemory_Greedy()
{
	dll_data.md.FreeMemory();
}
