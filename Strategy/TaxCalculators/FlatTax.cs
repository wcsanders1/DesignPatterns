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
            return 0;
        }
    }
}
