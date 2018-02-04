using CommonClientLib;
using System;
using System.Collections.Generic;

namespace Proxy
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();
        private static QuestionAsker Asker = new QuestionAsker();
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();
        private static readonly List<string> YesOrNo = new List<string>
        {
            "Yes",
            "No"
        };

        static void Main(string[] args)
        {
            TxtPrinter.PrintInformation("WELCOME TO THE PROXY PROGRAM -- WHICH DOES MILDLY INTERESTING THINGS");

            while (true)
            {
                var diskReader = new DiskReaderProxy();

                var drives = diskReader.GetDrives();

                var driveChoice = Asker.GetChoiceFromList("Choose a drive that you want to search: ", drives);

                Console.WriteLine("What file extension do you want to search for?");
                var extension = Console.ReadLine();

                var files = diskReader.GetFiles(drives[driveChoice], extension);

                if (files == null)
                {
                    Console.WriteLine($"There was a problem getting files for extension {extension}.");

                    if (!ContinuationDeterminer.GoAgain())
                    {
                        Environment.Exit(0);
                    }

                    continue;
                }

                if (files.Count == 0)
                {
                    Console.WriteLine($"Did not find any files with the extension {extension}");

                    if (!ContinuationDeterminer.GoAgain())
                    {
                        Environment.Exit(0);
                    }

                    continue;
                }

                Console.WriteLine($"Found {files.Count} files with the extension {extension}.");
                var printChoice = Asker.GetChoiceFromList("Do you want to see them?", YesOrNo);

                if (YesOrNo[printChoice] == "Yes")
                {
                    foreach (var file in files)
                    {
                        Console.WriteLine(file);
                    }
                }

                Console.WriteLine();

                if (!ContinuationDeterminer.GoAgain())
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
