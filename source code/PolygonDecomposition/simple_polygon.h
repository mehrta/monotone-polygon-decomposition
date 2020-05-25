#pragma once
#include "geometry.h"

namespace pd
{
	// A struct for representing a simple polygon
	struct SimplePolygon
	{
		int num_vertices;			// Number of vertices of the polygon
		int num_merges;				// Number of MERGE vertices
		int num_splits;				// Number of SPLIT vertices
		Vertex *vertices;			// An array of vertices of the polygon
		VertexTag *vertices_tags;   // An array of tags that describes vertices of polygon
		int* splits;				// Indexes of SPLIT vertices of the polygon
		int* merges;				// Indexes of MERGE vertices of the polygon

		// Set vertices of the polygon and pre-process them
		void set_vertices(Vertex* vertices, int num_vertices);
		SimplePolygon();
		~SimplePolygon();
	};
}