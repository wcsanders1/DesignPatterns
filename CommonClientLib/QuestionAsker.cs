using System;

namespace CommonClientLib
{
    public class QuestionAsker
    {
        /// <summary>
        /// Asks user whether the answer to a question is true or false.
        /// </summary>
        /// <param name="question">Question the user answers.</param>
        /// <returns><code>true</code> if user enters 1; <code>false</code> if user enters 2.</returns>
        public bool IsTrueOrFalse(string question)
        {
            Console.WriteLine();
            Console.WriteLine($"{question} Enter 1 for true or 2 for false.");

            while (true)
            {
                var answer = Console.ReadLine();
                if (answer == "1")
                {
                    return true;
                }
                else if (answer == "2")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("That input isn't valid. Let's try again.");
                }
            }
        }
    }
}
