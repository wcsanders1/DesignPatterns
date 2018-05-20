using System;
using System.Collections.Generic;

namespace Strategy.TaxCalculators
{
    public class FlatTax : ITaxCalculator
    {
        public List<string> TaxableCountries { get; }

        public FlatTax(List<string> taxableCountries)
        {
            TaxableCountries = taxableCountries;
        }

        public decimal CalculateTax()
        {
            Console.WriteLine("This is the flat tax calculator");
            return 0;
        }
    }
}
