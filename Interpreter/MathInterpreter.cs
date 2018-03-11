using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Interpreter
{
    public class MathInterpreter
    {
        private static readonly List<char> MathOperators = new List<char>
        {
            '*',
            '/',
            '-',
            '+'
        };

        public decimal GetAnswer(string expression)
        {
            var cleanedExpression = Regex.Replace(expression, @"\s+", "");
            var resolvedExpression = ResolveMultDiv(cleanedExpression);
            var (result, evaluatedExpression) = Evaluate(resolvedExpression);

            return result;
        }

        private string SimplifyExpression(string expression)
        {
            var needsSimplification = false;
            var newExpression = string.Copy(expression);
            do
            {
                needsSimplification = false;

                // Look for first parenthetical and resolve it
                var startExpressionToResolve = 0;
                var endExpressionToResolve = 0;
                for (int i = 0; i < expression.Length; i++)
                {
                    if (expression[i] == '(')
                    {
                        startExpressionToResolve = i;
                        needsSimplification = true;
                    }

                    if (expression[i] == ')')
                    {
                        endExpressionToResolve = i;
                        needsSimplification = true;
                        break;
                    }
                }

            } while (needsSimplification);

            return expression;
        }

        private string ResolveParens(string expression)
        {
            return expression;
        }

        private string ResolveMultDiv(string expression)
        {
            var numbers = new List<decimal>();
            var newString = new StringBuilder();
            var currentSign = Sign.Positive;
            while (expression.Length > 0)
            {
                decimal firstNum;
                decimal secondNum;

                if (expression[0] == '+' || expression[0] == '-')
                {
                    currentSign = GetCurrentSign(expression[0]);
                    expression = expression.Substring(1);
                }

                if (expression[0] == '*' || expression[0] == '/')
                {
                    if (expression[0] == '*')
                    {
                        expression = expression.Substring(1);
                        (firstNum, expression) = GetNextNumber(expression);
                        numbers[numbers.Count - 1] *= firstNum;
                        continue;
                    }

                    if (expression[0] == '/')
                    {
                        expression = expression.Substring(1);
                        (firstNum, expression) = GetNextNumber(expression);
                        numbers[numbers.Count - 1] /= firstNum;
                        continue;
                    }
                }
                (firstNum, expression) = GetNextNumber(expression);
                if (currentSign == Sign.Negative)
                {
                    firstNum *= -1;
                    currentSign = Sign.Positive;
                }

                numbers.Add(firstNum);
                if (expression.Length == 0)
                {
                    foreach (var number in numbers)
                    {
                        if (number >= 0)
                        {
                            newString.Append("+");
                        }

                        newString.Append(number);
                    }
                    return newString.ToString();
                }

                var op = expression[0];
                if (op == '-')
                {
                    currentSign = Sign.Negative;
                }
                expression = expression.Substring(1);
                (secondNum, expression) = GetNextNumber(expression);
                if (op == '*' || op == '/')
                {
                    if (op == '*')
                    {
                        numbers[numbers.Count - 1] *= secondNum;
                    }
                    if (op == '/')
                    {
                        numbers[numbers.Count - 1] /= secondNum;
                    }
                }
                else
                {
                    if (currentSign == Sign.Negative)
                    {
                        secondNum *= -1;
                    }
                    numbers.Add(secondNum);
                }
            }
            foreach (var number in numbers)
            {
                if (number >= 0)
                {
                    newString.Append("+");
                }

                newString.Append(number);
            }
            return newString.ToString();
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

                //if (MustGetNextNumber(expression))
                //{
                //    decimal subsequentNextNum;

                //    (subsequentNextNum, expression) = Evaluate(expression);
                //    nextNum = +subsequentNextNum;
                //}

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

        //private bool MustGetNextNumber(string expression)
        //{
        //    return expression.Length > 0 && PrecedentialOperators.Contains(expression[0]);
        //}

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
