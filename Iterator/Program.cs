using CommonClientLib;
using System;

namespace Iterator
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();

        static void Main(string[] args)
        {
            TxtPrinter.PrintInformation("WELCOME TO THE ITERATOR PROGRAM -- WHICH IS A BORING PROGRAM TO USE");

            while (true)
            {

                if (!ContinuationDeterminer.GoAgain())
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
