using System;

namespace State.States
{
    public class ModerateGameState : GameState
    {
        private const int NumberMax = 1000;
        private const int NumberMin = 101;
        private const int ModerateModeLimit = 10;
        private static Random Random = new Random();

        public ModerateGameState(GameState gameState) :
            this(gameState.QuestionsAttempted, gameState.QuestionsCorrect, gameState.MathGame)
        { }

        public ModerateGameState(int questionsAttempted, int questionsCorrect, MathGame mathGame)
        {
            QuestionsAttempted = questionsAttempted;
            QuestionsCorrect = questionsCorrect;
            MathGame = mathGame;
        }

        public override void AskQuestion()
        {
            var questionsTillNextLevel = ModerateModeLimit - QuestionsCorrect;
            Console.Write("You're in the ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("moderate");
            Console.ResetColor();
            Console.WriteLine($" mode. Answer {questionsTillNextLevel} " +
                $"more {(questionsTillNextLevel > 1 ? "questions " : "question ")}correctly to get to the next level.");

            var (question, answer) = GetQuestionAndCorrectAnswer();
            var answerGiven = GetAnswer(question);

            EvaluateAnswer(answer, answerGiven);
            StateChangeCheck();
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
                    digitOne = Random.Next(NumberMin, NumberMax);
                    digitTwo = Random.Next(NumberMin, NumberMax);
                    answer = Add(digitOne, digitTwo);
                    opSign = "+";
                    break;
                case Operation.Subtract:
                    digitOne = Random.Next(NumberMax / 2, NumberMax);
                    digitTwo = Random.Next(NumberMin, NumberMax / 2);
                    answer = Subtract(digitOne, digitTwo);
                    opSign = "-";
                    break;
                case Operation.Divide:
                    digitOne = Random.Next(NumberMax / 2, NumberMax);
                    digitTwo = Random.Next(NumberMin, NumberMax / 2);
                    answer = Divide(digitOne, digitTwo);
                    opSign = "/";
                    break;
                case Operation.Multiply:
                    digitOne = Random.Next(NumberMin, NumberMax);
                    digitTwo = Random.Next(NumberMin, NumberMax);
                    answer = Multiply(digitOne, digitTwo);
                    opSign = "*";
                    break;
                default:
                    opSign = string.Empty;
                    break;
            }

            return ($"What is {digitOne} {opSign} {digitTwo}?", answer);
        }

        private Operation GetRandomOperation()
        {
            var operation = Random.Next(0, Enum.GetNames(typeof(Operation)).Length);

            return (Operation)operation;
        }

        private void StateChangeCheck()
        {
            if (QuestionsCorrect >= ModerateModeLimit)
            {
                MathGame.ChangeState(new HardGameState(this));
            }
        }
    }
}
