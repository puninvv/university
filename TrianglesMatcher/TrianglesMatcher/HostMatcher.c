#include "HOSTMatcher.h"
#include <math.h>

#ifndef PI
#define PI 3.141592f
#endif // !PI

//точки
float countDistanceBetweenPointsHOST(Point first, Point second)
{
	return (first.x - second.x)*(first.x - second.x) + (first.y - second.y)*(first.y - second.y);
}
Point countMovedPointHOST(Point p, float dx, float dy)
{
	Point result;
	result.x = p.x + dx;
	result.y = p.y + dy;
	return result;
}
Point countRotatedPointHOST(Point p, float cos_phi, float sin_phi)
{
	Point result;
	result.x = p.x * cos_phi - p.y * sin_phi;
	result.y = p.x * sin_phi + p.y * cos_phi;
	return result;
}

//треугольники
float countDistanceBetweenTrianglesHOST(Triangle* first, Triangle* second)
{
	return
		countDistanceBetweenPointsHOST(first->A, second->A) +
		countDistanceBetweenPointsHOST(first->B, second->B) +
		countDistanceBetweenPointsHOST(first->C, second->C);
}
Point countTriangleMassCenterHOST(Triangle* ABC)
{
	Point result;
	result.x = (ABC->A.x + ABC->B.x + ABC->C.x) / 3;
	result.y = (ABC->A.y + ABC->B.y + ABC->C.y) / 3;
	return result;
}
Triangle countMovedTriangleHOST(Triangle* ABC, float dx, float dy)
{
	Triangle result;
	result.A = countMovedPointHOST(ABC->A, dx, dy);
	result.B = countMovedPointHOST(ABC->B, dx, dy);
	result.C = countMovedPointHOST(ABC->C, dx, dy);
	return result;
}
Triangle countRotatedTriangleHOST(Triangle* ABC, float cos_phi, float sin_phi)
{
	Triangle result;
	result.A = countRotatedPointHOST(ABC->A, cos_phi, sin_phi);
	result.B = countRotatedPointHOST(ABC->B, cos_phi, sin_phi);
	result.C = countRotatedPointHOST(ABC->C, cos_phi, sin_phi);
	return result;
}
Triangle countTransformedTriangleHOST(Triangle* ABC, Transformation t)
{
	Point ABCmc = countTriangleMassCenterHOST(ABC);
	Triangle ABC_moved = countMovedTriangleHOST(ABC, -ABCmc.x, -ABCmc.y);
	Triangle ABC_moved_rotated = countRotatedTriangleHOST(&ABC_moved, t.cos_phi, t.sin_phi);
	return countMovedTriangleHOST(&ABC_moved_rotated, t.dx, t.dy);
}

//преобразования
float countOptimumlPhiHOST(float phi0, float sProd, float vProd, int maxIterations, float e)
{
	float resultPhi = phi0;

	float cos_phi;
	float sin_phi;
	float newPhi;
	for (int i = 0; i < maxIterations; i++)
	{
		cos_phi = cosf(resultPhi);
		sin_phi = sinf(resultPhi);

		newPhi = resultPhi - (sin_phi * sProd + cos_phi * vProd) / (cos_phi * sProd - sin_phi * vProd);

		if (fabsf(newPhi - resultPhi) < e)
			return newPhi;
		else
			resultPhi = newPhi;
	}

	return resultPhi;
}
TransformationWithDistance findOptimumTransformationABCHOST(Triangle* ABC_, Triangle* ABC, float e, int maxIterations, int parts)
{
	Point ABCmc = countTriangleMassCenterHOST(ABC);
	Triangle movedABC = countMovedTriangleHOST(ABC, -ABCmc.x, -ABCmc.y);

	Point ABC_mc = countTriangleMassCenterHOST(ABC_);
	Triangle movedABC_ = countMovedTriangleHOST(ABC_, -ABC_mc.x, -ABC_mc.y);

	float sProd = movedABC.A.x * movedABC_.A.x + movedABC.A.y * movedABC_.A.y + movedABC.B.x * movedABC_.B.x + movedABC.B.y * movedABC_.B.y + movedABC.C.x * movedABC_.C.x + movedABC.C.y * movedABC_.C.y;
	float vProd = movedABC.A.x * movedABC_.A.y - movedABC.A.y * movedABC_.A.x + movedABC.B.x * movedABC_.B.y - movedABC.B.y * movedABC_.B.x + movedABC.C.x * movedABC_.C.y - movedABC.C.y * movedABC_.C.x;

	TransformationWithDistance result;
	result.transformation.dx = ABCmc.x; result.transformation.dy = ABCmc.y;
	result.transformation.cos_phi = 1; result.transformation.sin_phi = 0;
	result.distance = countDistanceBetweenTrianglesHOST(&movedABC, &movedABC_);

	float optimumPhi;
	float optimum_cos;
	float optimum_sin;
	float step = 2 * PI / parts;
	Triangle tmpResult;
	float distance;

	for (int i = 0; i < parts; i++)
	{
		optimumPhi = countOptimumlPhiHOST(i * step - PI, sProd, vProd, maxIterations, e);
		optimum_cos = cosf(optimumPhi);
		optimum_sin = sinf(optimumPhi);

		tmpResult = countRotatedTriangleHOST(&movedABC_, optimum_cos, optimum_sin);

		distance = countDistanceBetweenTrianglesHOST(&movedABC, &tmpResult);
		if (distance < result.distance)
		{
			result.distance = distance;
			result.transformation.cos_phi = optimum_cos;
			result.transformation.sin_phi = optimum_sin;
		}
	}

	return result;
}
TransformationWithDistance findOptimumTransformationHOST(Triangle* ABC_, Triangle* ABC, float e, int maxIterations, int parts)
{
	TransformationWithDistance twdABC = findOptimumTransformationABCHOST(ABC_, ABC, e, maxIterations, parts);

	Triangle tmpTriangle;
	tmpTriangle.A = ABC_->B;
	tmpTriangle.B = ABC_->C;
	tmpTriangle.C = ABC_->A;
	TransformationWithDistance twdBCA = findOptimumTransformationABCHOST(&tmpTriangle, ABC, e, maxIterations, parts);

	tmpTriangle.A = ABC_->C;
	tmpTriangle.B = ABC_->A;
	tmpTriangle.C = ABC_->B;
	TransformationWithDistance twdCAB = findOptimumTransformationABCHOST(&tmpTriangle, ABC, e, maxIterations, parts);

	if (twdBCA.distance < twdABC.distance)
		twdABC = twdBCA;

	return (twdCAB.distance < twdABC.distance) ? twdCAB : twdABC;
}