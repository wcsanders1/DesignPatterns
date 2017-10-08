using System;
using System.Text;

namespace Bridge.Formatters
{
    public class Fancy : IFormatter
    {
        public string GetString(string originalValue, string valueName, string converted, string value)
        {
            var sb = new StringBuilder();
            sb.Append($"{new String('-', (Console.WindowWidth - 1))}\n");
            sb.Append($"{"Converted From".PadRight(Console.WindowWidth / 4)}");
            sb.Append($"{"Original Measurement".PadRight(Console.WindowWidth / 4)}");
            sb.Append($"{"Converted Into".PadRight(Console.WindowWidth / 4)}");
            sb.Append($"{"Converted Value".PadRight(Console.WindowWidth / 4)}\n");
            sb.Append($"{originalValue.PadRight(Console.WindowWidth / 4)}");
            sb.Append($"{valueName.PadRight(Console.WindowWidth / 4)}");
            sb.Append($"{converted.PadRight(Console.WindowWidth / 4)}");
            sb.Append($"{value.PadRight(Console.WindowWidth)}\n");

            return sb.ToString();
        }
    }
}
