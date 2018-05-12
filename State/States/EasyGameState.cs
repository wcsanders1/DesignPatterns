using System;

namespace State.States
{
    public class EasyGameState : GameState
    {
        private const int NumberLimit = 100;
        private static Random Random = new Random();

        public EasyGameState(GameState gameState) :
            this(gameState.QuestionsAttempted, gameState.QuestionsCorrect, gameState.MathGame)
        { }

        public EasyGameState(int questionsAttempted, int questionsCorrect, MathGame mathGame)
        {
            QuestionsAttempted = questionsAttempted;
            QuestionsCorrect = questionsCorrect;
            MathGame = mathGame;
        }

        public override void AskQuestion()
        {
            var (question, answer) = GetQuestionAndCorrectAnswer();
            var answerGiven = GetAnswer(question);

            if (answerGiven == answer)
            {
                Console.WriteLine("That's the right answer. Good.");
                QuestionsCorrect++;
            }
            else
            {
                Console.WriteLine($"Nope. The right answer is {answer}");
            }
            QuestionsAttempted++;
        }

        private (string, int) GetQuestionAndCorrectAnswer()
        {
            var operation = GetRandomOperation();
            var digitOne = 0;
            var digitTwo = 0;
            var answer = 0;
            string opSign;
            switch (operation)
            {
                case Operation.Add:
                    digitOne = Random.Next(1, NumberLimit);
                    digitTwo = Random.Next(1, NumberLimit);
                    answer = Add(digitOne, digitTwo);
                    opSign = "+";
                    break;
                case Operation.Subtract:
                    digitOne = Random.Next(NumberLimit / 2, NumberLimit);
                    digitTwo = Random.Next(1, NumberLimit / 2);
                    answer = Subtract(digitOne, digitTwo);
                    opSign = "-";
                    break;
                default:
                    opSign = string.Empty;
                    break;
            }

            return ($"What is {digitOne} {opSign} {digitTwo}?", answer);
        }

        private Operation GetRandomOperation()
        {
            var operation = Random.Next(0, 2);

            return (Operation)operation;
        }
    }
}
