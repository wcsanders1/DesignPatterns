using Bridge.Formatters;
using System;

namespace Bridge.Converters
{
    public class Inches : AbstractConverter
    {
        const string ORIGINAL_TYPE = "Inches";

        private decimal Feet { get; set; }
        private decimal Meters { get; set; }
        private decimal Miles { get; set; }
        private string Kilometers { get; set; }
        private IFormatter Formatter { get; }

        public Inches(IFormatter formatter) : base(formatter)
        {
            Formatter = formatter;
        }

        public override void Convert(decimal amount)
        {
            Feet = amount * 12;
            Meters = amount * .0254M;
            Miles = amount / 63360;
            Kilometers = "whatever you feel like";

            Print();
        }

        private void Print()
        {
            Console.WriteLine(Formatter.GetString(ORIGINAL_TYPE, nameof(Feet), Feet.ToString("#.######")));
            Console.WriteLine(Formatter.GetString(ORIGINAL_TYPE, nameof(Meters), Meters.ToString("#.######")));
            Console.WriteLine(Formatter.GetString(ORIGINAL_TYPE, nameof(Miles), Miles.ToString("#.######")));
            Console.WriteLine(Formatter.GetString(ORIGINAL_TYPE, nameof(Kilometers), Kilometers));
            Console.WriteLine();
        }
    }
}
