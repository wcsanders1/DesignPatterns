using System;
using System.Text.RegularExpressions;

namespace CommonClientLib
{
    public class TextParser
    {
        public string CleanString(string s)
        {
            return Regex.Replace(s, @"\s+", "");
        }

        public string PascalToString(string s)
        {
            var cleanedString = CleanString(s);
            return Regex.Replace(cleanedString, "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1 ");
        }

        public string[] PascalToStringArray(string s)
        {
            return PascalToString(s).Split(' ');
        }

        public void PrintEnum<T>() where T : struct
        {
            var sorters = Enum.GetValues(typeof(T));
            foreach (var sorter in sorters)
            {
                var sorterName = Regex.Replace(sorter.ToString(), "(\\B[A-Z])", " $1");
                Console.WriteLine($"{(int)sorter}. {sorterName}");
            }
            Console.WriteLine();
        }
    }
}
