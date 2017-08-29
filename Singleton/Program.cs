using CommonClientLib;
using Singleton.Topics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Singleton
{
    class Program
    {
        private static TextParser TxtParser = new TextParser();
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();

        static void Main(string[] args)
        {
            var keepLooping = true;
            var (topicDictionary, topicNames) = GetTopics();

            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("    WELCOME TO THE ARGUMENT PROGRAM -- WHICH IS A PROGRAM THAT'S KIND OF FUN, AT LEAST AT FIRST MAYBE.");
            Console.WriteLine("**********************************************************************************************************\n");

            while (keepLooping)
            {
                PrintTopics(topicNames);

                keepLooping = ContinuationDeterminer.GoAgain();
            }
        }

        static (Dictionary<int, Type>, List<string>) GetTopics()
        {
            var topics = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => type.GetInterface(typeof(IArguable).ToString()) != null)
                    .ToList();

            var topicNames = new List<string>();
            topics.ForEach(topic =>
            {
                var nameArray = topic.ToString().Split('.');
                var nameString = nameArray[nameArray.Length - 1];
                var name = TxtParser.PascalToStringArray(nameString)[0];
                topicNames.Add(name);
            });

            topicNames.Sort();

            var key = 1;
            var topicDictionary = new Dictionary<int, Type>();
            topicNames.ForEach(name =>
            {
                var topic = topics
                    .Where(x => x.ToString().Contains(name))
                    .FirstOrDefault();

                topicDictionary.Add(key++, topic);
            });

            return (topicDictionary, topicNames);
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
