using System;

namespace CommonClientLib
{
    public class ContinuationDeterminer
    {
        public bool GoAgain()
        {
            Console.WriteLine("Enter 0 to quit, or something else to have some more fun.\n");

            return GetChoice();
        }

        public bool GoAgainWithInvalidInputMessage(string message = "That input is invalid.")
        {
            Console.WriteLine($"{message} Enter 0 to quit or anything else to try again.");

            return GetChoice();
        }

        private bool GetChoice()
        {
            var goAgain = Console.ReadLine();

            if (goAgain == "0")
            {
                return false;
            }

            return true;
        }
    }
}
