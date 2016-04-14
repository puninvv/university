using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace IntegrationLib
{
    public class FunctionContainer : Component
    {
        public interface IFunction
        {
            double GetValue(double x);
        }

        private class FunctionContainerResult : IFunction
        {
            private Dictionary<Char, Double> Arguments
            {
                get;
                set;
            }

            private Char Variable
            {
                get;
                set;
            }

            private Func<Dictionary<Char, Double>, Double> Function
            {
                get;
                set;
            }

            public FunctionContainerResult(Func<Dictionary<Char, Double>, Double> function ,Dictionary<Char, Double> arguments, Char variable)
            {
                Arguments = arguments;
                Variable = variable;
                Function = function;
            }

            public double GetValue(double x)
            {
                Dictionary<Char, double> args = new Dictionary<char, double>(Arguments);
                args.Remove(Variable);
                args.Add(Variable, x);
                return Function(args);
            }
        }

        public IFunction CreateOneArgumentFunction(Func<Dictionary<Char, Double>, Double> function, Dictionary<Char, Double> arguments, Char variable)
        {
            return new FunctionContainerResult(function, arguments, variable);
        }
    }
}
