#ifndef TrianglesMatcherWithCUDAHEADER
#define TrianglesMatcherWithCUDAHEADER

#include "cuda_runtime.h"
#include "DEVICE_launch_parameters.h"

typedef struct { float x; float y; } Point;
typedef struct { Point A; Point B; Point C; } Triangle;
typedef struct { float dx; float dy; float sin_phi; float cos_phi; } Transformation;
typedef struct { Transformation transformation; float distance; } TransformationWithDistance;
typedef enum{ TYPE_ABC, TYPE_BCA, TYPE_CAB }TriangleType;

/*
	Attention!
	���������� ������������� ������ ���������! - ���� ��� �� ������� �������, ���� ��� ������! ��� ������������� �����!
	� ������ ������ ������, ���������� ����� ����� �������������� (������� + �������), ������� �����
	�������������� ���������� ����� ��������� ������������� ABC(� ���� ����� ���������)
	� ABC_ - ��� ����� �������, �������, ����������� ��������.

	�������: ������� �������� �������. ��� �������� �� ���� ����������(dx, dy, dphi). ����� ����� ������������, ��������� ��� � ����.
	���������� ���������� ������� �� ���� ���������, � ����� ������������. ���� �� ��� ������? �������� �������, � ����� ��� ������� ���������� �������.
	���� ����������� �����������. ����������� ����� ����� 360 ��������� �������, ����� ����� ��� �����-����� ������. ������ ������ - ������ �� �����?
	���� ����� 360 ��� ������ ��������� �����������, � ����� ������� ��������� �������?
	��� �����. �������� ������, ������ ����. �� ��� �� 360 ��������... ������������ � ���������� � ���������� �������, � ����� ��������� ��������� �������:

	��� �� ����� ��������������� ����������� ABC_? ������� ������� ��� ����� ���� � ������ ���������, ����� �������� �� ����������� ����,
	� ����� ��������� ��� ����� ���� � ����� ���� ������������ ABC. ��� ��� ��� ����?
	1) ������ ������� �� ���� ���������� ����� ������������� ���� ������� �� phi
	2) ���������� ������� �������, ��� � ���������� �������, �� � �� ������� ������ ��� � ����������� ��������� (~10-15 ��������, �� �����)

	��������� ��������� ��������:
	0) ������� ������ ���� �������������, � �������������� ��������������: dx = -ABCmc.x, dy = -ABCmc.y, ��� ABCmc - ����� ���� ������������ ABC
	1) ��������� ������� ����������� ABC~
	2) ����� ������� ���������� ��������� ��������� �������:
		F = (A-A~)^2 + (B-B~)^2 + (C-C~)^2 =
		= (Ax - A~x)^2 + (Ay - A~y)^2 + ... + (Cx - C~x)^2 + (Cy - C~y)^2,
		��� ����:
		A~x = A_x cos_phi - A_y sin_phi
		A~y = A_x sin_phi + A_y cos_phi
	3) ����� ����� �������, ������� ���������� ����� ���� �����������
		dF = 2 * ( sin_phi(Ax * A_x + Ay * A_y) + cos_phi(Ax * A_y - Ay * A_x) + ...)
		dF^2 = 2 * ( cos_phi(Ax * A_x + Ay * A_y) - sin_phi(Ax * A_y - Ay * A_x)  + ...)

	4) ��������� sProd (��������� ������������) = Ax * A_x + Ay * A_y + ... + Cx * C_x + Cy * C_y
				 vProd (��������� ������������) = Ax * A_y - Ay * A_x + ... + Cx * C_y - Cy * C_x

		-> dF/2     = sin_phi * sProd + cos_phi * vProd
		-> (dF^2)/2 = cos_phi * sProd - sin_phi * vProd

	5) ������ ��� ���������� ��������� � ����������� ���������� �������������, �������� ������� ����
	6) � �������������� ���������� ���������� ������� ����� ����, � ��� �� �����, �������� ���������� ����� �������������� ���������

	7) ��������� ���� 3-6 ��� ������������� BCA_, CAB_
	8) ������� �������������� � ����������� �����������, ��� � ����� �������
	9) profit!

	�������:
	������������� ������� �������
	findOptimumTransformationPlusDistanceVersionCUDA(Triangle* ABC_, int ABC_size, Triangle* ABC, int ABCsize, Transformation* result, float* resultDistance, int maxIterations, float e, int parts)
		ABC_   - ������ �������������, ������� ����� ��������,   ABC_size - ��� ������
		ABC    - ������ �������������, � ������� ����� ��������, ABCsize  - ��� ������
		result - ������ ��������������, �������� ABC_size * ABCsize, ������ ��� ���� ����� ���������� �������(!)
		resultDistance - ������ ���������, ���������� �����������
		maxIteration   - ������������ ����� �������� ��� ���������� ������������ ����       ������������� ~ 10-15
		e      - ���������� �����������														������������� ~ 0.00001f - 0.000001f
		parts  - ����� ��������� ����������� ��� ���� phi									������������� ~ 3-5
*/


/*
���������� ���������� ����� ����� �������, ��� ���������� �����.
*/
__host__ __device__ float countDistanceBetweenPoints(Point first, Point second);
/*
���������� ������ �����, ��������� ������������ �������� �� dx, dy
*/
__host__ __device__ Point countMovedPoint(Point p, float dx, float dy);
/*
���������� ������ �����, ����� �������� ������������ ������ ��������� �� ���� phi,
����� �������� ��� ����� � �������
*/
__host__ __device__ Point countRotatedPoint(Point p, float cos_phi, float sin_phi);


//������������

/*
���������� ����� ���������� ����� ��������������� ��������� ���� �������������
*/
__host__ __device__ float countDistanceBetweenTriangles(Triangle* first, Triangle* second);
/*
���������� ������ ����
*/
__host__ __device__ Point countTriangleMassCenter(Triangle* ABC);
/*
���������� ������ ������������, ������������� �� dx, dy
*/
__host__ __device__ Triangle countMovedTriangle(Triangle* ABC, float dx, float dy);
/*
���������� ������ ������������, ����������� ������������ ������ ��������� �� ���� phi
���������� �������� ��� ����� � �������
*/
__host__ __device__ Triangle countRotatedTriangle(Triangle* ABC, float cos_phi, float sin_phi);
/*
1)�������� ����������� � ������ ���������
2)������������
3)����������� �� dx, dy
*/
__host__ __device__ Triangle countTransformedTriangle(Triangle* ABC, Transformation t);


//��������������
/*
������� ����������� ������������� ����� ABC_ -> ABC, BCA_ -> ABC, CBA_ -> ABC
*/
__host__ __device__ TransformationWithDistance findOptimumTransformation(Triangle* ABC_, Triangle* ABC, float e, int maxIterations, int parts);

cudaError_t findOptimumTransformationNonParallelForWithCuda(Triangle* ABC_, int ABC_size, Triangle* ABC, int ABCsize, TransformationWithDistance* result, int maxIterations, float e, int parts);
cudaError_t findOptimumTransformationParallelForWithCuda(Triangle* ABC_, int ABC_size, Triangle* ABC, int ABCsize, TransformationWithDistance* result, int maxIterations, float e, int parts);
cudaError_t findOptimumTransformationPlusDistanceVersionCUDA(Triangle* ABC_, int ABC_size, Triangle* ABC, int ABCsize, Transformation* result, float* resultDistance, int maxIterations, float e, int parts);
#endif