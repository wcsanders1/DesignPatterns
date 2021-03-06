﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

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
        /// <returns>The index of the element of the list of choices that the user chose</returns>
        public int GetChoiceFromList(string question, List<string> choices)
        {
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine($"{question}\n");

                var index = 0;
                foreach (var choice in choices)
                {
                    Console.WriteLine($"{++index}. {choice}");
                }

                Console.WriteLine();
                var strAnswer = Console.ReadLine();
                if (!int.TryParse(strAnswer, out var answer))
                {
                    Console.WriteLine($"\n{InvalidInputMessage}");
                    continue;
                }

                if (answer < 1 || answer > index)
                {
                    Console.WriteLine($"\n{InvalidInputMessage}");
                    continue;
                }

                return --answer;
            }
        }

        /// <summary>
        /// Gets an integer value between zero and some value from the user.
        /// </summary>
        /// <param name="question">Question asked to the user</param>
        /// <param name="limit">Upper bound of the integer</param>
        /// <returns>Integer provided by the user</returns>
        public int GetInt(string question, int limit = int.MaxValue)
        {
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine($"{question}\n");

                var strAnswer = Console.ReadLine();
                if (!int.TryParse(strAnswer, out var intAnswer))
                {
                    Console.WriteLine("\nYou must provide an integer.\n");
                    continue;
                }

                if (intAnswer > limit || intAnswer < 0)
                {
                    Console.WriteLine($"\nYou must provide an integer between 0 and {limit}.\n");
                    continue;
                }

                return intAnswer;
            }
        }

        /// <summary>
        /// Gets a value of certain type.
        /// </summary>
        /// <typeparam name="T">Type that value needs to be.</typeparam>
        /// <returns>A user-entered value.</returns>
        public T GetValue<T>(string message)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));

            while (true)
            {
                Console.WriteLine(message);
                var strAnswer = Console.ReadLine();
                if (string.IsNullOrEmpty(strAnswer))
                {
                    Console.WriteLine($"That input is not valid.");
                    continue;
                }

                try
                {
                    var converted = (T)converter.ConvertFromString(strAnswer);
                    return converted;
                }
                catch (Exception)
                {
                    Console.WriteLine("That value is improper. Try again. And thank you.");
                }
            }
        }
    }
}
