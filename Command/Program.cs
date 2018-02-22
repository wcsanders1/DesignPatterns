using CommonClientLib;

namespace Command
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();

        static void Main(string[] args)
        {
            TxtPrinter.PrintInformation("WELCOME TO THE COMMAND PROGRAM - WHICH IS YET FORMLESS");
        }
    }
}
