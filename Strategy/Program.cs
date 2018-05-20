using CommonClientLib;
using Newtonsoft.Json.Linq;
using Strategy.TaxCalculators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Strategy
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();
        private static string MapPath = "CountryTaxSystemMap.json";
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();
        private static TypeParser TypParser = new TypeParser(new TextParser());
        private static QuestionAsker Asker = new QuestionAsker();

        static void Main(string[] args)
        {
            TxtPrinter.PrintInformation("WELCOME TO THE STRATEGY PROGRAM -- WHICH SORT OF MIGHT MAKE YOU THINK");

            var countryTaxSystemMap = new Dictionary<string, List<string>>();
            try
            {
                using (var reader = new StreamReader(MapPath))
                {
                    var json = reader.ReadToEnd();
                    var jObject = JObject.Parse(json);
                    foreach (var system in jObject)
                    {
                        if (countryTaxSystemMap.ContainsKey(system.Key))
                        {
                            Console.WriteLine($"There is a duplicate tax system entry in {MapPath}. Only the first entry will be used.");
                            continue;
                        }

                        countryTaxSystemMap.Add(system.Key, system.Value.ToObject<List<string>>());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to parse the county-tax system map from {MapPath}. Exception: {ex.Message}");
                Console.ReadKey();
                Environment.Exit(1);
            }

            var taxCalculators = new List<ITaxCalculator>();
            try
            {
                var (calulatorDictionary, calculatorNames) = TypParser.GetTypeDictionaryAndNameList<ITaxCalculator>();
                var key = 1;
                foreach (var calculatorName in calculatorNames)
                {
                    if (!countryTaxSystemMap.TryGetValue(calculatorName.ToLower(), out var countries))
                    {
                        Console.WriteLine($"There is no entry for the {calculatorName} tax calculator in the map; therefore, " +
                            $"this tax calculator will be ignored.");
                        continue;
                    }

                    var calculator = Activator.CreateInstance(calulatorDictionary[key++], countries) as ITaxCalculator;
                    taxCalculators.Add(calculator);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an error trying to create the tax calculators. Exception: {ex.Message}");
                Console.ReadKey();
                Environment.Exit(1);
            }

            var allCountries = taxCalculators
                .SelectMany(c => c.TaxableCountries)
                .Distinct()
                .OrderBy(c => c)
                .ToList();

            var calculatorContext = new CalculatorContext();

            while (true)
            {
                var currentCountry = allCountries[Asker.GetChoiceFromList("What country do you live in?", allCountries)];
                var taxCalculator = taxCalculators.FirstOrDefault(c => c.TaxableCountries.Contains(currentCountry));
                calculatorContext.SetCalculator(taxCalculator);
                calculatorContext.CalculateTax();

                if (!ContinuationDeterminer.GoAgain())
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
