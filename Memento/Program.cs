using CommonClientLib;
using System;

namespace Memento
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();

        static void Main(string[] args)
        {
            TxtPrinter.PrintInformation("WELCOME TO THE MEMENTO PROGRAM -- WHICH IS RELATIVELY NEAT AND FUN!");

            while(true)
            {
                var mine = new Mine(15, 7, 5, 20);
                mine.PrintMineBoard();
                Console.WriteLine("\nUse the arrow keys to move around the mine. Press ESC to quit.");
                var cursorReturnPosition = Console.CursorTop;

                Console.CursorVisible = false;
                var key = new ConsoleKey();
                while (key != ConsoleKey.Escape)
                {
                    key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.RightArrow:
                            mine.MoveRight();
                            break;
                        case ConsoleKey.LeftArrow:
                            mine.MoveLeft();
                            break;
                        case ConsoleKey.DownArrow:
                            mine.MoveDown();
                            break;
                        case ConsoleKey.UpArrow:
                            mine.MoveUp();
                            break;
                        default:
                            break;
                    }
                }

                Console.CursorVisible = true;
                Console.CursorTop = ++cursorReturnPosition;
                if (!ContinuationDeterminer.GoAgain())
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
