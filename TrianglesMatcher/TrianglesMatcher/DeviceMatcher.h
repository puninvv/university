#pragma once
#ifndef DeviceMatcherHEADER
#define DeviceMatcherHEADER

#include "Structures.h"

/*
	Всё, что касается точек:
		1) нахождение расстояния (корень не извлекается)
		2) нахождение сдвинутой точки на dx, dy
		3) нахождение точки, повернутой относительно начала координат на угол phi,
			в функцию нужно передать исходную точку, cos(phi), sin(phi) - их лучше вычислить
			в вызывающей функции (чтобы не считать несколько раз синус и косинус)
*/
float countDistanceBetweenPointsDEVICE(Point first, Point second);
Point countMovedPointDEVICE(Point p, float dx, float dy);
Point countRotatedPointDEVICE(Point p, float cos_phi, float sin_phi);

/*
	Всё, что касается трегольников:
		1) вычисление расстояния между треугольниками -
			вычисляются расстояния между соответсвующими вершинами, корень не извлекается
		2) нахождение центра масс
		3) вычисление передвинутого треугольника на dx, dy
		4) вычисление повернутого относительно начала координат треугольника на угол phi;
			как и с точками, нужно передать sin, cos (phi)
		5) вычисление преобразованного треугольника:
			сначала сдвигаем в начало координат, затем крутим, затем сдвигаем
*/
float countDistanceBetweenTrianglesDEVICE(Triangle* first, Triangle* second);
Point countTriangleMassCenterDEVICE(Triangle* ABC);
Triangle countMovedTriangleDEVICE(Triangle* ABC, float dx, float dy);
Triangle countRotatedTriangleDEVICE(Triangle* ABC, float cos_phi, float sin_phi);
Triangle countTransformedTriangleDEVICE(Triangle* ABC, Transformation t);

/*
	Треугольники должны быть ориентированы (!)ПО часовой стрелке.

	Теперь про оптимальное расположение:
		1) вычисление оптимального угла:
			* в качестве минимизируемой функции была взята сумма квадратов соответствующих координат вершин у треугольников
				для точки А треугольника ABC будет выглядеть следующим образом:
				(Ax - A_x * cos_phi + A_y sin_phi)^2 + (Ay - A_x sin_phi - A_y cos_phi)^2, где
				ABC - "цель", ABC_ - "то, что двигаем"
				тогда F = сумма подобных выражений для A, B, C. Функция зависит (!)лишь от phi, но зависит нелинейно
			* чтобы найти её минимум, необходимо найти нули дифференциала.
				dF = 2 (( Ax A_y - Ay A_x) cos_phi + (Ax A_x + Ay A_y) sin_phi) + ...
			* дифференциал - нелинейное уравнение относительно phi, корни будем искать методом ньютона, на отрезке
				[-pi, pi]
				для этого найдем второй дифференциал:
				dF^2 = 2 ((Ax A_x + Ay A_y) cos_phi - ( Ax A_y - Ay A_x) sin_phi) + ...
			* обозначим:
				sProd = Ax A_x + Ay A_y + Bx B_x + By B_y + Cx C_x + Cy C_y (сумма скалярных произведений)
				vProd = Ax A_y - Ay A_x + Bx B_y - By B_x + Cx C_y - Cy C_x (сумму "векторных" произведений)
			* тогда:
				dF = 2 ( sin_phi sProd + cos_phi vProd )
				dF^2 = 2 (cos_phi sProd - sin_phi vProd )
				корни ищем через несколько начальных приближений - делим окружность на n частей
				sProd, vProd нужно вычислить всего 1 раз - в самом начале, после переноса треугольников к началу координат
			* profit!
		2) вычисление оптимального преобразования: делится на две функции, для ABC, и в общем
			* для ABC - переносим треугольники в начало, делим окружность на равные части (выбираем начальные приближения),
				вычисляем оптимальный угол, крутим треугольник, замеряем расстояние, если меньше исходного - сохраняем результат
			* все тоже самое, но ещё для BCA, CAB
*/
float countOptimumlPhiDEVICE(float phi0, float sProd, float vProd, int maxIterations, float e);
TransformationWithDistance findOptimumTransformationABCDEVICE(Triangle* ABC_, Triangle* ABC, float e, int maxIterations, int parts);
TransformationWithDistance findOptimumTransformationDEVICE(Triangle* ABC_, Triangle* ABC, float e, int maxIterations, int parts);
#endif