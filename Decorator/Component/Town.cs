using System;

namespace Decorator.Component
{
    public class Town : ILocation
    {
        private string Name { get; }
        private string Location { get; }

        public Town(string name, string location)
        {
            Name = name;
            Location = location;
        }

        public void PrintLocation()
        {
            Console.WriteLine($"{Name} is located in {Location}");
        }

        public void PrintName()
        {
            Console.WriteLine($"The name of this town is {Name}.");
        }
    }
}
