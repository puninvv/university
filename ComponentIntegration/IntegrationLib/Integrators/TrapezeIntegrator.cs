using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace IntegrationLib.Integrators
{
    public class TrapezeIntegrator : Component
    {
        private double CountStep(double from, double to, double err, double d2Max)
        {
            return Math.Sqrt(12 * err / ((to - from) * d2Max))/64;
        }

        private double GetMaxValueOf(Func<double, double> function, double from, double to)
        {
            double result = 0;
            double now = from;
            while (now <= to)
            {
                double d = function(now);
                if (!Double.IsNaN(d) && d > result)
                    result = d;
                now += 0.01;
            }
            return result;
        }

        public double Integrate(Func<double, double> function, Func<double, double> d2, double from, double to, double err)
        {
            double result = 0;

            double h = CountStep(from, to, err, GetMaxValueOf(d2, from, to));

            double now = from;
            while (now < to)
            {
                result += (function(now) + function(now + h)) / 2;
                now += h;
            }

            return result * h;
        }

        public double Integrate(Func<double, double> function, double d2Max, double from, double to, double err)
        {
            double result = 0;

            double h = CountStep(from, to, err, d2Max);

            double now = from;
            while (now < to)
            {
                result += (function(now) + function(now+ h))/2;
                now += h;
            }

            return result * h;
        }

        public double Integrate(double[] x, double[] y)
        {
            double result = 0;

            for (int i = 0; i < x.Length - 1; i++)
                result += (y[i+1]-y[i]) * (x[i + 1] - x[i]) / 2;

            return result;
        }
    }
}
