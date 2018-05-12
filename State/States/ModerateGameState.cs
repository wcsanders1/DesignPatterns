using System;

namespace State.States
{
    public class ModerateGameState : GameState
    {
        private const int NumberMax = 1000;
        private const int NumberMin = 101;
        private const int EasyModeLimit = 10;
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
            Console.Write("You're in the ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("moderate");
            Console.ResetColor();
            Console.WriteLine($" mode. Answer {EasyModeLimit - QuestionsCorrect} more questions correctly to get to the next level.");
        }
    }
}
