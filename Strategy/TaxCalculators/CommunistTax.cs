using CommonClientLib;
using System;
using System.Collections.Generic;

namespace Strategy.TaxCalculators
{
    public class CommunistTax : ITaxCalculator
    {
        private static readonly QuestionAsker Asker = new QuestionAsker();

        public List<string> TaxableCountries { get; }

        public CommunistTax(List<string> taxableCountries)
        {
            TaxableCountries = taxableCountries;
        }

        public decimal CalculateTax(string country)
        {
            Console.WriteLine($"\nThe country of {country} uses the communist tax system.");
            Console.WriteLine("The communist tax system confiscates all of your assets.");

            var totalAssets = Asker.GetValue<decimal>("What is the total value of everything you own?");

            return totalAssets;
        }
    }
}
