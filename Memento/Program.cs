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
                Console.WriteLine();

                Console.CursorVisible = false;
                var key = new ConsoleKey();
                while (key != ConsoleKey.D0)
                {
                    key = Console.ReadKey().Key;
                    if (key == ConsoleKey.RightArrow)
                    {
                        mine.MoveRight();
                    }
                }

                Console.CursorVisible = true;
                if (!ContinuationDeterminer.GoAgain())
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
