using Strategy.TaxCalculators;
using System;

namespace Strategy
{
    public class CalculatorContext
    {
        private ITaxCalculator Calculator { get; set; }

        public void SetCalculator(ITaxCalculator calculator)
        {
            Calculator = calculator;
        }

        public decimal CalculateTax()
        {
            if (Calculator == null)
            {
                Console.WriteLine("Cannot calculate tax because no calculator has been provided.");

                return 0;
            }

            return Calculator.CalculateTax();
        }
    }
}
