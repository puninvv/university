#ifndef POINT_TRIANGLE
#define POINT_TRIANGLE

typedef struct Point2d { float x; float y; } Point;
typedef struct Triangle2D { Point A; Point B; Point C; } Triangle;

float getDistancePointToPoint(Point first, Point second);
Point getMassCenter(Triangle triangle);

float getDistanceTriangleToTriangle(Triangle ABC, Triangle ABC_);

#endif