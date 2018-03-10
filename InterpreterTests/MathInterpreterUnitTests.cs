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
    }
}
