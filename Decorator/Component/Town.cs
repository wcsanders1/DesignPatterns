using System;
using CommonClientLib;

namespace Decorator.Component
{
    public class Town : ILocation
    {
        public string Name { get; }
        public string Location { get; }

        private int Population { get; }

        public Town(string name, string location, int population)
        {
            Name = name;
            Location = location;
            Population = population;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"\nThe name of this town is {Name}.");
            Console.WriteLine($"{Name} has a population of {Population.ToStringWithComma()}.");
        }
    }
}
