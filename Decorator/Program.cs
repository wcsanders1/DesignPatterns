﻿using CommonClientLib;
using System;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using Decorator.Component;
using Decorator.Decorators;

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

        private static readonly List<string> Decorators = new List<string>
        {
            "countries",
            "continents",
            "planet",
            "galaxy",
            "universe",
            "mind"
        };

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
                town.PrintInfo();
                
                foreach (var decorator in Decorators)
                {
                    if (!KeepGoing(town.Name))
                    {
                        continue;
                    }

                    town = Decorate(locationInfo, town, decorator);
                    town.PrintInfo();
                }
                
                if (!ContinuationDeterminer.GoAgain())
                {
                    Environment.Exit(0);
                }
            }
        }

        private static bool KeepGoing(string name)
        {
            if (!ContinuationDeterminer.GoAgain($"\nWould you like to know where {name} is?"))
            {
                if (!ContinuationDeterminer.GoAgain())
                {
                    Environment.Exit(0);
                }

                return false;
            }

            return true;
        }

        private static ILocation GetTown(JObject data)
        {
            var rawTowns = GetJToken(data, "towns");
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

        private static ILocation Decorate(JObject data, ILocation component, string decorator)
        {
            var rawData = GetJToken(data, decorator);
            foreach (var element in rawData)
            {
                var name = element.Value<string>("name");
                if (name == component.Location)
                {
                    var location = element.Value<string>("location");
                    switch (decorator)
                    {
                        case "countries":
                            return new Country(
                                component,
                                name,
                                location,
                                element.Value<string>("anthem"));
                        case "continents":
                            return new Continent(
                                component,
                                name,
                                location,
                                element.Value<int>("countryAmount"),
                                element.Value<string>("size")
                                );
                        default:
                            return null;
                    }
                    
                }
            }

            return null;
        }

        private static JToken GetJToken(JObject data, string property)
        {
            if (!data.TryGetValue(property, out var token))
            {
                Console.WriteLine($"Unable to parse {InfoFile}.");
                Environment.Exit(1);
            }

            return token;
        }
    }
}
