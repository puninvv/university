using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Kursach
{
    class mySection
    {
        private myVertex left;
        public myVertex Left
        {
            get { return new myVertex(left); }
        }
        private myVertex right;
        public myVertex Right
        {
            get { return new myVertex(right); }
        }

        public mySection(myVertex Left, myVertex Right) 
        {
            this.left = new myVertex(Left);
            this.right = new myVertex(Right);
        }

        public bool Contain(myVertex vertex)
        {
            if (right.X != left.X && right.Y != left.Y) 
            {
                double t_x = (vertex.X - left.X) / (right.X - left.X);
                double t_y = (vertex.Y - left.Y) / (right.Y - left.Y);
                if (Math.Abs(t_x - t_y) < 0.001 && t_x >= 0 && t_x <= 1 && t_y>=0 && t_y <=1)
                    return true;
                else
                    return false;
            }
            else
                if (right.X == left.X && right.Y != left.Y)
                {
                    if (vertex.X == right.X)
                    {
                        if ((vertex.Y >= left.Y && vertex.Y <= right.Y) || (vertex.Y <= left.Y && vertex.Y >= right.Y))
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else
                if (right.X != left.X && right.Y == left.Y)
                {
                    if (vertex.Y == right.Y)
                    {
                        if ((vertex.X >= left.X && vertex.X <= right.X) || (vertex.X <= left.X && vertex.X >= right.X))
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                }
            
            return false;
        }
        public myVertex Intersect(mySection section) 
        {

            myVertex result = null;
            //если вектора не коллинеарны - просто найдем координаты точки пересечения
            double dx = right.X - left.X;
            double dy = right.Y - left.Y;
            double sdx = section.right.X - section.left.X;
            double sdy = section.right.Y - section.left.Y;
            //проверяем вектора на коллинеарность
            if (dx * sdy - dy * sdx != 0)
            {
                /* 
                 * если они не коллинеарны, тогда точки пересечения отрезков либо нет, либо она одна
                 * 
                 * x_l + a * dx = s_x_l + b * sdx       | * sdy
                 * y_l + a * dy = s_y_l + b * sdy       | * sdx
                 * 
                 * sdy*x_l + sdy*a*dx = sdy*s_x_l + sdy*b*sdx   (1)
                 * sdx*y_l + sdx*a*dy = sdx*s_y_l + sdx*b*sdy   (2)
                 * 
                 * (1)-(2) = sdy*(x_l-y_l) + a*(sdy*dx - sdx*dy) = sdy*s_x_l - sdx*s_y_l;
                 * a = ( sdy*s_x_l - sdx*s_y_l - sdy*(x_l-y_l) )/(sdy*dx - sdx*dy);
                 * таким образом вычислим параметр а
                 * 
                 */
                double a = (sdy * section.left.X - sdx * section.left.Y - (sdy * left.X - sdx * left.Y)) / (sdy * dx - sdx * dy);

                /* 
                 * x_l + a * dx = s_x_l + b * sdx       | * dy
                 * y_l + a * dy = s_y_l + b * sdy       | * dx
                 * 
                 * dy*x_l + dy*a*dx = dy*s_x_l + dy*b*sdx   (1)
                 * dx*y_l + dx*a*dy = dx*s_y_l + dx*b*sdy   (2)
                 * 
                 * (1)-(2) = dy*x_l - dx*y_l = b * (dy*sdx - dx*sdy) + (dy*s_x_l - dx*s_y_l)
                 * b = (dy*x_l - dx*y_l - (dy*s_x_l - dx*s_y_l))/(dy*sdx - dx*sdy)
                 * таким образом вычислим параметр b
                 */
                double b = (dy * left.X - dx * left.Y - (dy * section.left.X - dx * section.left.Y)) / (dy * sdx - dx * sdy);
                //если оба параметра лежат на [0;1], тогда точка пересечения отрезков - лежит на отрезков (!)
                if (a >= 0 && a <= 1 && b >= 0 && b <= 1)
                    result = new myVertex(left.X + a * dx, left.Y + a * dy);
            }
            else
            {
                /*
                 * теперь рассмоотрим случай с коллинеарными векторами
                 * 1) dx != 0
                 * 2) dy != 0
                 * 3) а если и то и то 0, тогда это не отрезок, а точка - таких в данном случае сработает алгоритм заполнения полигона
                 *
                 * суть метода:
                 * найдем какая точка из концов отсекающего отрезка наиболее приближена к правой границе со стороны левой
                 * она и будет результатом (если конечно она попадает на [0;1]
                 */
                if (dx != 0)
                {
                    double t_left = (section.left.X - left.X) / (dx);
                    double t_right = (section.right.X - left.X) / (dx);
                    double result_t = -1;
                    if (t_left <= 1 && t_left > result_t) result_t = t_left;
                    if (t_right <= 1 && t_right > result_t) result_t = t_right;
                    //теперь проверим, а точно ли точка лежит на отрезке
                    if (result_t != -1 && (left.Y + result_t * dy == section.left.Y || left.Y + t_right * dy == section.right.Y))
                        result = new myVertex(left.X + result_t * dx, left.Y + result_t * dy);
                }
                else
                    if (dy != 0)
                    {
                        //всё то же, что и в предыдущем пункте, но с dy
                        double t_left = (section.left.Y - left.Y) / (dy);
                        double t_right = (section.right.Y - left.Y) / (dy);
                        double result_t = -1;
                        if (t_left <= 1 && t_left > result_t) result_t = t_left;
                        if (t_right <= 1 && t_right > result_t) result_t = t_right;
                        //теперь проверим, а точно ли точка лежит на отрезке
                        if (result_t != -1 && (left.X + result_t * dx == section.left.X || left.X + t_right * dx == section.right.X))
                            result = new myVertex(left.X + result_t * dx, left.Y + result_t * dy);
                    }
                    else
                        throw new Exception("Ошибка при поиске пересечения отрезков:\ndx == dy == 0");
            }

            return result;
        }
        public void Paint(Graphics graphics, Pen pen) 
        {
            graphics.DrawLine(pen, left.ToPoint(), right.ToPoint());
        }
    }
}
