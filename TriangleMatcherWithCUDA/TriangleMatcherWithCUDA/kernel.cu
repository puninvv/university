#include "cuda_runtime.h"
#include "device_launch_parameters.h"

#include <malloc.h>
#include <stdlib.h>
#include <time.h>
#include <math.h>

#define defaultRow() blockIdx.y*blockDim.y + threadIdx.y
#define defaultColumn() blockIdx.x*blockDim.x + threadIdx.x
#define ceilMod(x ,y) (x + y - 1)/(y)

#define PI 3.141592f
const int defaultThreadCount = 16;

typedef struct { float x; float y; } Point;
typedef struct { Point A; Point B; Point C; } Triangle;
typedef struct { float dx; float dy; float sin_phi; float cos_phi; } Transformation;
typedef struct { Transformation transformation; float distance; } TransformationWithDistance;

//точки
__host__ float countDistanceBetweenPointsHOST(Point first, Point second)
{
	return (first.x - second.x)*(first.x - second.x) + (first.y - second.y)*(first.y - second.y);
}
__host__ Point countMovedPointHOST(Point p, float dx, float dy)
{
	Point result;
	result.x = p.x + dx;
	result.y = p.y + dy;
	return result;
}
__host__ Point countRotatedPointHOST(Point p, float cos_phi, float sin_phi)
{
	Point result;
	result.x = p.x * cos_phi - p.y * sin_phi;
	result.y = p.x * sin_phi + p.y * cos_phi;
	return result;
}

//треугольники
__host__ float countDistanceBetweenTrianglesHOST(Triangle* first, Triangle* second)
{
	return
		countDistanceBetweenPointsHOST(first->A, second->A) +
		countDistanceBetweenPointsHOST(first->B, second->B) +
		countDistanceBetweenPointsHOST(first->C, second->C);
}
__host__ Point countTriangleMassCenterHOST(Triangle* ABC)
{
	Point result;
	result.x = (ABC->A.x + ABC->B.x + ABC->C.x) / 3;
	result.y = (ABC->A.y + ABC->B.y + ABC->C.y) / 3;
	return result;
}
__host__ Triangle countMovedTriangleHOST(Triangle* ABC, float dx, float dy)
{
	Triangle result;
	result.A = countMovedPointHOST(ABC->A, dx, dy);
	result.B = countMovedPointHOST(ABC->B, dx, dy);
	result.C = countMovedPointHOST(ABC->C, dx, dy);
	return result;
}
__host__ Triangle countRotatedTriangleHOST(Triangle* ABC, float cos_phi, float sin_phi)
{
	Triangle result;
	result.A = countRotatedPointHOST(ABC->A, cos_phi, sin_phi);
	result.B = countRotatedPointHOST(ABC->B, cos_phi, sin_phi);
	result.C = countRotatedPointHOST(ABC->C, cos_phi, sin_phi);
	return result;
}
__host__ Triangle countTransformedTriangleHOST(Triangle* ABC, Transformation t)
{
	Point ABCmc = countTriangleMassCenterHOST(ABC);
	Triangle ABC_moved = countMovedTriangleHOST(ABC, -ABCmc.x, -ABCmc.y);
	Triangle ABC_moved_rotated = countRotatedTriangleHOST(&ABC_moved, t.cos_phi, t.sin_phi);
	return countMovedTriangleHOST(&ABC_moved_rotated, t.dx, t.dy);
}

//преобразования
__host__ float countOptimumlPhiHOST(float phi0, float sProd, float vProd, int maxIterations, float e)
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
__host__ TransformationWithDistance findOptimumTransformationABCHOST(Triangle* ABC_, Triangle* ABC, float e, int maxIterations, int parts)
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
__host__ TransformationWithDistance findOptimumTransformationHOST(Triangle* ABC_, Triangle* ABC, float e, int maxIterations, int parts)
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


//точки
__device__ float countDistanceBetweenPointsDEVICE(Point first, Point second)
{
	return (first.x - second.x)*(first.x - second.x) + (first.y - second.y)*(first.y - second.y);
}
__device__ Point countMovedPointDEVICE(Point p, float dx, float dy)
{
	Point result;
	result.x = p.x + dx;
	result.y = p.y + dy;
	return result;
}
__device__ Point countRotatedPointDEVICE(Point p, float cos_phi, float sin_phi)
{
	Point result;
	result.x = p.x * cos_phi - p.y * sin_phi;
	result.y = p.x * sin_phi + p.y * cos_phi;
	return result;
}

//треугольники
__device__ float countDistanceBetweenTrianglesDEVICE(Triangle* first, Triangle* second)
{
	return
		countDistanceBetweenPointsDEVICE(first->A, second->A) +
		countDistanceBetweenPointsDEVICE(first->B, second->B) +
		countDistanceBetweenPointsDEVICE(first->C, second->C);
}
__device__ Point countTriangleMassCenterDEVICE(Triangle* ABC)
{
	Point result;
	result.x = (ABC->A.x + ABC->B.x + ABC->C.x) / 3;
	result.y = (ABC->A.y + ABC->B.y + ABC->C.y) / 3;
	return result;
}
__device__ Triangle countMovedTriangleDEVICE(Triangle* ABC, float dx, float dy)
{
	Triangle result;
	result.A = countMovedPointDEVICE(ABC->A, dx, dy);
	result.B = countMovedPointDEVICE(ABC->B, dx, dy);
	result.C = countMovedPointDEVICE(ABC->C, dx, dy);
	return result;
}
__device__ Triangle countRotatedTriangleDEVICE(Triangle* ABC, float cos_phi, float sin_phi)
{
	Triangle result;
	result.A = countRotatedPointDEVICE(ABC->A, cos_phi, sin_phi);
	result.B = countRotatedPointDEVICE(ABC->B, cos_phi, sin_phi);
	result.C = countRotatedPointDEVICE(ABC->C, cos_phi, sin_phi);
	return result;
}
__device__ Triangle countTransformedTriangleDEVICE(Triangle* ABC, Transformation t)
{
	Point ABCmc = countTriangleMassCenterDEVICE(ABC);
	Triangle ABC_moved = countMovedTriangleDEVICE(ABC, -ABCmc.x, -ABCmc.y);
	Triangle ABC_moved_rotated = countRotatedTriangleDEVICE(&ABC_moved, t.cos_phi, t.sin_phi);
	return countMovedTriangleDEVICE(&ABC_moved_rotated, t.dx, t.dy);
}

//преобразования
__device__ float countOptimumlPhiDEVICE(float phi0, float sProd, float vProd, int maxIterations, float e)
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
__device__ TransformationWithDistance findOptimumTransformationABCDEVICE(Triangle* ABC_, Triangle* ABC, float e, int maxIterations, int parts)
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
__device__ TransformationWithDistance findOptimumTransformationDEVICE(Triangle* ABC_, Triangle* ABC, float e, int maxIterations, int parts)
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


__global__ void fOTKernel(Triangle* ABC_, int ABC_size, Triangle* ABC, int ABCsize, TransformationWithDistance* result, int maxIterations, float e, int parts)
{
	extern __shared__ Triangle cache[];
		
	int row = defaultRow();
	int column = defaultColumn();

	if (row < ABC_size && column < ABCsize)
	{
		Triangle abc_ = ABC_[row];
		Triangle abc = ABC[column];
		result[row * ABCsize + column] = findOptimumTransformationDEVICE(&abc_, &abc, e, maxIterations, parts);
	}
}
cudaError_t findOptimumTransformationWithCuda(Triangle* ABC_, int ABC_size, Triangle* ABC, int ABCsize, TransformationWithDistance* result, int maxIterations, float e, int parts)
{
	
	Triangle* devABC_;
	Triangle* devABC;
	TransformationWithDistance* devResult;

	cudaError_t cudaStatus;

	cudaStatus = cudaMalloc((void**)& devABC_, ABC_size * sizeof(Triangle));
	if (cudaStatus != cudaSuccess)
		goto Error;

	cudaStatus = cudaMalloc((void**)& devABC, ABCsize * sizeof(Triangle));
	if (cudaStatus != cudaSuccess)
		goto Error;

	cudaStatus = cudaMalloc((void**)& devResult, ABC_size * ABCsize * sizeof(TransformationWithDistance));
	if (cudaStatus != cudaSuccess)
		goto Error;

	cudaStatus = cudaMemcpy(devABC_, ABC_, ABC_size * sizeof(Triangle), cudaMemcpyHostToDevice);
	if (cudaStatus != cudaSuccess)
		goto Error;

	cudaStatus = cudaMemcpy(devABC, ABC, ABCsize * sizeof(Triangle), cudaMemcpyHostToDevice);
	if (cudaStatus != cudaSuccess)
		goto Error;

	dim3 threads(defaultThreadCount, defaultThreadCount);
	dim3 blocks(ceilMod(ABC_size, defaultThreadCount), ceilMod(ABCsize,defaultThreadCount));

	//void findOptimumTransformationKernel(Triangle* ABC_, int ABC_size, Triangle* ABC, int ABCsize, TransformationWithDistance* result, int maxIterations, float e, int parts)
	fOTKernel <<< blocks, threads, 2*sizeof(Triangle)*defaultThreadCount >>>(devABC_, ABC_size, devABC, ABCsize, devResult, maxIterations, e, parts);

	cudaStatus = cudaGetLastError();
	if (cudaStatus != cudaSuccess)
		goto Error;

	cudaStatus = cudaDeviceSynchronize();
	if (cudaStatus != cudaSuccess)
		goto Error;

	cudaStatus = cudaMemcpy(result, devResult, ABC_size * ABCsize * sizeof(TransformationWithDistance), cudaMemcpyDeviceToHost);
	if (cudaStatus != cudaSuccess)
		goto Error;

Error:
	cudaFree(devABC_);
	cudaFree(devABC);
	cudaFree(devResult);

	return cudaStatus;
}

int main()
{
	int max_rand = 100;

	int ABCsize = 100;
	int ABC_size = 200;
	Triangle* ABC = (Triangle*)malloc(ABCsize * sizeof(Triangle));
	Triangle* ABC_ = (Triangle*)malloc(ABC_size * sizeof(Triangle));

	srand(time(NULL));                      // инициализация функции rand значением функции time

	for (int i = 0; i < ABCsize; i++)
	{
		Triangle ABCt;

		ABCt.A.x = max_rand / 2 - rand() % max_rand;
		ABCt.A.y = max_rand / 2 - rand() % max_rand;

		ABCt.B.x = max_rand / 2 - rand() % max_rand;
		ABCt.B.y = max_rand / 2 - rand() % max_rand;

		ABCt.C.x = max_rand / 2 - rand() % max_rand;
		ABCt.C.y = max_rand / 2 - rand() % max_rand;
		ABC[i] = ABCt;
	}

	for (int i = 0; i < ABC_size; i++)
	{
		Triangle ABCt;

		ABCt.A.x = max_rand / 2 - rand() % max_rand;
		ABCt.A.y = max_rand / 2 - rand() % max_rand;

		ABCt.B.x = max_rand / 2 - rand() % max_rand;
		ABCt.B.y = max_rand / 2 - rand() % max_rand;

		ABCt.C.x = max_rand / 2 - rand() % max_rand;
		ABCt.C.y = max_rand / 2 - rand() % max_rand;
		ABC_[i] = ABCt;
	}


	TransformationWithDistance* result = (TransformationWithDistance*)malloc(ABC_size * ABCsize * sizeof(TransformationWithDistance));
	cudaError_t cudaStatus = findOptimumTransformationWithCuda(ABC_, ABC_size, ABC, ABCsize, result, 10, 0.00001f, 3);

	if (cudaStatus != cudaSuccess)
		goto End;

	cudaStatus = cudaDeviceReset();
	if (cudaStatus != cudaSuccess)
		goto End;

End:
	free(ABC);
	free(ABC_);
	free(result);

	return 0;
}