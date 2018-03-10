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

        //[Fact]
        //public void GetAnswer_ReturnsCorrectAnswer_WhenProvidedExpressionWithDecimal_4()
        //{
        //    const string testExpression = "4012.43 + 550992.111";
        //    const decimal expectedAnswer = 555004.541M;

        //    var sut = new MathInterpreter();
        //    var result = sut.GetAnswer(testExpression);

        //    Assert.Equal(expectedAnswer, result);
        //}
    }
}
