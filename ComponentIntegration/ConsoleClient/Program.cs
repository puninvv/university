using System;
using System.Collections.Generic;
using Parser = IntegrationLib.Parser;
using Derivator = IntegrationLib.Derivator;
using FunctionContainer = IntegrationLib.FunctionContainer;
using Integrators = IntegrationLib.Integrators;

namespace ConsoleClient
{
    class Program
    {
        public static void Main(String[] args)
        {
            String input = "ln(5^2*(sin(x))^2+6^2*(cos(x))^2)";

            Parser parser = new Parser();

            Parser.IManyArgumentsFunction f = parser.ParseString(input);

            Dictionary<Char, Double> arguments = new Dictionary<char, double>();
            arguments.Add('x', 0+0.0001);           

            Derivator derivator = new Derivator();

            Derivator.IManyArgumentsDerivative d1 = derivator.Derivate(input, 'x');
            Derivator.IManyArgumentsDerivative d2 = derivator.Derivate(input, 'x', 2);
            Derivator.IManyArgumentsDerivative d3 = derivator.Derivate(input, 'x', 3);
            Derivator.IManyArgumentsDerivative d4 = derivator.Derivate(input, 'x', 4);

            Console.WriteLine("f: " + f.GetValue(arguments));
            Console.WriteLine("d1: " + d1.GetValue(arguments));
            Console.WriteLine("d2: " + d2.GetValue(arguments));
            Console.WriteLine("d3: " + d3.GetValue(arguments));
            Console.WriteLine("d4: " + d4.GetValue(arguments));

            FunctionContainer f_container = new FunctionContainer();
            FunctionContainer.IFunction f_onearg = f_container.CreateOneArgumentFunction(f.GetValue, arguments, 'x');
            FunctionContainer.IFunction d2_onearg = f_container.CreateOneArgumentFunction(d2.GetValue, arguments, 'x');

            double from = 0;
            double to = 1.57;
            double err = 0.0001;

            Integrators.RectanglesIntegrator Integrator = new Integrators.RectanglesIntegrator();
            Console.WriteLine("error: " + err + "\tresult: " + Integrator.Integrate(f_onearg.GetValue, d2_onearg.GetValue, from, to, err));

            Integrators.TrapezeIntegrator TrpIntegrator = new IntegrationLib.Integrators.TrapezeIntegrator();
            Console.WriteLine("error: " + err + "\tresult: " + TrpIntegrator.Integrate(f_onearg.GetValue, d2_onearg.GetValue, from, to, err));

            Integrators.SimpsonsIntegrator SmpIntegrator = new Integrators.SimpsonsIntegrator();
            Console.WriteLine("error: " + err + "\tresult: " + SmpIntegrator.Integrate(f_onearg.GetValue, d2_onearg.GetValue, from, to, err));
        }
    }
}