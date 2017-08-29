using CommonClientLib;
using System;
using System.Collections.Generic;

namespace Prototype
{
    class Program
    {
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();

        static void Main()
        {
            var urls                          = GetUrls();
            var explorers                     = GetExplorers(urls);
            var keepLooping                   = true;
            const string invalidChoiceMessage = "\nThat's not a valid choice, so I guess we'll try this again.\n";

            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("                  WELCOME TO THE PROTOTYPE PROGRAM -- WHICH IS KIND OF A BORING PROGRAM");
            Console.WriteLine("**********************************************************************************************************\n");

            while (keepLooping)
            {
                Console.WriteLine("Enter the number of one of the classic websites below to get information about that site!");

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

                WebPageExplorer explorer;
                try
                {
                    explorer = explorers[choice];
                }
                catch
                {
                    Console.WriteLine(invalidChoiceMessage);
                    continue;
                }
                
                var (info, error) = explorer.GetInformationAsync().Result;

                if (error != null)
                {
                    Console.WriteLine($"{error.Message}");
                    Console.WriteLine($"{error.Exception.Message}");
                }
                else
                {
                    Console.WriteLine($"The version number of {explorer.CurrentUrl} is {info.Version.ToString()}. Neat!\n");
                }

                keepLooping = ContinuationDeterminer.GoAgain();
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

        static List<WebPageExplorer> GetExplorers(List<string> urls)
        {
            var explorer = new WebPageExplorer(string.Empty);
            var explorers = new List<WebPageExplorer>();
            urls.ForEach(url =>
            {
                explorers.Add(explorer.Clone(url));
            });

            return explorers;
        }
    }
}