using CommonClientLib;
using System;
using System.Text;

namespace Builder
{
    class Program
    {
        private static TextParser TxtParser                          = new TextParser();
        private static TypeParser TypParser                          = new TypeParser(TxtParser);
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();

        static void Main()
        {
            var keepLooping                   = true;
            const string invalidChoiceMessage = "\nThat isn't a number of one of the characters, which is what you were asked to provide, so let's try this again.\n";

            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("                  WELCOME TO THE CHARACTER BUILDER PROGRAM -- WHICH IS A PRETTY NEAT PROGRAM!");
            Console.WriteLine("**********************************************************************************************************\n");

            while (keepLooping)
            {
                Console.WriteLine("Enter the number of the character that you want to build.\n");

                var (characterBuilders, characterNames) = TypParser.GetInstantiatedTypeDictionaryAndNameList<AbstractCharacterBuilder>();
                TxtParser.PrintStringList(characterNames);

                var choiceString = Console.ReadLine();

                if (!TypParser.TryGetType(choiceString, characterBuilders, out var builder))
                {
                    Console.WriteLine(invalidChoiceMessage);
                    continue;
                }

                var character = CharacterMaker.GetCharacter(builder);

                DescribeCharacter(character);

                keepLooping = ContinuationDeterminer.GoAgain();
            }            
        }

        static void DescribeCharacter(Character character)
        {
            string article;
            if ("aeiou".IndexOf(character.Name[0].ToString(), StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                article = "an";
            }
            else
            {
                article = "a";
            }

            Console.WriteLine($"\nYou chose to build {article} {character.Name} character.\n");

            foreach (var prop in character.GetType().GetProperties())
            {
                if (prop.Name == "Name")
                {

                    continue;
                }

                string toBe;
                if (prop.PropertyType == typeof(bool))
                {
                    if ((bool)prop.GetValue(character))
                    {
                        toBe = "is";
                    }
                    else
                    {
                        toBe = "is not";
                    }

                    var stringArray = TxtParser.PascalToStringArray(prop.Name);
                    Console.WriteLine($"This person {toBe} {stringArray[1]}.");
                    continue;
                }

                var propValueArray = prop.GetValue(character, null).ToString().Split(',');
                var propValueString = GetFormattedString(propValueArray);

                Console.WriteLine($"This person's {TxtParser.PascalToString(prop.Name)} status consists of {propValueString}.");
            }
        }

        static string GetFormattedString(string[] stringArray)
        {
            if (stringArray.Length <= 1)
            {
                return TxtParser.PascalToString(stringArray[0]);
            }

            if (stringArray.Length == 2)
            {
                return $"{TxtParser.PascalToString(stringArray[0])} and {TxtParser.PascalToString(stringArray[1])}";
            }

            var result = new StringBuilder();
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (i == stringArray.Length - 1)
                {
                    result.Append($"and {TxtParser.PascalToString(stringArray[i])}");

                    return result.ToString();
                }

                result.Append($"{TxtParser.PascalToString(stringArray[i])}, ");
            }

            return result.ToString();
        }
    }
}
