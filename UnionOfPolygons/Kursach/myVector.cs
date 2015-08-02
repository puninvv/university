using System;
using System.Collections.Generic;
using System.Text;

namespace Kursach
{
    class myVector
    {
        private double x;
        public double X
        {
            get { return x; }
        }

        private double y;
        public double Y
        {
            get { return y; }
        }

        public myVector(myVertex start, myVertex end) 
        {
            this.x = end.X - start.X;
            this.y = end.Y - start.Y;
        }
        public double getNorm()
        {
            return Math.Sqrt(x * x + y * y);
        }

        public double getDotProduct(myVector vector) 
        {
            return x * vector.x + y * vector.y;
        }
        public double getVectorProduct(myVector vector)
        {
            return x * vector.y - y * vector.x;
        }
        public double getAngleTo(myVector vector)
        {
            //будем искать угол между текущим вектором и вектором vector, лежащий в интервале (-pi;pi)
            //для этого найдем косинус - через скалярное произведение
            double cos = getDotProduct(vector) / (this.getNorm() * vector.getNorm());
            //и синус - через векторное
            double sin = getVectorProduct(vector) / (this.getNorm() * vector.getNorm());

            //теперь поработаем с косинусом
            //нам нужен сам угол, поэтому нужно взять арккосинус, который вернёт значение [0;pi]
            //ему соответствует два угла (этот), и -(этот)
            double angle_1_cos = Math.Acos(cos);
            double angle_2_cos = -angle_1_cos;

            //теперь поработаем с синусом
            //ему соответствует угол Asin(sin) [-pi/2;pi/2] - если вернёт положител
            double angle_1_sin = Math.Asin(sin);
            double angle_2_sin;
            if (angle_1_sin >= 0)
                angle_2_sin = Math.PI - angle_1_sin;
            else
                angle_2_sin = -Math.PI - angle_1_sin;

            //теперь простейшее сравнение
            if (Math.Abs(angle_1_cos - angle_1_sin) < 0.000001 || Math.Abs(angle_2_cos - angle_1_sin) < 0.000001)
                return angle_1_sin;
            else
                return angle_2_sin;
        }
    }
}
