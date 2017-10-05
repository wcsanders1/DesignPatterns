namespace Bridge.Formatters
{
    public class SimpleStringFormatter : IFormatter
    {
        public string GetString(string original, string converted, string value)
        {
            return $"{original} to {converted} is {value}";
        }
    }
}
