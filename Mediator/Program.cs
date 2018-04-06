using CommonClientLib;

namespace Mediator
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();

        static void Main(string[] args)
        {
            TxtPrinter.PrintInformation("WELCOME TO THE MEDIATOR PROGRAM -- WHICH IS A MILDLY ENTERTAINING PROGRAM");
        }
    }
}
