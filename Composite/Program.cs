using CommonClientLib;
using System;
using System.Collections.Generic;
using System.Globalization;

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

                var decedent = new Decedent(decedentName, estateValue);
                var tree = new Tree<string>(decedentName, new string[] 
                {
                    $"Estate value: ${estateValue.ToString("#.00", CultureInfo.InvariantCulture)}"
                });

                tree.PrintTree();
                Console.WriteLine();


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

            if (!TryGetEstateValue(decedentName, out var estateValue))
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

        private static bool TryGetEstateValue(string decedentName, out decimal estateValue)
        {
            while (true)
            {
                Console.WriteLine($"What is the value of {decedentName}'s estate? (Enter a number.)");

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

        private static void GetDescendants(Decedent decedent, Tree<String> tree)
        {
            int numDescendants;
            while (true)
            {
                Console.WriteLine($"How many descendants does {decedent.Name} have?");
                if (!int.TryParse(Console.ReadLine(), out numDescendants))
                {
                    if (!ContinuationDeterminer.GoAgainWithInvalidInputMessage())
                    {
                        Environment.Exit(0);
                    }
                }

                break;
            }

            for (int i = 1; i <= numDescendants; i++)
            {
                var lastDigit = i >= 10 ? i / 10 : i;
                string suffix;
                switch (lastDigit)
                {
                    case 1:
                        suffix = "st";
                        break;
                    case 2:
                        suffix = "nd";
                        break;
                    case 3: suffix = "rd";
                        break;
                    default:
                        suffix = "th";
                        break;
                }

                Console.WriteLine($"What is {decedent.Name}'s {i}{suffix} descendant's name?");
                
            }
        }

        private static void GetChildrenOfDescendants(List<Descendant> descendants, Tree<String> tree)
        {

        }

        private static bool NameIsValid(string name)
        {
            return name != null &&
                   name.Length > 0 &&
                   name.Length <= NAME_LENGTH_LIMIT;
        }
    }
}
