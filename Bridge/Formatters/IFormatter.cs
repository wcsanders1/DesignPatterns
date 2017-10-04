namespace Bridge.Formatters
{
    public interface IFormatter
    {
        string Format(decimal key, decimal value);
    }
}
