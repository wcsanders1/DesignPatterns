using CommonClientLib;
using System;

namespace Strategy
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();        
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();
        private static QuestionAsker Asker = new QuestionAsker();

        static void Main(string[] args)
        {
            TxtPrinter.PrintInformation("WELCOME TO THE INTERPRETER METHOD PROGRAM -- WHICH MAY PROVIDE SOME INTEREST");

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
