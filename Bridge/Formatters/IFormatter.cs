namespace Bridge.Formatters
{
    public interface IFormatter
    {
        string GetString(string original, string converted, string value);
    }
}
