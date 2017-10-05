using Bridge.Formatters;
using System;

namespace Bridge.Converters
{
    public class ConvertFromInches : AbstractConverter
    {
        const string OriginalType = "Inches";

        private decimal Feet { get; set; }
        private decimal Meters { get; set; }
        private decimal Miles { get; set; }
        private string Kilometers { get; set; }
        private IFormatter Formatter { get; }

        public ConvertFromInches(IFormatter formatter) : base(formatter)
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
            Console.WriteLine(Formatter.GetString(OriginalType, nameof(Feet), Feet.ToString("#.######")));
            Console.WriteLine(Formatter.GetString(OriginalType, nameof(Meters), Meters.ToString("#.######")));
            Console.WriteLine(Formatter.GetString(OriginalType, nameof(Miles), Miles.ToString("#.######")));
            Console.WriteLine(Formatter.GetString(OriginalType, nameof(Kilometers), Kilometers));
        }
    }
}
