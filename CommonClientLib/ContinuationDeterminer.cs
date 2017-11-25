using System;

namespace CommonClientLib
{
    public class ContinuationDeterminer
    {
        public bool GoAgain()
        {
            Console.WriteLine("Enter 0 to quit, or something else to have some more fun.\n");

            return GetNegativeChoice();
        }

        public bool GoAgain(string question)
        {
            Console.WriteLine(question);
            Console.WriteLine("If so, enter 1. Otherwise, enter anything else.");

            return GetPositiveChoice();
        }

        public bool GoAgainWithInvalidInputMessage(string message = "That input is invalid.")
        {
            Console.WriteLine($"{message} Enter 0 to quit or anything else to try again.");

            return GetNegativeChoice();
        }

        private bool GetNegativeChoice()
        {
            var goAgain = Console.ReadLine();
            Console.WriteLine();
            if (goAgain == "0")
            {
                return false;
            }

            return true;
        }

        private bool GetPositiveChoice()
        {
            var goAgain = Console.ReadLine();
            Console.WriteLine();
            if (goAgain == "1")
            {
                return true;
            }

            return false;
        }
    }
}
