using System;
using Decorator.Component;

namespace Decorator.Decorators
{
    public class Planet : AbstractLocation
    {
        public string Weight { get; }
        public string Age { get; }

        public Planet(ILocation component, string name, string location, string weight, string age) : 
            base(component, name, location)
        {
            Weight = weight;
            Age = age;
        }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"{Component.Name} is on the planet of {Name}, " +
                $"which weighs {Weight} and has an age of {Age}.");
        }
    }
}
