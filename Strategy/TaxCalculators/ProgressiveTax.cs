using System;
using System.Collections.Generic;

namespace Strategy.TaxCalculators
{
    public class ProgressiveTax : ITaxCalculator
    {
        public List<string> TaxableCountries { get; }

        public ProgressiveTax(List<string> taxableCountries)
        {
            TaxableCountries = taxableCountries;
        }

        public decimal CalculateTax()
        {
            Console.WriteLine("This is the progressive tax calculator");
            return 0;
        }
    }
}
