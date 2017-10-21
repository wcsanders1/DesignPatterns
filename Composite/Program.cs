using CommonClientLib;
using System;

namespace Composite
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();
        private static TextParser TxtParser = new TextParser();
        private static TypeParser TypParser = new TypeParser(TxtParser);
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();
        
        private const int NAME_LENGTH_LIMIT = 10;

        static void Main(string[] args)
        {
            TxtPrinter.PrintAppTitle("WELCOME TO THE COMPOSITE PROGRAM -- WHICH IS A SOMEWHAT INTERESTING PROGRAM");

            while (true)
            {
                var (decedentName, estateValue) = GetDecedentNameAndEstateValue();
                if (decedentName == null || estateValue == 0)
                {
                    Environment.Exit(0);
                }

                if (!ContinuationDeterminer.GoAgain())
                {
                    Environment.Exit(0);
                }
            }
        }

        private static (string, decimal) GetDecedentNameAndEstateValue()
        {
            if (!TryGetDecedentName(out var decedentName))
            {
                return (null, 0);
            }

            if (!TryGetEstateValue(out var estateValue))
            {
                return (null, 0);
            }

            return (decedentName, estateValue);           
        }

        private static bool TryGetDecedentName(out string decedentName)
        {
            while (true)
            {
                Console.WriteLine($"What is the decedent's name? (Enter no more than {NAME_LENGTH_LIMIT} characters.)");

                decedentName = TxtParser.GetTextFromConsole();
                if (!NameIsValid(decedentName))
                {
                    if (!ContinuationDeterminer.GoAgainWithInvalidInputMessage())
                    {
                        return false;
                    }
                    continue;
                }

                return true;
            }
        }

        private static bool TryGetEstateValue(out decimal estateValue)
        {
            while (true)
            {
                Console.WriteLine($"What is the value of the decedent's estate? (Enter a number.)");

                var estateValuelStr = TxtParser.GetTextFromConsole();
                if (!decimal.TryParse(estateValuelStr, out estateValue))
                {
                    if (!ContinuationDeterminer.GoAgainWithInvalidInputMessage())
                    {
                        return false;
                    }
                    continue;
                }

                return true;
            }
        }

        private static bool NameIsValid(string name)
        {
            return name != null &&
                   name.Length > 0 &&
                   name.Length <= NAME_LENGTH_LIMIT;
        }
    }
}
