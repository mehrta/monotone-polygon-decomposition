#pragma once


struct Vertex
{
	float x;
	float y;

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

};

struct SimplePolygon
{
	int size;
	struct Vertex* vertices;
};

struct ExecutionResult_LeePreparata
{
	int num_components; // Number of y-monotone components
	SimplePolygon* components; // y-monotone components (simple polygons)
	int num_splits;
	int num_merges;
};

// Global helper functions
float CrossPproduct_Z(const Vertex &p0, const Vertex &p1, const Vertex &p2);
void CountSplitsAndMerges(Vertex *vertices, int num_vertices, int& num_splits, int& num_merges);

// Export Functions
#define DllExport __declspec(dllexport)

extern "C"
{
	DllExport SimplePolygon GenerateRandomPolygon(int size, int x_min, int x_max, int y_min, int y_max, unsigned int random_seed);
	DllExport void FreePolygon(Vertex* polygon);
	DllExport ExecutionResult_LeePreparata Decompose_LeePreparata(Vertex *vertices, int num_vertices);
	DllExport void FreeMemory_LeePreparata(ExecutionResult_LeePreparata er);

}

