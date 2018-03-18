using CommonClientLib;
using System;

namespace Interpreter
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();

        static void Main(string[] args)
        {
            TxtPrinter.PrintInformation("WELCOME TO THE INTERPRETER PROGRAM -- WHICH CAN BE NEAT TO USE");
            TxtPrinter.PrintInformation("This program solves mathematical expressions.", '-', ConsoleColor.Magenta);
            TxtPrinter.PrintInformation("E.g.: Enter '4 + 5 - 2 * (6 + 4)' and you'll receive the answer -11.", 
                '-', ConsoleColor.DarkYellow);

            while (true)
            {
                Console.Write("Enter a mathematical expression here: ");
                var expression = Console.ReadLine();
                var interpreter = new MathInterpreter();

                try
                {
                    var answer = interpreter.GetAnswer(expression);
                    Console.Write("\nAnswer: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{answer}\n\n");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine($"\nThere was an error evaluating the expression. " +
                        $"Error: {ex.Message}\n");
                }

                if (!ContinuationDeterminer.GoAgain())
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
