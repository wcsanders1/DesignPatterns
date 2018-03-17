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
            var simplifiedExpression = SimplifyExpression(cleanedExpression);
            var resolvedExpression = ResolveMultDiv(simplifiedExpression);
            var (result, unevaluatedExpression) = Evaluate(resolvedExpression);

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
                var start = 0;
                var end = 0;
                for (int i = 0; i < expression.Length; i++)
                {
                    if (expression[i] == '(')
                    {
                        start = i;
                        needsSimplification = true;
                    }

                    if (expression[i] == ')')
                    {
                        end = i;
                        needsSimplification = true;
                        break;
                    }
                }

                if (needsSimplification)
                {
                    var subExpression = expression.Substring(start, end - start + 1);
                    var strippedSubExpression = subExpression.Replace("(", "").Replace(")", "");
                    var resolvedSubExpression = ResolveMultDiv(strippedSubExpression);
                    var (solvedSubExpression, _) = Evaluate(resolvedSubExpression);
                    expression = expression.Replace(subExpression, solvedSubExpression.ToString());
                }
            } while (needsSimplification);

            return expression;
        }

        private string ResolveMultDiv(string expression)
        {
            var numbers = new List<decimal>();
            var newString = new StringBuilder();
            var currentSign = Sign.Positive;
            var lastChar = Syntax.Number;
            while (expression.Length > 0)
            {
                decimal firstNum;
                decimal secondNum;

                if (IsSign(expression[0]))
                {
                    currentSign = GetCurrentSign(expression[0], currentSign, lastChar);
                    lastChar = Syntax.Sign;
                    expression = expression.Substring(1);
                    continue;
                }

                if (expression[0] == '*')
                {
                    expression = expression.Substring(1);
                    if (IsSign(expression[0]))
                    {
                        currentSign = GetCurrentSign(expression[0], currentSign, lastChar);
                        expression = expression.Substring(1);
                    }

                    (firstNum, expression) = GetNextNumber(expression);

                    if (currentSign == Sign.Negative)
                    {
                        firstNum *= -1;
                    }

                    numbers[numbers.Count - 1] *= firstNum;
                    lastChar = Syntax.Number;
                    continue;
                }

                if (expression[0] == '/')
                {
                    expression = expression.Substring(1);
                    if (IsSign(expression[0]))
                    {
                        currentSign = GetCurrentSign(expression[0], currentSign, lastChar);
                        expression = expression.Substring(1);
                    }

                    (firstNum, expression) = GetNextNumber(expression);

                    if (currentSign == Sign.Negative)
                    {
                        firstNum *= -1;
                    }

                    numbers[numbers.Count - 1] /= firstNum;
                    lastChar = Syntax.Number;
                    continue;
                }
                
                (firstNum, expression) = GetNextNumber(expression);
                if (currentSign == Sign.Negative)
                {
                    firstNum *= -1;
                    currentSign = Sign.Positive;
                    lastChar = Syntax.Number;
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
                    currentSign = GetCurrentSign('-', currentSign, lastChar);
                    expression = expression.Substring(1);
                    lastChar = Syntax.Sign;
                    continue;
                }
                expression = expression.Substring(1);

                if (IsSign(expression[0]))
                {
                    currentSign = GetCurrentSign(expression[0], currentSign, lastChar);
                    expression = expression.Substring(1);
                    lastChar = Syntax.Sign;
                }

                (secondNum, expression) = GetNextNumber(expression);
                lastChar = Syntax.Number;
                if (op == '*' || op == '/')
                {
                    if (currentSign == Sign.Negative)
                    {
                        secondNum *= -1;
                    }

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
            var lastChar = Syntax.Number;

            while (expression.Length > 0)
            {
                if (IsSign(expression[0]))
                {
                    currentSign = GetCurrentSign(expression[0], currentSign, lastChar);
                    expression = expression.Substring(1);
                    lastChar = Syntax.Sign;
                    continue;
                }

                decimal nextNum;
                (nextNum, expression) = GetNextNumber(expression);
                lastChar = Syntax.Number;
                answer = GetNewSum(answer, nextNum, currentSign);
            }

            return (answer, expression);
        }

        private Sign GetCurrentSign(char sign, Sign currentSign, Syntax lastChar)
        {
            switch (sign)
            {
                case '+':
                    return Sign.Positive;
                case '-':
                    if (currentSign == Sign.Negative && lastChar != Syntax.Number)
                    {
                        return Sign.Positive;
                    }
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

        private enum Syntax
        {
            Number,
            Paren,
            Sign,
            Operator
        }
    }
}
