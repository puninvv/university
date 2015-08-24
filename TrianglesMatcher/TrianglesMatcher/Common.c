#include <math.h>
#include "Structures.h"
#include "DeviceMatcher.h"
#include "HostMatcher.h"

void main()
{
	Point a_; a_.x = 0; a_.y = 0;
	Point b_; b_.x = 0; b_.y = 2;
	Point c_; c_.x = 2; c_.y = 0;
	Triangle ABC_; ABC_.A = a_; ABC_.B = b_; ABC_.C = c_;

	Point a; a.x = -2; a.y = -2;
	Point b; b.x = 0; b.y = 0;
	Point c; c.x = 2; c.y = -2;
	Triangle ABC; ABC.A = a; ABC.B = b; ABC.C = c;

	TransformationWithDistance twd = findOptimumTransformationHOST(&ABC_, &ABC, 0.000001f, 10, 3);

	Triangle result = countTransformedTriangleHOST(&ABC_, twd.transformation);
}