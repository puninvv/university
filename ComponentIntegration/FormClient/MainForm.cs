using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using IntegrationLib;

namespace FormClient
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private bool GetInputs(ref String function, ref double from, ref double to, ref double error, ref char variable)
        {
            try
            {
                function = TextFunction.Text;
                from = Double.Parse(TextFrom.Text);
                to = Double.Parse(TextTo.Text);
                error = Double.Parse(TextError.Text);
                variable = TextVariable.Text[0];
            }
            catch (Exception e)
            {
                MessageBox.Show("Проверьте корректность введенных данных!\nСкорее всего вместо запятой поставили точку.");
                return false;
            }
            return true;
        }

        private void Integrate_Click(object sender, EventArgs e)
        {
            String function="";
            double from = 0;
            double to = 0;
            double error = 0;
            Char variable = ' ';
            if (!GetInputs(ref function, ref from, ref to, ref error, ref variable))
                return;

            Parser parser = new Parser();
            Parser.IManyArgumentsFunction f_many_arguments = parser.ParseString(function);

            Dictionary<char, double> args = new Dictionary<char, double>();
            args.Add(variable, 0);

            FunctionContainer functionContainer = new FunctionContainer();
            FunctionContainer.IFunction f_one_argument = functionContainer.CreateOneArgumentFunction(f_many_arguments.GetValue, args, variable);

            Derivator derivator = new Derivator();
            if (RadioRectangle.Checked)
            {
                Derivator.IManyArgumentsDerivative d_2_many_arguments = derivator.Derivate(function, variable, 2);
                FunctionContainer.IFunction d_2_one_argument = functionContainer.CreateOneArgumentFunction(d_2_many_arguments.GetValue, args, variable);

                IntegrationLib.Integrators.RectanglesIntegrator rectanglesIntegrator = new IntegrationLib.Integrators.RectanglesIntegrator();
                MessageBox.Show(rectanglesIntegrator.Integrate(f_one_argument.GetValue, d_2_one_argument.GetValue, from, to, error).ToString());
            }
            else
            if (RadioTrapeze.Checked)
            {
                Derivator.IManyArgumentsDerivative d_2_many_arguments = derivator.Derivate(function, variable, 2);
                FunctionContainer.IFunction d_2_one_argument = functionContainer.CreateOneArgumentFunction(d_2_many_arguments.GetValue, args, variable);

                IntegrationLib.Integrators.TrapezeIntegrator trapezeIntegrator = new IntegrationLib.Integrators.TrapezeIntegrator();
                MessageBox.Show(trapezeIntegrator.Integrate(f_one_argument.GetValue, d_2_one_argument.GetValue, from, to, error).ToString());
            }
            else
            if (RadioSimpsons.Checked)
            {
                Derivator.IManyArgumentsDerivative d_4_many_arguments = derivator.Derivate(function, variable, 4);
                FunctionContainer.IFunction d_4_one_argument = functionContainer.CreateOneArgumentFunction(d_4_many_arguments.GetValue, args, variable);

                IntegrationLib.Integrators.SimpsonsIntegrator simpsonsIntegrator = new IntegrationLib.Integrators.SimpsonsIntegrator();
                MessageBox.Show(simpsonsIntegrator.Integrate(f_one_argument.GetValue, d_4_one_argument.GetValue, from, to, error).ToString());
            }
            else
            {
                Derivator.IManyArgumentsDerivative d_2_many_arguments = derivator.Derivate(function, variable, 2);
                Derivator.IManyArgumentsDerivative d_4_many_arguments = derivator.Derivate(function, variable, 4);

                FunctionContainer.IFunction d_2_one_argument = functionContainer.CreateOneArgumentFunction(d_2_many_arguments.GetValue, args, variable);
                FunctionContainer.IFunction d_4_one_argument = functionContainer.CreateOneArgumentFunction(d_4_many_arguments.GetValue, args, variable);

                IntegrationLib.Integrators.RectanglesIntegrator rectanglesIntegrator = new IntegrationLib.Integrators.RectanglesIntegrator();
                IntegrationLib.Integrators.TrapezeIntegrator trapezeIntegrator = new IntegrationLib.Integrators.TrapezeIntegrator();
                IntegrationLib.Integrators.SimpsonsIntegrator simpsonsIntegrator = new IntegrationLib.Integrators.SimpsonsIntegrator();

                Container cnt = new Container();
                cnt.Add(rectanglesIntegrator, "Rectangles");
                cnt.Add(trapezeIntegrator, "Trapeze");
                cnt.Add(simpsonsIntegrator, "Simpsons");

                String result = "Прямоугольники:\t" + rectanglesIntegrator.Integrate(f_one_argument.GetValue, d_2_one_argument.GetValue, from, to, error).ToString() + "\n";
                result += "Трапеции:\t" + trapezeIntegrator.Integrate(f_one_argument.GetValue, d_2_one_argument.GetValue, from, to, error).ToString() + "\n";
                result += "Симпсон:\t\t" + simpsonsIntegrator.Integrate(f_one_argument.GetValue, d_2_one_argument.GetValue, from, to, error).ToString() + "\n";

                MessageBox.Show(result);
            }
        }
    }
}
