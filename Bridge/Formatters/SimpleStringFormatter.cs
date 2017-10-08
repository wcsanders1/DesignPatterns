namespace Bridge.Formatters
{
    public class SimpleStringFormatter : IFormatter
    {
        public string GetString(string originalValue, string valueName, string converted, string value)
        {
            return $"{originalValue} {valueName} to {converted} is {value}";
        }
    }
}
