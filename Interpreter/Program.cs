using CommonClientLib;

namespace Interpreter
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();

        static void Main(string[] args)
        {
            TxtPrinter.PrintInformation("WELCOME TO THE INTERPRETER PROGRAM -- WHICH CAN BE NEAT TO USE");
        }
    }
}
