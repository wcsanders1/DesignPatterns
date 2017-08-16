using System;
using System.Collections.Generic;

namespace Prototype
{
    class Program
    {
        static void Main()
        {
            var keepLooping = true;
            const string invalidChoiceMessage = "\nThat's not a valid choice, so I guess we'll try this again.\n";

            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("                  WELCOME TO THE PROTOTYPE PROGRAM -- WHICH IS KIND OF A BORING PROGRAM");
            Console.WriteLine("**********************************************************************************************************\n");

            while (keepLooping)
            {
                Console.WriteLine("Enter the number of one of the classic websites below to get information about that site!");
                var urls = GetUrls();

                for (int i = 0; i < urls.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {urls[i]}");
                }

                Console.WriteLine();

                var choiceString = Console.ReadLine();
                if (!Int32.TryParse(choiceString, out var choice))
                {
                    Console.WriteLine($"{invalidChoiceMessage}");
                    continue;
                }

                var url = urls[choice - 1];

                var explorer = new WebPageExplorer(url);
                var (info, error) = explorer.GetInformationAsync().Result;

                if (error != null)
                {
                    Console.WriteLine($"{error.Message}");
                    Console.WriteLine($"{error.Exception.Message}");
                }
                else
                {
                    Console.WriteLine($"The version number of {url} is {info.Version.ToString()}. Neat!\n");
                }

                keepLooping = KeepGoing();
            }
        }

        static List<string> GetUrls()
        {
            return new List<string>
            {
                "exotic-recipes.com",
                "keithsandersmusic.com",
                "williamsandersdev.com",
                "golfbagonline.com"
            };
        }

        static bool KeepGoing()
        {
            Console.WriteLine("\nPress 1 if you want to do this again, or press anything else to exit this program\n" +
                                "and do something else I guess.\n");

            var result = Console.ReadLine();

            if (result == "1")
            {
                return true;
            }

            return false;
        }
    }
}