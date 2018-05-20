using CommonClientLib;
using System;
using System.Collections.Generic;

namespace Strategy.TaxCalculators
{
    public class ProgressiveTax : ITaxCalculator
    {
        private static readonly QuestionAsker Asker = new QuestionAsker();
        private const decimal FirstLevel = 50000;
        private const decimal SecondLevel = 100000;

        public List<string> TaxableCountries { get; }

        public ProgressiveTax(List<string> taxableCountries)
        {
            TaxableCountries = taxableCountries;
        }

        public decimal CalculateTax(string country)
        {
            Console.WriteLine($"\nThe country of {country} uses the progressive tax system.\n");
            Console.WriteLine($"The progressive tax system taxes income up to ${FirstLevel} at a rate of 10%, " +
                $"anything between ${FirstLevel + 1} and ${SecondLevel} at rate of 20%, and all else at a rate of 30%.\n");

            var lastYearEarnings = Asker.GetValue<decimal>($"How much money did you earn last year in the country of {country}?");

            if (lastYearEarnings < 1)
            {
                Console.WriteLine("There is no tax on earnings lest than $1");

                return 0;
            }
            
            if (lastYearEarnings <= FirstLevel)
            {
                return lastYearEarnings * .1M;
            }

            if (lastYearEarnings <= SecondLevel)
            {
                return (FirstLevel * .1M) + ((lastYearEarnings - FirstLevel) * .2M);
            }

            return (FirstLevel * .1M) + ((SecondLevel - FirstLevel) * .2M) + ((lastYearEarnings - SecondLevel) * .3M);
        }
    }
}
