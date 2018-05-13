using CommonClientLib;
using System;

namespace State.States
{
    public abstract class GameState
    {
        public MathGame MathGame { get; set; }
        public int QuestionsCorrect { get; set; }
        public int QuestionsAttempted { get; set; }
        private static QuestionAsker QuestionAsker = new QuestionAsker();

        public abstract void AskQuestion();

        protected int Add(int digitOne, int digitTwo)
        {
            return digitOne + digitTwo;
        }

        protected int Subtract(int digitOne, int digitTwo)
        {
            return digitOne - digitTwo;
        }

        protected int Divide(int digitOne, int digitTwo)
        {
            return digitOne / digitTwo;
        }

        protected int Multiply(int digitOne, int digitTwo)
        {
            return digitOne * digitTwo;
        }

        protected int GetAnswer(string question)
        {
            return QuestionAsker.GetValue<int>(question);
        }

        protected void EvaluateAnswer(int answer, int answerGiven)
        {
            if (answerGiven == answer)
            {
                Console.WriteLine("\nThat's the right answer. Good.\n");
                QuestionsCorrect++;
            }
            else
            {
                Console.WriteLine($"\nNope. The right answer is {answer}\n");
            }
            QuestionsAttempted++;
        }
    }
}
