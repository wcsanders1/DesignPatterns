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
            TxtPrinter.PrintInformation("WELCOME TO THE PROXY PROGRAM -- WHICH PERFORMS MILDLY INTERESTING TASKS");

            while (true)
            {
                var diskReader = new DiskReaderProxy();

                var drives = diskReader.GetDrives();

                var driveChoice = Asker.GetChoiceFromList("Choose a drive that you want to search: ", drives);

                Console.WriteLine("What file extension do you want to search for?");
                var extension = Console.ReadLine();

                var files = diskReader.GetFiles(drives[driveChoice], extension);

                if (files.Count == 0)
                {
                    Console.WriteLine($"Did not find any files with the extension {extension}");

                    if (!ContinuationDeterminer.GoAgain())
                    {
                        Environment.Exit(0);
                    }
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

                if (!ContinuationDeterminer.GoAgain())
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
