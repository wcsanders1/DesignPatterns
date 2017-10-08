namespace Bridge.Formatters
{
    public interface IFormatter
    {
        string GetString(string originalValue, string valueName, string converted, string value);
    }
}
