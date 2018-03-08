using System.Text;
using System.Text.RegularExpressions;

namespace Interpreter
{
    public class MathInterpreter
    {
        public decimal GetAnswer(string expression)
        {
            var cleanedExpression = Regex.Replace(expression, @"\s+", "");
            var (result, evaluatedExpression) = Evaluate(cleanedExpression);

            return result;
        }

        private (decimal, string) Evaluate(string expression)
        {
            var currentSign = Sign.Positive;
            var answer = 0M;
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '+')
                {
                    currentSign = Sign.Positive;
                    continue;
                }

                if (expression[i] == '-')
                {
                    currentSign = Sign.Negative;
                    continue;
                }


                if (decimal.TryParse(expression[i].ToString(), out var num))
                {
                    var number = new StringBuilder();
                    number.Append(num);
                    for (; i < expression.Length; ++i)
                    {
                        if (!decimal.TryParse(expression[i].ToString(), out var otherNum))
                        {
                            break;
                        }
                        number.Append(otherNum);
                    }

                    var numberString = number.ToString();

                    if(decimal.TryParse(numberString, out var newNum))
                    {
                        if (currentSign == Sign.Positive)
                        {
                            answer += newNum;
                        }

                        if (currentSign == Sign.Negative)
                        {
                            answer -= newNum;
                        }
                    }
                }
            }

            return (answer, expression);
        }

        private enum Sign
        {
            Positive,
            Negative
        }
    }
}
