#pragma once
#ifndef HostMatcherHEADER
#define HostMatcherHEADER

#include "Structures.h"

/*
	��, ��� �������� �����:
		1) ���������� ���������� (������ �� �����������)
		2) ���������� ��������� ����� �� dx, dy
		3) ���������� �����, ���������� ������������ ������ ��������� �� ���� phi,
			� ������� ����� �������� �������� �����, cos(phi), sin(phi) - �� ����� ���������
			� ���������� ������� (����� �� ������� ��������� ��� ����� � �������)
*/
float countDistanceBetweenPointsHOST(Point first, Point second);
Point countMovedPointHOST(Point p, float dx, float dy);
Point countRotatedPointHOST(Point p, float cos_phi, float sin_phi);

/*
	��, ��� �������� ������������:
		1) ���������� ���������� ����� �������������� -
			����������� ���������� ����� ��������������� ���������, ������ �� �����������
		2) ���������� ������ ����
		3) ���������� ������������� ������������ �� dx, dy
		4) ���������� ����������� ������������ ������ ��������� ������������ �� ���� phi;
			��� � � �������, ����� �������� sin, cos (phi)
		5) ���������� ���������������� ������������:
		������� �������� � ������ ���������, ����� ������, ����� ��������
*/
float countDistanceBetweenTrianglesHOST(Triangle* first, Triangle* second);
Point countTriangleMassCenterHOST(Triangle* ABC);
Triangle countMovedTriangleHOST(Triangle* ABC, float dx, float dy);
Triangle countRotatedTriangleHOST(Triangle* ABC, float cos_phi, float sin_phi);
Triangle countTransformedTriangleHOST(Triangle* ABC, Transformation t);

/*
	������������ ������ ���� ������������� (!)�� ������� �������.

	������ ��� ����������� ������������:
	1) ���������� ������������ ����:
		* � �������� �������������� ������� ���� ����� ����� ��������� ��������������� ��������� ������ � �������������
			��� ����� � ������������ ABC ����� ��������� ��������� �������:
			(Ax - A_x * cos_phi + A_y sin_phi)^2 + (Ay - A_x sin_phi - A_y cos_phi)^2, ���
			ABC - "����", ABC_ - "��, ��� �������"
			����� F = ����� �������� ��������� ��� A, B, C. ������� ������� (!)���� �� phi, �� ������� ���������
		* ����� ����� � �������, ���������� ����� ���� �������������.
			dF = 2 (( Ax A_y - Ay A_x) cos_phi + (Ax A_x + Ay A_y) sin_phi) + ...
		* ������������ - ���������� ��������� ������������ phi, ����� ����� ������ ������� �������, �� �������
			[-pi, pi]
			��� ����� ������ ������ ������������:
			dF^2 = 2 ((Ax A_x + Ay A_y) cos_phi - ( Ax A_y - Ay A_x) sin_phi) + ...
		* ���������:
			sProd = Ax A_x + Ay A_y + Bx B_x + By B_y + Cx C_x + Cy C_y (����� ��������� ������������)
			vProd = Ax A_y - Ay A_x + Bx B_y - By B_x + Cx C_y - Cy C_x (����� "���������" ������������)
		* �����:
			dF = 2 ( sin_phi sProd + cos_phi vProd )
			dF^2 = 2 (cos_phi sProd - sin_phi vProd )
			����� ���� ����� ��������� ��������� ����������� - ����� ���������� �� n ������
			sProd, vProd ����� ��������� ����� 1 ��� - � ����� ������, ����� �������� ������������� � ������ ���������
		* profit!
	2) ���������� ������������ ��������������: ������� �� ��� �������, ��� ABC, � � �����
		* ��� ABC - ��������� ������������ � ������, ����� ���������� �� ������ ����� (�������� ��������� �����������),
			��������� ����������� ����, ������ �����������, �������� ����������, ���� ������ ��������� - ��������� ���������
		* ��� ���� �����, �� ��� ��� BCA, CAB
*/
float countOptimumlPhiHOST(float phi0, float sProd, float vProd, int maxIterations, float e);
TransformationWithDistance findOptimumTransformationABCHOST(Triangle* ABC_, Triangle* ABC, float e, int maxIterations, int parts);
TransformationWithDistance findOptimumTransformationHOST(Triangle* ABC_, Triangle* ABC, float e, int maxIterations, int parts);
#endif