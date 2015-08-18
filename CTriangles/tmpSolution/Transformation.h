#pragma once
#include "Structures.h"

typedef struct TriangleTransformation2D { float dx; float dy; float dphi; } Transformation;

Point transformPoint(Point p, Transformation t);
Triangle transformTriangle(Triangle t, Transformation transformation);
Transformation getHardOptimumTransformation(Triangle ABC, Triangle ABC_, int maxIterations, int parts, float e);
Transformation getSoftOptimumTransformation(Triangle ABC, Triangle ABC_, int maxIterations, int parts, float e);