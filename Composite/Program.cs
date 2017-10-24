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
        private static QuestionAsker QuestionAsker = new QuestionAsker();

        private const int NAME_LENGTH_LIMIT = 10;
        private static readonly string NAME_LENGTH_LIMIT_MESSAGE = $"(Enter no more than {NAME_LENGTH_LIMIT} characters).";

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

                decedent.Descendants = GetDescendants(decedent.Name, tree);
                decedent.DistributeEstate();

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
                Console.WriteLine($"What is the decedent's name? {NAME_LENGTH_LIMIT_MESSAGE}");

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

                if (!decimal.TryParse(TxtParser.GetTextFromConsole(), out estateValue))
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

        private static List<Descendant> GetDescendants(string decedentName, Tree<String> tree)
        {
            var descendants = new List<Descendant>();
            int numDescendants = GetNumberOfDescendants(decedentName);
            
            for (int i = 1; i <= numDescendants; i++)
            {
                string descendantName = TryAddDescendantToTree(decedentName, i, tree);
                tree.PrintTree();

                var isDeceased = QuestionAsker.IsTrueOrFalse($"Is {descendantName} deceased?");
                List<Descendant> descendantsOfDescendant = new List<Descendant>();
                if (isDeceased)
                {
                    descendantsOfDescendant = GetDescendants(descendantName, tree);
                }

                var descendant = new Descendant()
                {
                    Name = descendantName,
                    Deceased = isDeceased,
                    Descendants = descendantsOfDescendant.Count <= 0 ? null : descendantsOfDescendant
                };

                descendants.Add(descendant);
            }

            return descendants;
        }

        private static string TryAddDescendantToTree(string decedentName, int i, Tree<string> tree)
        {
            while (true)
            {
                var descendantName = GetDescendantName(decedentName, i);
                if (!tree.TryAddNode(descendantName))
                {
                    if (!ContinuationDeterminer.GoAgainWithInvalidInputMessage("That name already exists and, therefore, is invalid."))
                    {
                        Environment.Exit(0);
                    }
                    continue;
                }

                return descendantName;
            }
        }

        private static int GetNumberOfDescendants(string decedentName)
        {
            while (true)
            {
                Console.WriteLine($"How many descendants does {decedentName} have?");
                if (!int.TryParse(TxtParser.GetTextFromConsole(), out var numDescendants))
                {
                    if (!ContinuationDeterminer.GoAgainWithInvalidInputMessage())
                    {
                        Environment.Exit(0);
                    }
                    continue;
                }

                return numDescendants;
            }
        }

        private static string GetDescendantName(string decedentName, int i)
        {
            var suffix = TxtParser.GetOrdinalSuffix(i);
            string descendantName;
            while (true)
            {
                Console.WriteLine($"What is {decedentName}'s {i}{suffix} descendant's name? {NAME_LENGTH_LIMIT_MESSAGE}");

                descendantName = TxtParser.GetTextFromConsole();
                if (!NameIsValid(descendantName))
                {
                    if (!ContinuationDeterminer.GoAgainWithInvalidInputMessage())
                    {
                        Environment.Exit(0);
                    }
                    continue;
                }
                return descendantName;
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
