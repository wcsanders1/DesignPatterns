using System;

namespace Memento
{
    public class ExplosionsRemainingManager
    {
        private const string Message = "Explosions Remaining: ";

        private int ExplosionsRemaining { get; set; }
        private int XMessagePosition { get; set; }
        private int YMessagePosition { get; set; }

        public ExplosionsRemainingManager(int explosionsAmount, int[,] boardTopLeftPosition)
        {
            ExplosionsRemaining = explosionsAmount;
            Initialize(boardTopLeftPosition);
        }

        public void ReduceExplosionsRemaining()
        {
            ExplosionsRemaining--;
            PrintMessage();
        }

        public void IncreaseExplosionsRemaining()
        {
            ExplosionsRemaining++;
            PrintMessage();
        }

        public bool ExplosionsRemain()
        {
            return ExplosionsRemaining > 0;
        }

        private void Initialize(int[,] boardTopLeftPosition)
        {
            YMessagePosition = boardTopLeftPosition[0, 0] - 2;
            XMessagePosition = boardTopLeftPosition[0, 1] - 1;
            PrintMessage();
        }

        private void PrintMessage()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.CursorLeft = XMessagePosition;
            Console.CursorTop = YMessagePosition;
            Console.WriteLine($"{Message}{ExplosionsRemaining}");
            Console.ResetColor();
        }
    }
}
