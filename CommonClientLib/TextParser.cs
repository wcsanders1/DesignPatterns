using System;
using System.Collections.Generic;
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

        public void PrintStringList(List<string> strings)
        {
            var key = 1;
            strings.ForEach(s =>
            {
                Console.WriteLine($"{key++}. {s}");
            });
            Console.WriteLine();
        }

        public string GetTextFromConsole()
        {
            var val = Console.ReadLine();
            Console.WriteLine();

            return val;
        }

        public string GetOrdinalSuffix(int digit)
        {
            var lastDigit = digit >= 10 ? digit / 10 : digit;
            switch (lastDigit)
            {
                case 1:
                    return "st";
                case 2:
                    return "nd";
                case 3:
                    return "rd";
                default:
                    return "th";
            }
        }

        public string Pluralize(int amount)
        {
            return amount > 1 ? "s" : string.Empty;
        }
    }
}
