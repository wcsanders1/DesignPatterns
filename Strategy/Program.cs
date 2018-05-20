using CommonClientLib;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace Strategy
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();
        private static string MapPath = "CountryTaxSystemMap.json";
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();

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



            while (true)
            {
                if (!ContinuationDeterminer.GoAgain())
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
