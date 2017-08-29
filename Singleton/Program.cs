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

        static void Main(string[] args)
        {
            var keepLooping                   = true;
            var (topicDictionary, topicNames) = TypParser.GetTypeDictionaryAndNameList<IArguable>();

            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("    WELCOME TO THE ARGUMENT PROGRAM -- WHICH IS A PROGRAM THAT'S KIND OF FUN, AT LEAST AT FIRST MAYBE.");
            Console.WriteLine("**********************************************************************************************************\n");

            while (keepLooping)
            {
                PrintTopics(topicNames);

                keepLooping = ContinuationDeterminer.GoAgain();
            }
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
