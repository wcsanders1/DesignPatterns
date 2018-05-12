using CommonClientLib;
using System;

namespace State
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();

        static void Main(string[] args)
        {
            TxtPrinter.PrintInformation("WELCOME TO THE STATE PROGRAM -- WHICH PRESENTS A SLIGHT CHALLENGE");

            while (true)
            {
                var mathGame = new MathGame();
                mathGame.PlayGame();

                if (!ContinuationDeterminer.GoAgain())
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
