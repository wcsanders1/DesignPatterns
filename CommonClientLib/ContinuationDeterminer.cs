using System;

namespace CommonClientLib
{
    public class ContinuationDeterminer
    {
        public bool GoAgain()
        {
            Console.WriteLine("Enter 0 to quit, or something else to have some more fun.\n");
            var goAgain = Console.ReadLine();

            if (goAgain == "0")
            {
                return false;
            }

            return true;
        }
    }
}
