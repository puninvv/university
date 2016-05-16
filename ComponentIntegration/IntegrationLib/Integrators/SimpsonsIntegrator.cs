using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace IntegrationLib.Integrators
{
    public class SimpsonsIntegrator: Component
    {
        private int Count2N(double from, double to, double err, double d4Max)
        {
            return (int)Math.Round(Math.Sqrt(Math.Sqrt((to - from)* (to - from) * (to - from) * (to - from) * (to - from) * d4Max / (2880* err)))) + 16000;
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

            int n2 = Count2N(from, to, err, GetMaxValueOf(d4, from, to));
            if (n2 % 2 == 1)
                n2++;

            double[] y = new double[n2 + 1];
            for (int i = 0; i <= n2; i++)
            {
                double x_i = from + ((to - from) / n2) * i;
                y[i] = function(x_i);
            }
            

            for (int i = 1; i <= n2; i++)
            {
                if (i % 2 == 0)
                    result += 2 * y[i];
                else
                    result += 4 * y[i];
            }

            result += y[0] + y[n2];

            return result * ((to - from) / n2) / 3;
        }

        public double Integrate(Func<double, double> function, double d4Max, double from, double to, double err)
        {
            double result = 0;

            int n2 = Count2N(from, to, err, d4Max);
            if (n2 % 2 == 1)
                n2++;

            n2 = 16000;


            double[] y = new double[n2 + 1];
            for (int i = 0; i <= n2; i++)
            {
                double x_i = from + ((to - from) / n2) * i;
                y[i] = function(x_i);
            }


            for (int i = 1; i <= n2; i++)
            {
                if (i % 2 == 0)
                    result += 2 * y[i];
                else
                    result += 4 * y[i];
            }

            result += y[0] + y[n2];

            return result * ((to - from) / n2) / 3;
        }

        public double Integrate(double[] y, double h)
        {
            double result = 0;

            result += y[0] + y[y.Length - 1];

            for (int i = 1; i <= y.Length - 2; i++)
            {
                if (i % 2 == 0)
                    result += 2 * y[i];
                else
                    result += 4 * y[i];
            }

            return result * h / 3;
        }
    }
}
