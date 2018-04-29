using CommonClientLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mediator
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();
        private static List<string> Arguments = new List<string>();
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();
        private static string ArgumentsPath = "Arguments.json";

        static void Main(string[] args)
        {
            TxtPrinter.PrintInformation("WELCOME TO THE MEDIATOR PROGRAM -- WHICH IS A MILDLY ENTERTAINING PROGRAM");

            try
            {
                using (var reader = new StreamReader(ArgumentsPath))
                {
                    var json = reader.ReadToEnd();
                    Arguments.AddRange(JsonConvert.DeserializeObject<List<string>>(json));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to retrieve arguments from {ArgumentsPath}.\n " +
                    $"Using default arguments. Exception message: {ex.Message}\n");
                Arguments.AddRange(DefaultArguments.GetDefaultArguments());
            }

            while(true)
            {
                var debateMediator = new DebateMediator();
                var debators = GetDebators(debateMediator, "Martha", "Willis", "Redford", "Millie");
                Console.WriteLine("Enter your argument.\n");
                Console.ReadLine();
                Console.WriteLine();
                debateMediator.Mediate();
                Console.WriteLine();

                if (!ContinuationDeterminer.GoAgain())
                {
                    Environment.Exit(0);
                }
            }
        }

        private static List<Debator> GetDebators(DebateMediator debateMediator, params string[] names)
        {
            List<string> processedNames;
            if (names.Length > Arguments.Count)
            {
                processedNames = names.Take(Arguments.Count).ToList();
            }
            else
            {
                processedNames = names.ToList();
            }

            var debators = new List<Debator>();
            foreach (var name in processedNames)
            {
                var debator = new Debator(debateMediator, name, Arguments);
                debateMediator.RegisterDebator(debator);
                debators.Add(debator);
            }

            return debators;
        }
    }
}
