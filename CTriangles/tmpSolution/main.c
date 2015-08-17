#include "Structures.h"
#include <omp.h>
#include <math.h>
#define Pi 3.14159265

//functions for system
float f1(float dx, float dy, float phi, Point ABC_c, Point ABCc, float vProd, float sProd)
{
	return ABC_c.x * cosf(phi) - ABC_c.y * sinf(phi) + dx - ABCc.x;
}
float f2(float dx, float dy, float phi, Point ABC_c, Point ABCc, float vProd, float sProd)
{
	return ABC_c.x * sinf(phi) + ABC_c.y * cosf(phi) + dy - ABCc.y;
}
float f3(float dx, float dy, float phi, Point ABC_c, Point ABCc, float vProd, float sProd)
{
	return cosf(phi) * (3 * dx * ABC_c.y - 3 * dy * ABC_c.x + vProd) +
		sinf(phi) * (3 * dx * ABC_c.x + 3 * dy * ABC_c.y - sProd);
}

float df1dphi(float dx, float dy, float phi, Point ABC_c, Point ABCc, float vProd, float sProd)
{
	return -ABC_c.x * sinf(phi) - ABC_c.y * cosf(phi);
}

float df2dphi(float dx, float dy, float phi, Point ABC_c, Point ABCc, float vProd, float sProd)
{
	return ABC_c.x * cosf(phi) - ABC_c.y * sinf(phi);
}

float df3dx(float dx, float dy, float phi, Point ABC_c, Point ABCc, float vProd, float sProd)
{
	return 3 * ABC_c.y * cosf(phi) + 3 * ABC_c.x * sinf(phi);
}
float df3dy(float dx, float dy, float phi, Point ABC_c, Point ABCc, float vProd, float sProd)
{
	return -3 * ABC_c.x * cosf(phi) - 3 * ABC_c.y * sinf(phi);
}
float df3dphi(float dx, float dy, float phi, Point ABC_c, Point ABCc, float vProd, float sProd)
{
	return -sinf(phi) * (3 * dx * ABC_c.y - 3 * dy * ABC_c.x + vProd) +
		cosf(phi) * (3 * dx * ABC_c.x + 3 * dy * ABC_c.y - sProd);
}


float getDistancePointToPoint(Point first, Point second)
{
	return sqrtf((first.x - second.x)*(first.x - second.x) + (first.y - second.y)*(first.y - second.y));
}

Point transformPoint(Point p, Transformation t)
{
	Point result;
	result.x = p.x * cosf(t.dphi) - p.y * sinf(t.dphi) + t.dx;
	result.y = p.x * sinf(t.dphi) + p.y * cosf(t.dphi) + t.dy;
	return result;
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

Triangle transformTriangle(Triangle t, Transformation transformation)
{
	Triangle result;
	result.A = transformPoint(t.A, transformation);
	result.B = transformPoint(t.B, transformation);
	result.C = transformPoint(t.C, transformation);
	return result;
}

Transformation solveLinearSystem(float dx0, float dy0, float dphi0, Triangle ABC, Triangle ABC_)
{
	Transformation t;
	float vProd = ABC.A.x * ABC_.A.y - ABC.A.y * ABC_.A.x +
		+ABC.B.x * ABC_.B.y - ABC.B.y * ABC_.B.x +
		+ABC.C.x * ABC_.C.y - ABC.C.y * ABC_.C.x;

	float sProd = ABC.A.x * ABC_.A.x + ABC.A.y * ABC_.A.y +
		+ABC.B.x * ABC_.B.x + ABC.B.y * ABC_.B.y +
		+ABC.C.x * ABC_.C.x + ABC.C.y * ABC_.C.y;

	Point ABCc = getMassCenter(ABC);
	Point ABC_c = getMassCenter(ABC_);

	float k1 = df1dphi(dx0, dy0, dphi0, ABC_c, ABCc, vProd, sProd);
	float k2 = df2dphi(dx0, dy0, dphi0, ABC_c, ABCc, vProd, sProd);
	float k3 = df3dx(dx0, dy0, dphi0, ABC_c, ABCc, vProd, sProd);
	float k4 = df3dy(dx0, dy0, dphi0, ABC_c, ABCc, vProd, sProd);
	float k5 = df3dphi(dx0, dy0, dphi0, ABC_c, ABCc, vProd, sProd);
	
	float F1 = f1(dx0, dy0, dphi0, ABC_c, ABCc, vProd, sProd);
	float F2 = f2(dx0, dy0, dphi0, ABC_c, ABCc, vProd, sProd);
	float F3 = f3(dx0, dy0, dphi0, ABC_c, ABCc, vProd, sProd);
	
	/*
	x + k1 phi = -f1;
	y + k2 phi = -f2;
	k3 x + k4 y + k5 phi = -f3;

	x = -f1 - k1 phi
	y = -f2 - k2 phi

	k3(-f1 - k1 phi) + k4 (-f2 -k2phi) + k5 phi = -f3

	phi( - k3k1 - k2k4 + k5) + (-f1k3 - f2k4) = -f3

	*/
	t.dphi = (-F3 + F1*k3 + F2*k4) / (k5 - k3*k1 - k2*k4);
	t.dx = -F1 - k1 * t.dphi;
	t.dy = -F2 - k2 * t.dphi;
	return t;
}

Transformation solveNonLinearSystem(float dphi0, Triangle ABC, Triangle ABC_, int maxIterations, float e)
{
	Transformation result; result.dx = 0; result.dy = 0; result.dphi = dphi0;

	Triangle tmpABC_ = transformTriangle(ABC_, result);

	for (int i = 0; i < maxIterations; i++)
	{
		Transformation tmpResult = solveLinearSystem(result.dx, result.dy, result.dphi, ABC, tmpABC_);
		if (tmpResult.dphi * tmpResult.dphi + tmpResult.dx * tmpResult.dx + tmpResult.dy * tmpResult.dy< e)
			break;
		else
		{
			result.dx += tmpResult.dx;
			result.dy += tmpResult.dy;
			result.dphi += tmpResult.dphi;
		}
	}

	return result;
}

Transformation getOptimumTransformation(Triangle ABC, Triangle ABC_, int maxIterations, int parts, float e)
{
	Transformation result;
	float distance = getDistanceTriangleToTriangle(ABC, ABC_);

	for (int i = 0; i < parts; i++)
	{
		float phi = Pi * i * 2.0 / parts; //(Math.PI / 180) * i * (360 / n);
		Transformation tmpResult = solveNonLinearSystem(phi, ABC, ABC_, maxIterations, e);
		tmpResult.dphi += phi;
		float newDistance = getDistanceTriangleToTriangle(ABC, transformTriangle(ABC_, tmpResult));
		
		if (newDistance < distance)
		{
			result = tmpResult;
			distance = newDistance;
		}
	}
	
	return result;
}

Transformation getHardOptimumTransformation(Triangle ABC, Triangle ABC_, int maxIterations, int parts, float e)
{
	return getOptimumTransformation(ABC, ABC_, maxIterations, parts, e);
}

Transformation getSoftOptimumTransformation(Triangle ABC, Triangle ABC_, int maxIterations, int parts, float e)
{
	Transformation first = getOptimumTransformation(ABC, ABC_, maxIterations, parts, e);
	
	Triangle second; second.A = ABC_.B; second.B = ABC_.C; second.C = ABC_.A;
	Transformation secondt = getOptimumTransformation(ABC, second, maxIterations, parts, e);

	Triangle third; third.A = ABC_.C; third.B = ABC_.A; third.C = ABC_.B;
	Transformation thirdt = getOptimumTransformation(ABC, third, maxIterations, parts, e);

	float min_distance = getDistanceTriangleToTriangle(ABC,transformTriangle(ABC,first));
	Transformation toReturn = first;

	float distance = getDistanceTriangleToTriangle(ABC, transformTriangle(second, secondt));
	if (distance < min_distance) 
	{
		toReturn = secondt; 
		min_distance = distance;
	}

	distance = getDistanceTriangleToTriangle(ABC, transformTriangle(third, thirdt));
	if (distance < min_distance)
	{
		toReturn = thirdt;
		min_distance = distance;
	}
	return toReturn;
}

void main(){
	Point A; A.x = 10; A.y = 8;
	Point B; B.x = 12; B.y = 8;
	Point C; C.x = 11; C.y = 6;

	Triangle ABC; ABC.A = A; ABC.B = B; ABC.C = C;


	Point A_; A_.x = 4; A_.y = 1;
	Point B_; B_.x = 8; B_.y = 3;
	Point C_; C_.x = 12; C_.y = 1;

	Triangle ABC_; ABC_.A = A_; ABC_.B = B_; ABC_.C = C_;

	time_t t0 = omp_get_wtime();
	Transformation t = getOptimumTransformation(ABC_, ABC, 1000, 360, 0.0001);
	time_t t1 = omp_get_wtime();
	time_t dt = t1 - t0;

	t0 = omp_get_wtime();
	Transformation T = getSoftOptimumTransformation(ABC_, ABC, 10, 90, 0.0001);
	t1 = omp_get_wtime();
	dt = t1 - t0;

	t0 = omp_get_wtime();
	T = getSoftOptimumTransformation(ABC_, ABC, 10, 180, 0.0001);
	t1 = omp_get_wtime();
	dt = t1 - t0;

	t0 = omp_get_wtime();
	T = getSoftOptimumTransformation(ABC_, ABC, 10, 360, 0.0001);
	t1 = omp_get_wtime();
	dt = t1 - t0;

	t0 = omp_get_wtime();
	T = getSoftOptimumTransformation(ABC_, ABC, 10, 720, 0.0001);
	t1 = omp_get_wtime();
	dt = t1 - t0;

	t0 = omp_get_wtime();
	T = getSoftOptimumTransformation(ABC_, ABC, 10, 1440, 0.0001);
	t1 = omp_get_wtime();
	dt = t1 - t0;

	Triangle newABC = transformTriangle(ABC, T);
}