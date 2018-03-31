using CommonClientLib;
using System;
using System.Linq;

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
                    var newVal = QuestionAsker.GetValue<string>($"Enter a new value to put on the stack.\n");
                    iterableStack.Push(newVal);
                    if (!ContinuationDeterminer.GoAgain("Do you want to put another value on the stack?"))
                    {
                        break;
                    }
                }

                if (!ContinuationDeterminer.GoAgain())
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
