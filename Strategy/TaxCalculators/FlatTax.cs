using CommonClientLib;
using System;
using System.Collections.Generic;

namespace Strategy.TaxCalculators
{
    public class FlatTax : ITaxCalculator
    {
        private static readonly QuestionAsker Asker = new QuestionAsker();

        public List<string> TaxableCountries { get; }

        public FlatTax(List<string> taxableCountries)
        {
            TaxableCountries = taxableCountries;
        }

        public decimal CalculateTax(string country)
        {
            Console.WriteLine($"\nThe country of {country} uses the flat-tax system.");
            Console.WriteLine("The flat-tax system taxes all income at a rate of 5%.\n");

            var lastYearEarnings = Asker.GetValue<decimal>($"How much money did you earn last year in the country of {country}?");
            if (lastYearEarnings < 1)
            {
                Console.WriteLine("There is no tax on earnings lest than $1");
                return 0;
            }

            return lastYearEarnings * .05M;
        }
    }
}
