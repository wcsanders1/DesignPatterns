using System;

namespace Memento.Managers
{
    public class WinLoseMessageManager
    {
        private const string WinningMessage = "You Won!";
        private const string LosingMessage = "You Lost!";

        private int YMessagePosition { get; }
        private int BoardMiddleXPosition { get; }

        public WinLoseMessageManager(int boardTopLeftYPosition, int boardMiddleXPosition)
        {
            YMessagePosition = boardTopLeftYPosition - 2;
            BoardMiddleXPosition = boardMiddleXPosition;
        }

        public void PrintMessage(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Lost:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    PrintMessage(LosingMessage);
                    break;
                case GameState.Won:
                    Console.ForegroundColor = ConsoleColor.Green;
                    PrintMessage(WinningMessage);
                    break;
                default:
                    break;
            }

            Console.ResetColor();
            Console.CursorLeft = 0;
        }

        private void PrintMessage(string message)
        {
            Console.CursorTop = YMessagePosition;
            Console.CursorLeft = BoardMiddleXPosition - (message.Length / 2);
            Console.Write(message);
        }
    }
}
