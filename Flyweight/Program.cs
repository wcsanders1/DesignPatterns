using CommonClientLib;
using System;
using System.Collections.Generic;

namespace Flyweight
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();
        private static TextParser TxtParser = new TextParser();
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();
        private static QuestionAsker Asker = new QuestionAsker();
        private static readonly List<string> YesOrNo = new List<string>
        {
            "Yes",
            "No"
        };

        static void Main(string[] args)
        {
            TxtPrinter.PrintInformation("WELCOME TO THE FLYWEIGHT PROGRAM -- WHICH SORT OF HAS SOME INTERESTING ASPECTS");

            while (true)
            {
                var characterFactory = CreateCharacters();
                var stringToManipulate = GetString();

                foreach (var character in stringToManipulate)
                {
                    var qualifiedCharacter = characterFactory.GetCharacter(character);
                    qualifiedCharacter.Render(Console.CursorTop + 1);
                }

                if (!ContinuationDeterminer.GoAgain())
                {
                    Environment.Exit(0);
                }
            }
        }

        private static string GetString()
        {
            Console.WriteLine("\nGo ahead and type some stuff, maybe a story or something, whatever... Press Enter when you're finished.\n");
            return Console.ReadLine();
        }

        private static CharacterFactory CreateCharacters()
        {
            var factory = new CharacterFactory();
            var choice = YesOrNo[Asker.GetChoiceFromList("Are there any characters for which you want custom coloring?",
                YesOrNo)];

            switch (choice)
            {
                case "No":
                    return factory;
                case "Yes":
                    makeCustomCharacters();   
                    break;
                default:
                    return null;
            }

            return factory;
            
            void makeCustomCharacters()
            {
                while (true)
                {
                    var character = GetCharacter();
                    var foregroundColor = GetColor($"\nWhat do you want the foreground color of {character} to be?");
                    var backgroundColor = GetColor($"\nWhat do you want the background color of {character} to be?");

                    factory.SetCharacter(character, foregroundColor, backgroundColor);

                    var makeAnotherCharacter = YesOrNo[Asker.GetChoiceFromList("Are there any other characters for which you want custom coloring?",
                        YesOrNo)];

                    switch (makeAnotherCharacter)
                    {
                        case "Yes":
                            continue;
                        default:
                            return;
                    }
                }
            }
        }
        
        private static char GetCharacter()
        {
            Console.WriteLine("\nWhat character would you like to provide custom coloring for?\n");

            while (true)
            {
                if (!char.TryParse(Console.ReadLine(), out var character))
                {
                    Console.WriteLine("\nThat's not a valid character. Try again.\n");
                    continue;
                }

                return character;
            }
        }

        private static ConsoleColor GetColor(string question = null)
        {
            if (question != null)
            {
                Console.WriteLine(question);
            }

            Console.WriteLine("Choose a color from the following selection: \n");
            TxtParser.PrintEnum<ConsoleColor>();

            while (true)
            {
                if (!Int32.TryParse(Console.ReadLine(), out int colorChosen))
                {
                    Console.WriteLine("That input isn't valid. Enter the number of the color you want.");
                    continue;
                }

                if (!Enum.IsDefined(typeof(ConsoleColor), colorChosen))
                {
                    Console.WriteLine("That isn't a valid option. Choose a color from the list.");
                    continue;
                }

                return (ConsoleColor)colorChosen;
            }
        }
    }
}
