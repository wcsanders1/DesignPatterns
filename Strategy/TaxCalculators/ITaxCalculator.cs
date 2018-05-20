using CommonClientLib;
using System.Collections.Generic;

namespace Strategy.TaxCalculators
{
    public interface ITaxCalculator
    {
        List<string> TaxableCountries { get; }

        decimal CalculateTax(string country);
    }
}
