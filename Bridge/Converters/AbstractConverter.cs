using Bridge.Formatters;

namespace Bridge.Converters
{
    public abstract class AbstractConverter
    {
        public AbstractConverter(IFormatter formatter) { }

        abstract public void Convert(decimal amount);
    }
}
