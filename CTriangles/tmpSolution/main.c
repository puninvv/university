#include "Transformation.h"
#include "Structures.h"
#include <omp.h>

void main() {
	Point A; A.x = 10; A.y = 8;
	Point B; B.x = 12; B.y = 8;
	Point C; C.x = 11; C.y = 6;

	Triangle ABC; ABC.A = A; ABC.B = B; ABC.C = C;

	Point A_; A_.x = 4; A_.y = 1;
	Point B_; B_.x = 8; B_.y = 3;
	Point C_; C_.x = 12; C_.y = 1;

	Triangle ABC_; ABC_.A = A_; ABC_.B = B_; ABC_.C = C_;

	Transformation t = getHardOptimumTransformation(ABC_, ABC, 100, 360, 0.0001);

	Transformation T = getSoftOptimumTransformation(ABC_, ABC, 100, 360, 0.0001);

	Triangle newABCt = transformTriangle(ABC, t);
	Triangle newABCT = transformTriangle(ABC, T);
}