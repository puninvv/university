using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace IntegrationLib.Integrators
{
    public class SimpsonsIntegrator: Component
    {
        private double CountStep(double from, double to, double err, double d4Max)
        {
            return Math.Sqrt(Math.Sqrt(180 * err / ((to - from) * (d4Max+10))))/64;
        }

        private double GetMaxValueOf(Func<double, double> function, double from, double to)
        {
            double result = 0;
            double now = from;
            while (now <= to)
            {
                double d = Math.Abs(function(now));
                if (!Double.IsNaN(d) && d > result)
                    result = d;
                now += 0.001;
            }
            return result;
        }

        public double Integrate(Func<double, double> function, Func<double, double> d4, double from, double to, double err)
        {
            double result = 0;

            double h = CountStep(from, to, err, GetMaxValueOf(d4, from, to));

            int n = (int)((to - from) / h);

            double[] f_i = new double[2 * n - 1];
            for (int i = 0; i < 2 * n - 1; i++)
                f_i[i] = function(from + i * (h / 2));


            for (int i = 0; i < f_i.Length - 1;)
            {
                result += (f_i[i] + 4 * f_i[i +1] + f_i[i + 2]);
                i += 2;
            }

            return h* result/6;
        }

        public double Integrate(Func<double, double> function, double d4Max, double from, double to, double err)
        {
            double result = 0;

            double h = CountStep(from, to, err, d4Max);
            int n = (int)((to - from) / h);

            double[] f_i = new double[2 * n - 1];
            for (int i = 0; i < 2 * n - 1; i++)
                f_i[i] = function(from + i * (h / 2));


            for (int i = 0; i < f_i.Length-1;)
            {
                result += (f_i[i]+ 4* f_i[i+1]+f_i[i+2]);
                i += 2;
            }

            return h*result/6;
        }

        public double Integrate(double[] x, double[] y)
        {
            double result = 0;

            for (int i = 0; i < x.Length - 2; i++)
                result += (x[i + 2] - x[i]) * (y[i] + 4 * y[i + 1] + y[i + 2]) / 6;

            return result;
        }
    }
}
