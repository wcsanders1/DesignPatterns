using CommonClientLib;
using System;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using Decorator.Component;

namespace Decorator
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();
        private static TextParser TxtParser = new TextParser();
        private static TypeParser TypParser = new TypeParser(TxtParser);
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();
        private static QuestionAsker QuestionAsker = new QuestionAsker();

        private const string InfoFile = "LocationInfo.json";

        static void Main(string[] args)
        {
            TxtPrinter.PrintInformation("WELCOME TO THE DECORATOR PROGRAM -- WHICH IS SORT OF COOL, I GUESS");

            while(true)
            {
                JObject locationInfo = null;
                try
                {
                    using (var reader = new StreamReader(InfoFile))
                    {
                        var json = reader.ReadToEnd();
                        locationInfo = JObject.Parse(json);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unable to parse {InfoFile}.");
                    Console.WriteLine($"Exception message: {ex.Message}");
                    Environment.Exit(1);
                }

                var town = GetTown(locationInfo);

                if (!ContinuationDeterminer.GoAgain())
                {
                    Environment.Exit(0);
                }
            }
        }

        static Town GetTown(JObject data)
        {
            if (!data.TryGetValue("towns", out var rawTowns))
            {
                Console.WriteLine($"Unable to parse {InfoFile}.");
                Environment.Exit(1);
            }

            var towns = new List<Town>();
            foreach (var town in rawTowns)
            {
                var name = town.Value<string>("name");
                var location = town.Value<string>("location");
                var population = town.Value<int>("population");
                towns.Add(new Town(name, location, population));
            }

            var choice = QuestionAsker.GetChoiceFromList("What town are you in?", 
                towns.Select(t => t.Name).ToList());
            
            return towns[choice];
        }
    }
}
