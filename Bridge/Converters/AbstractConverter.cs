using Bridge.Formatters;

namespace Bridge.Converters
{
    public abstract class AbstractConverter
    {
        abstract public void Convert(decimal amount);
        abstract public void Print(IFormatter formatter);
    }
}
