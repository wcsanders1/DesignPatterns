﻿using CommonClientLib;
using System;
using System.Collections.Generic;

namespace Strategy.TaxCalculators
{
    public class ProgressiveTax : ITaxCalculator
    {
        private static readonly QuestionAsker Asker = new QuestionAsker();

        public List<string> TaxableCountries { get; }

        public ProgressiveTax(List<string> taxableCountries)
        {
            TaxableCountries = taxableCountries;
        }

        public decimal CalculateTax(string country)
        {
            Console.WriteLine($"\nThe country of {country} uses the progressive tax system.\n");
            return 0;
        }
    }
}
