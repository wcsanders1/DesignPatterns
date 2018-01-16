using CommonClientLib;
using System;
using System.Collections.Generic;

namespace Flyweight
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();
        private static QuestionAsker Asker = new QuestionAsker();
        private static readonly List<string> YesOrNo = new List<string>
        {
            "Yes",
            "No"
        };

        static void Main(string[] args)
        {
            TxtPrinter.PrintInformation("WELCOME TO THE FLYWEIGHT PROGRAM -- WHICH HAS SOME INTERESTING ASPECTS");

            while (true)
            {
                var stringToManipulate = GetString();

                if (!ContinuationDeterminer.GoAgain())
                {
                    Environment.Exit(0);
                }
            }
        }

        public static string GetString()
        {
            Console.WriteLine("Go ahead and type some stuff, maybe a story or something, whatever... Press Enter when you're finished.\n");
            return Console.ReadLine();
        }

        public CharacterFactory CreateCharacters()
        {
            var factory = new CharacterFactory();
            var choice = YesOrNo[Asker.GetChoiceFromList("Are there any characters in what you just wrote for which you want to provide custom coloring?",
                YesOrNo)];

            switch (choice)
            {
                case "No":
                    return factory;
                case "Yes":
                    return null;
                default:
                    return null;
            }
            
        }

        //public static (char, ConsoleColor, ConsoleColor) GetCharacterColors()
        //{

        //}
    }
}
