using CommonClientLib;

namespace Memento
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();

        static void Main(string[] args)
        {
            TxtPrinter.PrintInformation("WELCOME TO THE MEMENTO PROGRAM -- WHICH IS RELATIVELY NEAT AND FUN!");
        }
    }
}
