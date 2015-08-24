#pragma once
#ifndef StructuresHEADER
#define StructuresHEADER

typedef struct { float x; float y; } Point;
typedef struct { Point A; Point B; Point C; } Triangle;
typedef struct { float dx; float dy; float sin_phi; float cos_phi; } Transformation;
typedef struct { Transformation transformation; float distance; } TransformationWithDistance;

#endif