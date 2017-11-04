using CommonClientLib;
using System;
using System.Collections.Generic;

namespace Prototype
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();
        const string INVALID_CHOICE_MESSAGE = "\nThat's not a valid choice, so I guess we'll try this again.\n";

        static void Main()
        {
            var urls                          = GetUrls();
            var explorers                     = GetExplorers(urls);
            var keepLooping                   = true;
            
            TxtPrinter.PrintInformation("WELCOME TO THE PROTOTYPE PROGRAM -- WHICH IS KIND OF A BORING PROGRAM");
            
            while (keepLooping)
            {
                Console.WriteLine("Enter the number of one of the classic websites below to get information about that site!");

                for (int i = 0; i < urls.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {urls[i]}");
                }

                Console.WriteLine();

                var choiceString = Console.ReadLine();
                if (!int.TryParse(choiceString, out var choice))
                {
                    Console.WriteLine($"{INVALID_CHOICE_MESSAGE}");
                    continue;
                }

                WebPageExplorer explorer;
                try
                {
                    explorer = explorers[--choice];
                }
                catch
                {
                    Console.WriteLine(INVALID_CHOICE_MESSAGE);
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