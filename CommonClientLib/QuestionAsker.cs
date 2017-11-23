using System;
using System.Collections.Generic;

namespace CommonClientLib
{
    public class QuestionAsker
    {
        private const string InvalidInputMessage = "That input isn't valid. Let's try again.\n";

        /// <summary>
        /// Asks user whether the answer to a question is true or false.
        /// </summary>
        /// <param name="question">Question the user answers.</param>
        /// <returns><code>true</code> if user enters 1; <code>false</code> if user enters 2.</returns>
        public bool IsTrueOrFalse(string question)
        {
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine($"{question} Enter 1 for true or 2 for false.");
                var answer = Console.ReadLine();
                Console.WriteLine();
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
                    Console.WriteLine(InvalidInputMessage);
                }
            }
        }

        /// <summary>
        /// Prints a question and a list of choices to the console and 
        /// asks the user to enter a choice from the list.
        /// </summary>
        /// <param name="question">The question asked to the user</param>
        /// <param name="choices">List of choices</param>
        /// <returns>The element of the list of choices that the user chose</returns>
        public int GetChoiceFromList(string question, List<string> choices)
        {
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine($"{question}\n");

                var index = 1;
                foreach (var choice in choices)
                {
                    Console.WriteLine($"{index++}. {choice}");
                }

                Console.WriteLine();
                var strAnswer = Console.ReadLine();
                if (!int.TryParse(strAnswer, out var answer))
                {
                    Console.WriteLine(InvalidInputMessage);
                    continue;
                }

                if (answer < 1 || answer > index)
                {
                    Console.WriteLine(InvalidInputMessage);
                    continue;
                }

                return --answer;
            }
        }
    }
}
