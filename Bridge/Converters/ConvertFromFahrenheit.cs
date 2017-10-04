using Bridge.Formatters;

namespace Bridge.Converters
{
    public class ConvertFromFahrenheit : AbstractConverter
    {
        const decimal Div = 5 / 9;

        private decimal Celsius { get; set; }
        private decimal Kelvin { get; set; }

        public override void Convert(decimal fahrenheit)
        {
            Celsius = (fahrenheit - 32) * Div;
            Kelvin = (fahrenheit + 459.67M) * Div;
        }

        public override void Print(IFormatter formatter)
        {
            
        }
    }
}
