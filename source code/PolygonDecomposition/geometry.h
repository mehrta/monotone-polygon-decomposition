#pragma once

// namespace PolygonDecomposition
namespace pd
{
	// Defines indeces of edges of bounding box that contains all geomtric objects (simple polygons, points, ...)
	enum VERTEX_TYPE  {VT_REGULAR, VT_START, VT_END, VT_SPLIT, VT_MERGE};

	typedef struct Vertex
	{
		float x, y;

		Vertex(){}

		Vertex(const float &x, const float &y)
		{
			this->x = x;
			this->y = y;
		}

		// Returns true if 'this' point is located above the points p1 and p2, otherwise returns false
		inline bool IsUpper(const Vertex &p1, const Vertex &p2)
		{
			return (this->y > p1.y) && (this->y > p2.y);
		}

		// Returns true if 'this' point is located below the points p1 and p2, otherwise returns false
		inline bool IsLower(const Vertex &p1, const Vertex &p2)
		{
			return (this->y < p1.y) && (this->y < p2.y);
		}
	} Point;


	// A structure that describes a vertex
	struct VertexTag
	{
		VERTEX_TYPE type;
		// for split and merge vertices-if this field is true, the vertex is already connected to
		// another vertex(split or merge) and dont need processing
		bool dont_process; 
		struct Qsn* adjecent_trapezoid; // For split and merge vertices

		VertexTag()
		{
			dont_process = false;
			adjecent_trapezoid = nullptr;
		}
	};

	struct LineSegment
	{
		int upper_index; // index of upper vertex
		int lower_index; // index of lower vertex
		Vertex upper;	 // upper vertex of the line segment
		Vertex lower;	 // lower vertex of the line segment
	};

	struct Diagonal
	{
		int v1, v2;
	};

	struct Rectangle
	{
		float top, bottom, left, right;

		Rectangle() {}
		Rectangle(float top, float bottom, float left, float right)
		{
			this->top	 = top;
			this->bottom = bottom;
			this->left	 = left;
			this->right  = right;
		}
	};

	//  --- Public Functions ---

	// Calculates cross product of vectors a=[p1,p0] and b=[p1,p2] and returns Z cordinate of the result vector
	// If counterclockwise angle bettwen a and b is smaller than 180 degres, returns a positive number
	// If counterclockwise angle bettwen a and b is greater than 180 degres, returns a negetive number
	// If counterclockwise angle bettwen a and b is eaqual to 180 degres, returns zero
	inline float CrossPproduct_Z(const Vertex &p0, const Vertex &p1, const Vertex &p2)
	{
		return (p0.x - p1.x) * (p2.y - p1.y) - (p2.x - p1.x ) * (p0.y - p1.y);
	}

	// Calculates intersection point of horizontal line Y=y and line segments (a,b)
	// refrence: http://en.wikipedia.org/wiki/Line-line_intersection
	inline Vertex LineIntersection(Vertex &a, Vertex &b, float y)
	{
		Vertex p;
		float z;
		const float c_x = 2, d_x = 1;  // c.x = 1; d.x = 2;
		float c_y = y, d_y = y;  // c.y = d.y = y;

		z = (a.x - b.x) * (c_y - d_y) - (a.y - b.y) * (c_x - d_x);
		p.x = ((a.x * b.y - a.y * b.x) * (c_x - d_x) - (a.x - b.x) * (c_x * d_y - c_y * d_x)) / z;
		p.y = ((a.x * b.y - a.y * b.x) * (c_y - d_y) - (a.y - b.y) * (c_x * d_y - c_y * d_x)) / z;

		return p;
	}
}
