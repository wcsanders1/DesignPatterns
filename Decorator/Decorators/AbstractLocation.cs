using Decorator.Component;

namespace Decorator.Decorators
{
    public abstract class AbstractLocation : ILocation
    {
        public string Name { get; }
        public string Location { get; }

        protected ILocation Component;

        public AbstractLocation(ILocation component, string name, string location)
        {
            Component = component;
            Name = name;
            Location = location;
        }

        public virtual void PrintInfo()
        {
            Component.PrintInfo();
        }
    }
}
