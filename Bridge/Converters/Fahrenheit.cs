using Bridge.Formatters;
using System;

namespace Bridge.Converters
{
    public class Fahrenheit : AbstractConverter
    {
        const decimal DIV = 5M / 9M;
        const string ORIGINAL_TYPE = "Fahrenheit";

        private decimal Celsius { get; set; }
        private decimal Kelvin { get; set; }
        private IFormatter Formatter { get; }

        public Fahrenheit(IFormatter formatter) : base(formatter)
        {
            Formatter = formatter;
        }

        public override void Convert(decimal fahrenheit)
        {
            Celsius = (fahrenheit - 32) * DIV;
            Kelvin = (fahrenheit + 459.67M) * DIV;
            Print();
        }

        private void Print()
        {
            Console.WriteLine(Formatter.GetString(ORIGINAL_TYPE, nameof(Celsius), Celsius.ToString("#.##")));
            Console.WriteLine(Formatter.GetString(ORIGINAL_TYPE, nameof(Kelvin), Kelvin.ToString("#.##")));
            Console.WriteLine();
        }
    }
}
