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
                if (IsSign(expression[i]))
                {
                    currentSign = GetCurrentSign(expression[i]);
                    continue;
                }

                if (decimal.TryParse(expression[i].ToString(), out var num))
                {
                    i++;
                    var number = new StringBuilder();
                    number.Append(num);
                    for (; i < expression.Length; i++)
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

                        currentSign = Sign.Positive;
                    }
                }
            }

            return (answer, expression);
        }

        private Sign GetCurrentSign(char expression)
        {
            switch (expression)
            {
                case '+':
                    return Sign.Positive;
                case '-':
                    return Sign.Negative;
                default:
                    return Sign.Positive;
            }

        }

        private bool IsSign(char expression)
        {
            return expression == '+' || expression == '-';
        }

        private enum Sign
        {
            Positive,
            Negative
        }
    }
}
