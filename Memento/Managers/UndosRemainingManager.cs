using System;

namespace Memento.Managers
{
    public class UndosRemainingManager
    {
        private const string Message = "Undos Remaining: ";

        private int UndosRemaining { get; set; }
        private int XMessagePosition { get; set; }
        private int YMessagePosition { get; set; }

        public UndosRemainingManager(int undosAmount, int[,] boardTopRightPosision)
        {
            UndosRemaining = undosAmount;
            YMessagePosition = boardTopRightPosision[0, 0] - 2;
            XMessagePosition = boardTopRightPosision[0, 1] - Message.Length + 3;
            PrintMessage();
        }

        public void ReduceUndosRemaining()
        {
            UndosRemaining--;
            PrintMessage();
        }

        public bool UndosRemain()
        {
            return UndosRemaining > 0;
        }

        private void PrintMessage()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.CursorLeft = XMessagePosition;
            Console.CursorTop = YMessagePosition;
            Console.WriteLine($"{Message}{UndosRemaining}");
            Console.ResetColor();
        }
    }
}
