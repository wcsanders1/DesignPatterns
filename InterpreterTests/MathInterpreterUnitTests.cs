using Interpreter;
using Xunit;

namespace InterpreterTests
{
    public class MathInterpreterUnitTests
    {
        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedSimpleAdditionExpression()
        {
            const string testExpression = "4 + 5";
            const decimal expectedAnswer = 9;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedTwoDigitAdditionExpression()
        {
            const string testExpression = "40 + 55";
            const decimal expectedAnswer = 95;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedLargeAdditionExpression()
        {
            const string testExpression = "4012 + 550992";
            const decimal expectedAnswer = 555004;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedThreePartAdditionExpression()
        {
            const string testExpression = "4012 + 550992 + 6672";
            const decimal expectedAnswer = 561676;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedSimpleSubtractionExpression()
        {
            const string testExpression = "8 - 3";
            const decimal expectedAnswer = 5;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedLargeSubtractionExpression()
        {
            const string testExpression = "8854201 - 33445";
            const decimal expectedAnswer = 8820756;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedThreePartSubtractionExpression()
        {
            const string testExpression = "8854201 - 33445 - 54";
            const decimal expectedAnswer = 8820702;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedNegativeResultSubtractionExpression()
        {
            const string testExpression = "33445 - 8854201";
            const decimal expectedAnswer = -8820756;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedExpressionWithAdditionAndSubtractionExpression()
        {
            const string testExpression = "8854201 + 4435 - 690";
            const decimal expectedAnswer = 8857946;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedComplexAdditionAndSubstrationExpression()
        {
            const string testExpression = "8854201 + 4435 - 690 - 77744655 + 1 + 1+234   + 656432 - 90 - 90-99+1 +590333";
            const decimal expectedAnswer = -67639986;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedExpressionWithDecimal()
        {
            const string testExpression = "4012.43 + 550992.111";
            const decimal expectedAnswer = 555004.541M;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedComplexExpressionWithDecimal()
        {
            const string testExpression = "4012.43 + 550992.111 - .02 + 324 - 9999999 - 10.00 - 4323 + .009 + 39393";
            const decimal expectedAnswer = -9409610.47M;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedMultiplicationExpression()
        {
            const string testExpression = "5 * 6";
            const decimal expectedAnswer = 30;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedMultiplicationWithAdditionOne()
        {
            const string testExpression = "3 + 5 * 6";
            const decimal expectedAnswer = 33;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedMultiplicationWithAdditionTwo()
        {
            const string testExpression = "3 + 5 * 6 + 3";
            const decimal expectedAnswer = 36;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedMultiplicationWithAdditionThree()
        {
            const string testExpression = "3 + 5 * 6 + 3 * 10";
            const decimal expectedAnswer = 63;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedMultiplicationWithAdditionFour()
        {
            const string testExpression = "3 + 5 * 6 + 3 * 10 + 5 + 5 * 1 + 8.5 - 2";
            const decimal expectedAnswer = 79.5M;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedDivisionExpression()
        {
            const string testExpression = "10 / 2";
            const decimal expectedAnswer = 5;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedDivisionWithDecimalResult()
        {
            const string testExpression = "10 / 4";
            const decimal expectedAnswer = 2.5M;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedDivisionAndSubtraction()
        {
            const string testExpression = "10 / 2 - 1";
            const decimal expectedAnswer = 4;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedDivisionAndMultiplication()
        {
            const string testExpression = "10 / 2 * 5";
            const decimal expectedAnswer = 25;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedMultiplicationWithAdditionAndParen()
        {
            const string testExpression = "(3 + 5) * 6";
            const decimal expectedAnswer = 48;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedNegativeNumberFirst()
        {
            const string testExpression = "-6 + 4";
            const decimal expectedAnswer = -2;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedNegativeMult()
        {
            const string testExpression = "6 - 4 * 5";
            const decimal expectedAnswer = -14;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedNegativeMultWithParen()
        {
            const string testExpression = "6 - (4 * 5)";
            const decimal expectedAnswer = -14;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedDoubleNegativeMultWithParen()
        {
            const string testExpression = "6 - (-4 * 5)";
            const decimal expectedAnswer = 26;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedTripleNegativeMultWithParen()
        {
            const string testExpression = "-6 - (-4 * 5)";
            const decimal expectedAnswer = 14;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedExpression1()
        {
            const string testExpression = "2 * -1";
            const decimal expectedAnswer = -2;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedExpression2()
        {
            const string testExpression = "-6 - (-4 * 5) * -1";
            const decimal expectedAnswer = -26;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedExpression3()
        {
            const string testExpression = "-6 + 20 * -1";
            const decimal expectedAnswer = -26;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedExpression4()
        {
            const string testExpression = "-6 - (-4 * 5) * -1";
            const decimal expectedAnswer = -26;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedExpression5()
        {
            const string testExpression = "-(5 + 5)";
            const decimal expectedAnswer = -10;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedExpression6()
        {
            const string testExpression = "5 * 0";
            const decimal expectedAnswer = 0;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedExpression7()
        {
            const string testExpression = "5 * 0";
            const decimal expectedAnswer = 0;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedExpression8()
        {
            const string testExpression = "4 + 5 - 2 * (6 + 4)";
            const decimal expectedAnswer = -11;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedNestedParenthetical1()
        {
            const string testExpression = "5 * (6 + 2) - (10 * (5 - 3))";
            const decimal expectedAnswer = 20;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedNestedParenthetical2()
        {
            const string testExpression = "5 * ((6 + 2) - (10 * (5 - 3)))";
            const decimal expectedAnswer = -60;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }

        [Fact]
        public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedNestedParenthetical3()
        {
            const string testExpression = "5 * (-1 * ((6 + 2) - (10 * (5 - 3))))";
            const decimal expectedAnswer = 60;

            var sut = new MathInterpreter();
            var result = sut.GetAnswer(testExpression);

            Assert.Equal(expectedAnswer, result);
        }
    }
}
