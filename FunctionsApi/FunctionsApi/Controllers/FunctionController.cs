using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using IntegrationLib;

namespace FunctionsApi.Controllers
{
    public class FunctionController : ApiController
    {
        private const string WHERE = "where";

        // GET api/values/5
        [SwaggerOperation("GetById")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public string Get(string function)
        {
            PrepareFunction(ref function);

            try
            {
                var parsedValues = ParseFunction(function);

                if (parsedValues == null)
                    return "Произошла ошибка при парсинге";

                var clearFunction = parsedValues.Item1;
                var variables = parsedValues.Item2;

                return Evaluate(clearFunction, variables).ToString();
            }
            catch (Exception ex)
            {
                return "Произошла какая-то ошибка, хз что за ошибка, но она произошла";
            }
        }

        private void PrepareFunction(ref string _function)
        {
            _function = _function.Replace("_add_", "+");
            _function = _function.Replace("_min_", "-");
            _function = _function.Replace("_mul_", "*");
            _function = _function.Replace("_div_", "/");
            _function = _function.Replace("_pow_", "^");
            _function = _function.Replace("_dot_", ".");

            _function = _function.ToLowerInvariant();
        }

        private Tuple<string, Dictionary<char, double>> ParseFunction(string _function)
        {
            var resultVariables = new Dictionary<char, double>();

            if (!_function.Contains("where"))
                return new Tuple<string, Dictionary<char, double>>(_function, resultVariables);

            var indexOfWhere = _function.IndexOf(WHERE) + 1;
            var whereLength = WHERE.Length;

            var resultFunction = _function.Substring(0, indexOfWhere - 1);
            var variablesString = _function.Substring(indexOfWhere + 5, _function.Length - indexOfWhere - whereLength);

            var variables = variablesString.Split(',');
            foreach (var item in variables)
            {
                var splittedPair = item.Split('=');
                try
                {
                    resultVariables.Add(splittedPair[0][0], double.Parse(splittedPair[1], System.Globalization.NumberStyles.AllowDecimalPoint | System.Globalization.NumberStyles.AllowLeadingSign, System.Globalization.NumberFormatInfo.InvariantInfo));
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            return new Tuple<string, Dictionary<char, double>>(resultFunction, resultVariables);
        }

        private object Evaluate(string _function, Dictionary<char, double> _variables)
        {
            var parser = new IntegrationLib.Parser();
            Parser.IManyArgumentsFunction resultFunction = null;
            try
            {
                resultFunction = parser.ParseString(_function);
            }
            catch (Exception ex)
            {
                return "Ошибка при парсинге";
            }

            try
            {
                return resultFunction.GetValue(_variables);
            }
            catch (Exception ex)
            {
                return "Ошибка при вычислении значения распарсенного выражения";
            }
        }
    }
}
