using Bridge.Formatters;
using System;

namespace Bridge.Converters
{
    public class ConvertFromFahrenheit : AbstractConverter
    {
        const decimal Div = 5 / 9;
        const string OriginalType = "Fahrenheit";

        private decimal Celsius { get; set; }
        private decimal Kelvin { get; set; }
        private IFormatter Formatter { get; }

        public ConvertFromFahrenheit(IFormatter formatter) : base(formatter)
        {
            Formatter = formatter;
        }

        public override void Convert(decimal fahrenheit)
        {
            Celsius = (fahrenheit - 32) * Div;
            Kelvin = (fahrenheit + 459.67M) * Div;
            Print();
        }

        private void Print()
        {
            Console.WriteLine(Formatter.GetString(OriginalType, nameof(Celsius), Celsius.ToString("#.##")));
            Console.WriteLine(Formatter.GetString(OriginalType, nameof(Kelvin), Kelvin.ToString("#.##")));
        }
    }
}
