using Singleton.Topics;
using System;

namespace Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            var keepLooping = true;

            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("    WELCOME TO THE ARGUMENT PROGRAM -- WHICH IS A PROGRAM THAT'S KIND OF FUN, AT LEAST AT FIRST MAYBE.");
            Console.WriteLine("**********************************************************************************************************\n");

            while (keepLooping)
            {
                keepLooping = GoAgain();
            }
        }

        static bool GoAgain()
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
