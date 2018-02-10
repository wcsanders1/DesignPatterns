namespace Decorator.Component
{
    public interface ILocation
    {
        string Name {get;}
        string Location { get; }
        void PrintInfo();
    }
}
