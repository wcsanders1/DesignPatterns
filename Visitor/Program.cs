using CommonClientLib;
using System;

namespace Visitor
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();
        private static QuestionAsker Asker = new QuestionAsker();

        static void Main(string[] args)
        {
            TxtPrinter.PrintInformation("WELCOME TO THE VISITOR PROGRAM -- WHICH MAY OFFER SOME CONSOLATION");

            if (!ContinuationDeterminer.GoAgain())
            {
                Environment.Exit(0);
            }
        }
    }
}
