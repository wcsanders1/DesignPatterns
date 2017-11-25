using System;
using Decorator.Component;

namespace Decorator.Decorators
{
    public class Continent : AbstractLocation
    {
        public int CountryAmount { get; }
        public string Size { get; }

        public Continent(ILocation component, string name, string location, int countryAmount, string size) :
            base(component, name, location)
        {
            CountryAmount = countryAmount;
            Size = size;
        }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"{Component.Name} is in the continent of {Name}, " +
                $"which has {CountryAmount} countries in it, and has a size of {Size}.");
        }
    }
}
