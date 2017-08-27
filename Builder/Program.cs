using CommonClientLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Builder
{
    class Program
    {
        private static TextParser TxtParser = new TextParser();

        static void Main()
        {
            var keepLooping = true;
            const string invalidChoiceMessage = "\nThat isn't a number of one of the characters, which is what you were asked to provide, so let's try this again.\n";

            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("                  WELCOME TO THE CHARACTER BUILDER PROGRAM -- WHICH IS A PRETTY NEAT PROGRAM!");
            Console.WriteLine("**********************************************************************************************************\n");

            while (keepLooping)
            {
                Console.WriteLine("Enter the number of the character that you want to build.\n");

                var (characterBuilders, characterNames) = GetCharacterBuildersAndNames();
                PrintCharacters(characterNames);

                var choiceString = Console.ReadLine();

                if (!Int32.TryParse(choiceString, out var choice))
                {
                    Console.WriteLine($"{invalidChoiceMessage}");
                    continue;
                }

                if (!characterBuilders.TryGetValue(choice, out var builder))
                {
                    Console.WriteLine($"{invalidChoiceMessage}");
                    continue;
                }

                var characterBuilder = (AbstractCharacterBuilder)Activator.CreateInstance(builder);
                var character = CharacterMaker.GetCharacter(characterBuilder);

                DescribeCharacter(character);

                keepLooping = KeepGoing();
            }            
        }

        static void PrintCharacters(List<string> names)
        {
            var key = 1;
            foreach (var name in names)
            {
                Console.WriteLine($"{key}. {name}");
                key++;
            }
            Console.WriteLine();
        }

        static (Dictionary<int, Type>, List<string>) GetCharacterBuildersAndNames()
        {
            var characterBuilders = new Dictionary<int, Type>();
            var characters = AppDomain.CurrentDomain.GetAssemblies()
                                .SelectMany(assembly => assembly.GetTypes())
                                .Where(type => type.IsSubclassOf(typeof(AbstractCharacterBuilder)))
                                .ToList();

            var characterNames = new List<string>();
            characters.ForEach(character =>
            {
                var nameArray = character.ToString().Split('.');
                var nameString = nameArray[nameArray.Length - 1];
                var name = TxtParser.PascalToStringArray(nameString)[0];
                characterNames.Add(name);
            });

            characterNames.Sort();

            var key = 1;
            characterNames.ForEach(name =>
            {
                var builder = characters
                    .Where(x => x.ToString().Contains(name))
                    .FirstOrDefault();

                characterBuilders.Add(key, builder);
                key++;
            });

            return (characterBuilders, characterNames);
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

        static bool KeepGoing()
        {
            Console.WriteLine("\nPress 1 if you want to build another character, or press anything else to exit this program\n" +
                                "and do something else with the time you have here on this earth.\n");

            var result = Console.ReadLine();

            if (result == "1")
            {
                return true;
            }

            return false;
        }
    }
}
