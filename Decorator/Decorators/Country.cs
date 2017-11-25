using System;
using Decorator.Component;

namespace Decorator.Decorators
{
    public class Country : AbstractLocation
    {
        public string Anthem { get; }

        public Country(ILocation component, string name, string location, string anthem) : 
            base(component, name, location)
        {
            Anthem = anthem;
        }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"{Component.Name} is in the country of {Name}, " +
                $"whose national anthem is {Anthem}.");
        }
    }
}
