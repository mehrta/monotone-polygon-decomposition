// PolygonDecomposition.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "interface.h"

using namespace pd;
using namespace pd_interface;

//#define MAKE_DLL

#ifndef MAKE_DLL


int wmain(int argc, wchar_t argv[])
{
	Point p[6];

	SetBoundingBox(100, -100, -100, 100);

	p[0].x = -2;
	p[0].y = 2;

	p[1].x = 0;
	p[1].y = -1;

	p[2].x = 1;
	p[2].y = 1;

	p[3].x = 0;
	p[3].y = 0;

	p[4].x = 2;
	p[4].y = 3;

	p[5].x = 0;
	p[5].y = -4;

	//pd::ExecutionResult_Greedy er;
	//er = Decompose1(p, 5);

	//FreeMemory1();

	return 0;
}

#endif


