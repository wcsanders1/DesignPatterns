using CommonClientLib;
using System;

namespace Memento
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();
        private const int NumberOfExplositionsAllowed = 10;
        private const int NumberOfUndosAllowed = 5;
        private const int MineWidth = 20;
        private const int MineHeight = 7;

        static void Main(string[] args)
        {
            TxtPrinter.PrintInformation("WELCOME TO THE MEMENTO PROGRAM -- WHICH IS RELATIVELY NEAT AND FUN!");

            while(true)
            {
                var mine = new Mine(MineWidth, MineHeight, NumberOfExplositionsAllowed, NumberOfUndosAllowed);
                mine.PrintMineBoard();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nUse the arrow keys to move around the mine. Press space to blast, u to undo, and ESC to quit.");
                Console.ResetColor();
                var cursorReturnPosition = Console.CursorTop;

                Console.CursorVisible = false;
                var key = new ConsoleKey();
                while (key != ConsoleKey.Escape && mine.GetGameState() == GameState.InProgress)
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
                        case ConsoleKey.Spacebar:
                            mine.Blast();
                            break;
                        case ConsoleKey.U:
                            mine.UndoBlast();
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
