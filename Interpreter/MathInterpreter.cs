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
            while (expression.Length > 0)
            {
                if (IsSign(expression[0]))
                {
                    currentSign = GetCurrentSign(expression[0]);
                    expression = expression.Substring(1);
                    continue;
                }

                // For now, assume every character is either a sign or number
                decimal nextNum;
                (nextNum, expression) = GetNextNumber(expression);
                answer = GetNewSum(answer, nextNum, currentSign);
            }

            return (answer, expression);
        }

        private Sign GetCurrentSign(char sign)
        {
            switch (sign)
            {
                case '+':
                    return Sign.Positive;
                case '-':
                    return Sign.Negative;
                default:
                    return Sign.Positive;
            }

        }

        private bool IsSign(char character)
        {
            return character == '+' || character == '-';
        }

        private (decimal, string) GetNextNumber(string expression)
        {
            var numberString = string.Empty;
            if (decimal.TryParse(expression[0].ToString(), out var num))
            {
                var number = new StringBuilder();
                number.Append(num);
                expression = expression.Substring(1);
                while (expression.Length > 0)
                {
                    if (!decimal.TryParse(expression[0].ToString(), out var otherNum))
                    {
                        break;
                    }
                    number.Append(otherNum);
                    expression = expression.Substring(1);
                }
                numberString = number.ToString();
            }
            
            if (!decimal.TryParse(numberString, out var newNumber))
            {
                return (0, expression);
            }

            return (newNumber, expression);
        }

        private decimal GetNewSum(decimal currentSum, decimal newNumber, Sign currentSign)
        {
            if (currentSign == Sign.Positive)
            {
                return currentSum += newNumber;
            }

            if (currentSign == Sign.Negative)
            {
                return currentSum -= newNumber;
            }

            return 0;
        }

        private enum Sign
        {
            Positive,
            Negative
        }
    }
}
