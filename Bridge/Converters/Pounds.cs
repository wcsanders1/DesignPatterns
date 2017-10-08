using Bridge.Formatters;
using System;

namespace Bridge.Converters
{
    public class Pounds : AbstractConverter
    {
        const string ORIGINAL_TYPE = "Pounds";

        private decimal OriginalValue { get; set; }
        private decimal Kilograms { get; set; }
        private decimal Ounces { get; set; }
        private decimal Stone { get; set; }
        private IFormatter Formatter { get; }

        public Pounds(IFormatter formatter) : base(formatter)
        {
            Formatter = formatter;
        }

        public override void Convert(decimal amount)
        {
            OriginalValue = amount;
            Kilograms = amount * .45359237M;
            Ounces = amount * 16M;
            Stone = amount * .0714286M;

            Print();
        }

        private void Print()
        {
            Console.WriteLine(Formatter.GetString(OriginalValue.ToString(), ORIGINAL_TYPE, nameof(Kilograms), Kilograms.ToString("#.######")));
            Console.WriteLine(Formatter.GetString(OriginalValue.ToString(), ORIGINAL_TYPE, nameof(Ounces), Ounces.ToString("#.######")));
            Console.WriteLine(Formatter.GetString(OriginalValue.ToString(), ORIGINAL_TYPE, nameof(Stone), Stone.ToString("#.######")));
            Console.WriteLine();
        }
    }
}
