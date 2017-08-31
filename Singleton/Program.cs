using CommonClientLib;
using Singleton.Topics;
using System;
using System.Collections.Generic;

namespace Singleton
{
    class Program
    {
        private static TextParser TxtParser                          = new TextParser();
        private static TypeParser TypParser                          = new TypeParser(TxtParser);
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();
        private static Arguer Arguer                                 = new Arguer(new Random());

        private static string InvalidChoiceMessage = "\nThat isn't one of the available topics. So, let's try this again, eh?";

        static void Main(string[] args)
        {
            var keepLooping                   = true;
            var (topicDictionary, topicNames) = TypParser.GetTypeDictionaryAndNameList<IArguable>();

            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("    WELCOME TO THE ARGUMENT PROGRAM -- WHICH IS A PROGRAM THAT'S KIND OF FUN, AT LEAST AT FIRST MAYBE.");
            Console.WriteLine("**********************************************************************************************************\n");

            while (keepLooping)
            {
                Console.WriteLine("Enter the number of the topic on which you'd like to argue.");

                while (true)
                {
                    var topicChosen = GetType(topicDictionary, topicNames);
                    if (topicChosen == null)
                    {
                        continue;
                    }

                    var activatedTopic = ActivateTopic(topicChosen);
                }

                keepLooping = ContinuationDeterminer.GoAgain();
            }
        }

        static Type GetType(Dictionary<int, Type> typeDict, List<string> typeNames)
        {
            PrintTopics(typeNames);
            if (!Int32.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("That's not a valid choice. We'll try this again I guess.");
                return null;
            }

            if (!typeDict.TryGetValue(choice, out var topicChosen))
            {
                Console.WriteLine(InvalidChoiceMessage);
                return null;
            }

            return topicChosen;
        }

        static IArguable ActivateTopic(Type topic)
        {


            return null;
        }

        static void PrintTopics(List<string> topics)
        {
            var key = 1;
            foreach (var topic in topics)
            {
                Console.WriteLine($"{key}. {topic}");
                key++;
            }
            Console.WriteLine();
        }
    }
}
