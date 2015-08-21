#include <math.h>

typedef struct { float x; float y; } Point;
typedef struct { Point A; Point B; Point C; } Triangle;
typedef struct { float dx; float dy; float sin_phi; float cos_phi; } Transformation;
typedef struct { Transformation transformation; float distance; } TransformationWithDistance;

float countDistanceBetweenPoints(Point first, Point second)
{
	return sqrtf((first.x - second.x)*(first.x - second.x) + (first.y - second.y)*(first.y - second.y));
}

float countDistanceBetweenTriangles(Triangle first, Triangle second, short reflect)
{
	float result;

	float dABCabc =
		countDistanceBetweenPoints(first.A, second.A) +
		countDistanceBetweenPoints(first.B, second.B) +
		countDistanceBetweenPoints(first.C, second.C);

	float dABCbca =
		countDistanceBetweenPoints(first.A, second.B) +
		countDistanceBetweenPoints(first.B, second.C) +
		countDistanceBetweenPoints(first.C, second.A);

	float dABCcab =
		countDistanceBetweenPoints(first.A, second.C) +
		countDistanceBetweenPoints(first.B, second.A) +
		countDistanceBetweenPoints(first.C, second.B);

	result = fminf(fminf(dABCabc, dABCbca), dABCcab);
	if (!reflect)
		return result;

	float dABCacb =
		countDistanceBetweenPoints(first.A, second.A) +
		countDistanceBetweenPoints(first.B, second.C) +
		countDistanceBetweenPoints(first.C, second.B);

	float dABCbac =
		countDistanceBetweenPoints(first.A, second.B) +
		countDistanceBetweenPoints(first.B, second.A) +
		countDistanceBetweenPoints(first.C, second.C);

	float dABCcba =
		countDistanceBetweenPoints(first.A, second.C) +
		countDistanceBetweenPoints(first.B, second.B) +
		countDistanceBetweenPoints(first.C, second.A);

	return fminf(fminf(result, dABCacb), fminf(dABCbac, dABCcba));
}

Point countTriangleMassCenter(Triangle ABC)
{
	Point result;
	result.x = (ABC.A.x + ABC.B.x + ABC.C.x) / 3;
	result.y = (ABC.A.y + ABC.B.y + ABC.C.y) / 3;
	return result;
}

Point countMovedPoint(Point p, float dx, float dy)
{
	Point result;
	result.x = p.x + dx;
	result.y = p.y + dy;
	return result;
}
Point countRotatedPoint(Point p, float cos_phi, float sin_phi)
{
	Point result;
	result.x = p.x * cos_phi - p.y * sin_phi;
	result.y = p.x * sin_phi + p.y * cos_phi;
	return result;
}

Triangle countMovedTriangle(Triangle ABC, float dx, float dy)
{
	Triangle result;
	result.A = countMovedPoint(ABC.A, dx, dy);
	result.B = countMovedPoint(ABC.B, dx, dy);
	result.C = countMovedPoint(ABC.C, dx, dy);
	return result;
}
Triangle countRotatedTriangle(Triangle ABC, float cos_phi, float sin_phi)
{
	Triangle result;
	result.A = countRotatedPoint(ABC.A, cos_phi, sin_phi);
	result.B = countRotatedPoint(ABC.B, cos_phi, sin_phi);
	result.C = countRotatedPoint(ABC.C, cos_phi, sin_phi);
	return result;
}

Triangle countTransformedTriangle(Triangle ABC, Transformation t)
{
	/*
		чтобы произвести преобразование, необходимо
		сначала перенести треугольник в начало отсчета
		затем повернуть его
		и лишь после этого перенести
	*/

	Point ABCmc = countTriangleMassCenter(ABC);
	Triangle ABC_moved = countMovedTriangle(ABC, -ABCmc.x, -ABCmc.y);
	Triangle ABC_moved_rotated = countRotatedTriangle(ABC_moved, t.cos_phi, t.sin_phi);
	return countMovedTriangle(ABC_moved_rotated, t.dx, t.dy);
}

float F(Triangle ABC_, Triangle ABC, float cos_phi, float sin_phi)
{
	return
		(ABC.A.x - ABC_.A.x * cos_phi - ABC_.A.y * sin_phi) * (ABC.A.x - ABC_.A.x * cos_phi - ABC_.A.y * sin_phi) + (ABC.A.y - ABC_.A.x * sin_phi + ABC_.A.y * cos_phi) * (ABC.A.y - ABC_.A.x * sin_phi + ABC_.A.y * cos_phi) +
		(ABC.B.x - ABC_.B.x * cos_phi - ABC_.B.y * sin_phi) * (ABC.B.x - ABC_.B.x * cos_phi - ABC_.B.y * sin_phi) + (ABC.B.y - ABC_.B.x * sin_phi + ABC_.B.y * cos_phi) * (ABC.B.y - ABC_.B.x * sin_phi + ABC_.B.y * cos_phi) +
		(ABC.C.x - ABC_.C.x * cos_phi - ABC_.C.y * sin_phi) * (ABC.C.x - ABC_.C.x * cos_phi - ABC_.C.y * sin_phi) + (ABC.C.y - ABC_.C.x * sin_phi + ABC_.C.y * cos_phi) + (ABC.C.y - ABC_.C.x * sin_phi + ABC_.C.y * cos_phi);
}
float dF(Triangle ABC_, Triangle ABC, float cos_phi, float sin_phi)
{
	return 2 * (
		ABC.A.x * (ABC_.A.x * sin_phi - ABC_.A.y * cos_phi) + ABC.A.y * (-ABC_.A.x * cos_phi - ABC_.A.y * sin_phi) +
		ABC.B.x * (ABC_.B.x * sin_phi - ABC_.B.y * cos_phi) + ABC.B.y * (-ABC_.B.x * cos_phi - ABC_.B.y * sin_phi) +
		ABC.C.x * (ABC_.C.x * sin_phi - ABC_.C.y * cos_phi) + ABC.C.y * (-ABC_.C.x * cos_phi - ABC_.C.y * sin_phi));
}