#include "Structures.h"
#include <omp.h>
#include <math.h>
#define Pi 3.14159265

//functions for system

float getDistancePointToPoint(Point first, Point second)
{
	return sqrtf((first.x - second.x)*(first.x - second.x) + (first.y - second.y)*(first.y - second.y));
}

Point getMassCenter(Triangle triangle)
{
	Point p;
	p.x = (triangle.A.x + triangle.B.x + triangle.C.x) / 3;
	p.y = (triangle.A.y + triangle.B.y + triangle.C.y) / 3;
	return p;
}

float getDistanceTriangleToTriangle(Triangle ABC, Triangle ABC_)
{
	float d1 = getDistancePointToPoint(ABC.A, ABC_.A) + getDistancePointToPoint(ABC.B, ABC_.B) + getDistancePointToPoint(ABC.C, ABC_.C);
	float d2 = getDistancePointToPoint(ABC.A, ABC_.B) + getDistancePointToPoint(ABC.A, ABC_.C) + getDistancePointToPoint(ABC.C, ABC_.A);
	float d3 = getDistancePointToPoint(ABC.A, ABC_.C) + getDistancePointToPoint(ABC.A, ABC_.A) + getDistancePointToPoint(ABC.C, ABC_.B);

	return fmin(fmin(d1, d2), d3);
}