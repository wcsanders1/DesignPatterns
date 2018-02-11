using CommonClientLib;

namespace ChainOfResponsibility
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();

        static void Main(string[] args)
        {
            TxtPrinter.PrintInformation("WELCOME TO THE CHAIN OF RESPONSIBILITY PROGRAM -- WHICH IS REALLY KIND OF A SILLY PROGRAM");
        }
    }
}
