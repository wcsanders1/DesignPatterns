using System;
using System.Collections.Generic;
using System.Text;
using Decorator.Component;

namespace Decorator.Decorators
{
    public class Galaxy : AbstractLocation
    {
        public string StarNumber { get; }
        public string Size { get; }

        public Galaxy(ILocation component, string name, string location, string starNumber, string size) :
            base(component, name, location)
        {
            StarNumber = starNumber;
            Size = size;
        }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"{Component.Name} is in the {Name} galaxy, " +
                $"which has {StarNumber} stars and has a size of {Size}.");
        }
    }
}
