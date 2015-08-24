#include "DeviceMatcher.h"
#include <math.h>
#ifndef PI
#define PI 3.141592f
#endif // !PI

//точки
float countDistanceBetweenPointsDEVICE(Point first, Point second)
{
	return (first.x - second.x)*(first.x - second.x) + (first.y - second.y)*(first.y - second.y);
}
Point countMovedPointDEVICE(Point p, float dx, float dy)
{
	Point result;
	result.x = p.x + dx;
	result.y = p.y + dy;
	return result;
}
Point countRotatedPointDEVICE(Point p, float cos_phi, float sin_phi)
{
	Point result;
	result.x = p.x * cos_phi - p.y * sin_phi;
	result.y = p.x * sin_phi + p.y * cos_phi;
	return result;
}

//треугольники
float countDistanceBetweenTrianglesDEVICE(Triangle* first, Triangle* second)
{
	return
		countDistanceBetweenPointsDEVICE(first->A, second->A) +
		countDistanceBetweenPointsDEVICE(first->B, second->B) +
		countDistanceBetweenPointsDEVICE(first->C, second->C);
}
Point countTriangleMassCenterDEVICE(Triangle* ABC)
{
	Point result;
	result.x = (ABC->A.x + ABC->B.x + ABC->C.x) / 3;
	result.y = (ABC->A.y + ABC->B.y + ABC->C.y) / 3;
	return result;
}
Triangle countMovedTriangleDEVICE(Triangle* ABC, float dx, float dy)
{
	Triangle result;
	result.A = countMovedPointDEVICE(ABC->A, dx, dy);
	result.B = countMovedPointDEVICE(ABC->B, dx, dy);
	result.C = countMovedPointDEVICE(ABC->C, dx, dy);
	return result;
}
Triangle countRotatedTriangleDEVICE(Triangle* ABC, float cos_phi, float sin_phi)
{
	Triangle result;
	result.A = countRotatedPointDEVICE(ABC->A, cos_phi, sin_phi);
	result.B = countRotatedPointDEVICE(ABC->B, cos_phi, sin_phi);
	result.C = countRotatedPointDEVICE(ABC->C, cos_phi, sin_phi);
	return result;
}
Triangle countTransformedTriangleDEVICE(Triangle* ABC, Transformation t)
{
	Point ABCmc = countTriangleMassCenterDEVICE(ABC);
	Triangle ABC_moved = countMovedTriangleDEVICE(ABC, -ABCmc.x, -ABCmc.y);
	Triangle ABC_moved_rotated = countRotatedTriangleDEVICE(&ABC_moved, t.cos_phi, t.sin_phi);
	return countMovedTriangleDEVICE(&ABC_moved_rotated, t.dx, t.dy);
}

//преобразования
float countOptimumlPhiDEVICE(float phi0, float sProd, float vProd, int maxIterations, float e)
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
TransformationWithDistance findOptimumTransformationABCDEVICE(Triangle* ABC_, Triangle* ABC, float e, int maxIterations, int parts)
{
	Point ABCmc = countTriangleMassCenterDEVICE(ABC);
	Triangle movedABC = countMovedTriangleDEVICE(ABC, -ABCmc.x, -ABCmc.y);

	Point ABC_mc = countTriangleMassCenterDEVICE(ABC_);
	Triangle movedABC_ = countMovedTriangleDEVICE(ABC_, -ABC_mc.x, -ABC_mc.y);

	float sProd = movedABC.A.x * movedABC_.A.x + movedABC.A.y * movedABC_.A.y + movedABC.B.x * movedABC_.B.x + movedABC.B.y * movedABC_.B.y + movedABC.C.x * movedABC_.C.x + movedABC.C.y * movedABC_.C.y;
	float vProd = movedABC.A.x * movedABC_.A.y - movedABC.A.y * movedABC_.A.x + movedABC.B.x * movedABC_.B.y - movedABC.B.y * movedABC_.B.x + movedABC.C.x * movedABC_.C.y - movedABC.C.y * movedABC_.C.x;

	TransformationWithDistance result;
	result.transformation.dx = ABCmc.x; result.transformation.dy = ABCmc.y;
	result.transformation.cos_phi = 1; result.transformation.sin_phi = 0;
	result.distance = countDistanceBetweenTrianglesDEVICE(&movedABC, &movedABC_);

	float optimumPhi;
	float optimum_cos;
	float optimum_sin;
	float step = 2 * PI / parts;
	Triangle tmpResult;
	float distance;

	for (int i = 0; i < parts; i++)
	{
		optimumPhi = countOptimumlPhiDEVICE(i * step - PI, sProd, vProd, maxIterations, e);
		optimum_cos = cosf(optimumPhi);
		optimum_sin = sinf(optimumPhi);

		tmpResult = countRotatedTriangleDEVICE(&movedABC_, optimum_cos, optimum_sin);

		distance = countDistanceBetweenTrianglesDEVICE(&movedABC, &tmpResult);
		if (distance < result.distance)
		{
			result.distance = distance;
			result.transformation.cos_phi = optimum_cos;
			result.transformation.sin_phi = optimum_sin;
		}
	}

	return result;
}
TransformationWithDistance findOptimumTransformationDEVICE(Triangle* ABC_, Triangle* ABC, float e, int maxIterations, int parts)
{
	TransformationWithDistance twdABC = findOptimumTransformationABCDEVICE(ABC_, ABC, e, maxIterations, parts);

	Triangle tmpTriangle;
	tmpTriangle.A = ABC_->B;
	tmpTriangle.B = ABC_->C;
	tmpTriangle.C = ABC_->A;
	TransformationWithDistance twdBCA = findOptimumTransformationABCDEVICE(&tmpTriangle, ABC, e, maxIterations, parts);

	tmpTriangle.A = ABC_->C;
	tmpTriangle.B = ABC_->A;
	tmpTriangle.C = ABC_->B;
	TransformationWithDistance twdCAB = findOptimumTransformationABCDEVICE(&tmpTriangle, ABC, e, maxIterations, parts);

	if (twdBCA.distance < twdABC.distance)
		twdABC = twdBCA;

	return (twdCAB.distance < twdABC.distance) ? twdCAB : twdABC;
}