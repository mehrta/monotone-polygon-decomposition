// CGAL.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "CGAL.h"

typedef CGAL::Simple_cartesian<int>         K;
typedef K::Point_2                          Point_2;
typedef std::list<Point_2>                  Container;
typedef CGAL::Polygon_2<K, Container>       Polygon_2;
typedef std::list<Polygon_2>                Polygon_list;

// This typedefs are used for monotone decomposition (Lee-Preparata's algorithm)
typedef CGAL::Exact_predicates_inexact_constructions_kernel	  K_B;
typedef CGAL::Partition_traits_2<K_B>                         Traits_B;
typedef Traits_B::Point_2                                     Point_2_B;
typedef Traits_B::Polygon_2                                   Polygon_2_B;
typedef std::list<Polygon_2_B>                                Polygon_list_B;

SimplePolygon GenerateRandomPolygon(int size, int x_min, int x_max, int y_min, int y_max, unsigned int random_seed)
{
	Polygon_2 polygon;
	std::set<int> points_y;
	std::pair<std::set<int>::iterator,bool> res;
	std::list<Point_2> points_list;
	int x, y, width, height;

	srand(random_seed);

	width = x_max - x_min;
	height = y_max - y_min;

	// Generate vertices
	for (int i = 0; i < size; i++)
	{
		x = x_min + (rand() % (width+1));

		do {
			y = y_min + (rand() % (height+1));
			res = points_y.insert(y);
		} while (res.second == false);

		points_list.push_back(Point_2(x, y));
	}

	// Generate polygon
	CGAL::random_polygon_2(points_list.size(), std::back_inserter(polygon), points_list.begin());

	// Return
	SimplePolygon pi;
	Vertex* vertices;
	pi.size = polygon.size();
	vertices = new Vertex[pi.size];

	if (polygon.orientation() == CGAL::COUNTERCLOCKWISE) {
		polygon.reverse_orientation();
	}

	int i = 0;
	for (Polygon_2::Vertex_const_iterator it = polygon.vertices_begin(); it != polygon.vertices_end(); it++, i++)
	{
		Point_2 p = (*it);
		vertices[i].x = (float) (p.x());
		vertices[i].y = (float) (p.y());
	}

	pi.vertices = vertices;
	return pi;
}

void FreePolygon(Vertex* polygon)
{
	delete[] polygon;
}



ExecutionResult_LeePreparata Decompose_LeePreparata(Vertex *vertices, int num_vertices)
{
	ExecutionResult_LeePreparata er;

	// Count split and merge vertices
	CountSplitsAndMerges(vertices, num_vertices, er.num_splits, er.num_merges);

	// Monotone Decomposition (Lee and preparata's alggorithm)
	Polygon_2_B polygon;
	Polygon_list_B y_monotone_components;
	std::list<Polygon_2_B>::const_iterator poly_it;
	int i,j;

	for (i = num_vertices-1; i >= 0; i--)
		polygon.push_back(Point_2_B((int)vertices[i].x, (int)vertices[i].y));

	CGAL::y_monotone_partition_2(polygon.vertices_begin(),
		polygon.vertices_end(),
		std::back_inserter(y_monotone_components));

	// Convert and return results
	Polygon_2_B::Vertex_const_iterator vertex_it;
	er.num_components = y_monotone_components.size();
	er.components = new SimplePolygon[er.num_components];

	for (i = 0, poly_it = y_monotone_components.begin(); poly_it != y_monotone_components.end(); poly_it++, i++)
	{
		Polygon_2_B p= (*poly_it);

		er.components[i].size = p.size();
		er.components[i].vertices = new Vertex[er.components[i].size];

		for (j=0, vertex_it = p.vertices_begin(); vertex_it != p.vertices_end(); vertex_it++, j++){
			Point_2_B p = (*vertex_it);
			er.components[i].vertices[j].x = (float) (p.x());
			er.components[i].vertices[j].y = (float) (p.y());
		}
	}

	return er;
}

void FreeMemory_LeePreparata(ExecutionResult_LeePreparata er)
{
	for (int i = 0; i < er.num_components; i++) {
		delete[] er.components[i].vertices;
	}
	delete er.components;
}


//
// Helper functions
//

void CountSplitsAndMerges(Vertex *vertices, int size, int& num_splits, int& num_merges)
{
	float cp_z;
	int previous = size - 1, current, next = 1;
	num_splits = num_merges = 0;

	for (int current = 0; current < size; current++) {
		if (vertices[current].IsUpper(vertices[previous], vertices[next])) {
			cp_z = CrossPproduct_Z(vertices[previous], vertices[current], vertices[next]);
			if (cp_z < 0)
				num_splits++;
		}
		else if (vertices[current].IsLower(vertices[previous], vertices[next])) {
			cp_z = CrossPproduct_Z(vertices[previous], vertices[current], vertices[next]);
			if (cp_z < 0)
				num_merges++;
		}

		previous = (previous + 1) % size;
		next	 = (next + 1) % size;
	}
}

float CrossPproduct_Z(const Vertex &p0, const Vertex &p1, const Vertex &p2)
{
	return (p0.x - p1.x) * (p2.y - p1.y) - (p2.x - p1.x ) * (p0.y - p1.y);
}



