using System;
using Decorator.Component;

namespace Decorator.Decorators
{
    public class Universe : AbstractLocation
    {
        public string Size { get; }

        public Universe(ILocation component, string name, string location, string size) : 
            base(component, name, location)
        {
            Size = size;
        }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"{Component.Name} is in the {Name} universe, " +
                $"which has a size of {Size}.");
        }
    }
}
