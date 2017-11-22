using CommonClientLib;
using System;

namespace Decorator
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();
        private static TextParser TxtParser = new TextParser();
        private static TypeParser TypParser = new TypeParser(TxtParser);
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();
        private static QuestionAsker QuestionAsker = new QuestionAsker();

        static void Main(string[] args)
        {
            TxtPrinter.PrintInformation("WELCOME TO THE DECORATOR PROGRAM -- WHICH IS SORT OF COOL, I GUESS");
        }
    }
}
