using CommonClientLib;
using System;

namespace Iterator
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();
        private static QuestionAsker QuestionAsker = new QuestionAsker();

        static void Main(string[] args)
        {
            TxtPrinter.PrintInformation("WELCOME TO THE ITERATOR PROGRAM -- WHICH IS A BORING PROGRAM TO USE");
            TxtPrinter.PrintInformation("All this program really does is put data onto a stack and then iterate the stack.",
                '-', ConsoleColor.Magenta);

            while (true)
            {
                var iterableStack = new IterableStack<string>();
                
                while (true)
                {
                    var newVal = QuestionAsker.GetValue<string>($"\nEnter a new value to put on the stack.\n");
                    iterableStack.Push(newVal);
                    if (!ContinuationDeterminer.GoAgain("\nDo you want to put another value on the stack?\n"))
                    {
                        break;
                    }
                }

                Console.WriteLine("\nHere are the values that you put on the stack:\n");
                foreach (var item in iterableStack)
                {
                    Console.WriteLine(item);
                }

                if (!ContinuationDeterminer.GoAgain())
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
