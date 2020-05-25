#include "stdafx.h"
#include "simple_polygon.h"

using namespace pd;

pd::SimplePolygon::SimplePolygon(void)
{
	splits = nullptr;
	merges = nullptr;
}

pd::SimplePolygon::~SimplePolygon(void)
{
	if (splits) {
		delete[] splits;
		delete[] merges;
		delete[] vertices_tags;
	}
}

void pd::SimplePolygon::set_vertices(Vertex* v, int size)
{
	this->num_vertices = size;
	this->vertices = v;

	// Allocate memory to two arrays of merge and split vertices
	if (splits) {
		delete[] splits;
		delete[] merges;
		delete[] vertices_tags;
	}
	vertices_tags	= new VertexTag[size];
	splits			= new int[size]; // allocate maximum possibly neaded elements
	merges			= new int[size]; // allocate maximum possibly neaded elements
	num_splits		= num_merges = 0;

	// Analyse vertex types (REGULAR, MERGE, SPLIT, START, END)
	float cp_z;
	int previous = size - 1, current, next = 1;

	for (int current = 0; current < size; current++) {
		if (vertices[current].IsUpper(vertices[previous], vertices[next])) {
			cp_z = CrossPproduct_Z(vertices[previous] , vertices[current], vertices[next]);
			if (cp_z < 0) {
				vertices_tags[current].type = VT_SPLIT;
				splits[num_splits++] = current;
			}
			else {
				vertices_tags[current].type = VT_START;
			}
		}
		else if (vertices[current].IsLower(vertices[previous], vertices[next])) {
			cp_z = CrossPproduct_Z(vertices[previous] , vertices[current], vertices[next]);
			if (cp_z < 0) {
				vertices_tags[current].type = VT_MERGE;
				merges[num_merges++] = current;
			}
			else {
				vertices_tags[current].type = VT_END;
			}
		}
		else {
			vertices_tags[current].type = VT_REGULAR;
		}

		previous = (previous + 1) % num_vertices;
		next =	   (next + 1) % num_vertices;
	}
}
