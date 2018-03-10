using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Interpreter
{
    public class MathInterpreter
    {
        private static readonly List<char> PrecedentialOperators = new List<char>
        {
            '*',
            '/'
        };

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

                if (MustGetNextNumber(expression))
                {
                    decimal subsequentNextNum;

                    (subsequentNextNum, expression) = Evaluate(expression);
                    nextNum = +subsequentNextNum;
                }

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
            var numberString = new string(expression.TakeWhile(c => char.IsDigit(c) || c == '.').ToArray());
            expression = expression.Substring(numberString.Length);

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

        private bool MustGetNextNumber(string expression)
        {
            return expression.Length > 0 && PrecedentialOperators.Contains(expression[0]);
        }

        private (Operator, string) GetNextOperator(string expression)
        {
            Operator op;
            if (expression[0] == '*')
            {
                op = Operator.Multiply;
            }
            else
            {
                op = Operator.Divide;
            }

            expression = expression.Substring(1);
            return (op, expression);
        }

        private enum Sign
        {
            Positive,
            Negative
        }

        private enum Operator
        {
            Multiply,
            Divide
        }
    }
}
