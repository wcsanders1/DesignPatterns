using CommonClientLib;
using Singleton.Topics;
using System;
using System.Collections.Generic;
using Autofac;
using System.Linq;

namespace Singleton
{
    class Program
    {
        private static TextParser TxtParser                          = new TextParser();
        private static TypeParser TypParser                          = new TypeParser(TxtParser);
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();
        private static Arguer Arguer                                 = new Arguer(new Random());
        private static Container Container                           = new Container(new ContainerBuilder());

        private static string InvalidChoiceMessage = "\nThat isn't one of the available topics. So, let's try this again, eh?";

        static void Main(string[] args)
        {
            var keepLooping                   = true;
            var (topicDictionary, topicNames) = GetTypeDictionaryAndNameList<IArguable>();//TypParser.GetTypeDictionaryAndNameList<IArguable>();
            var topicContainer = Container.GetContainer(topicDictionary);

            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("    WELCOME TO THE ARGUMENT PROGRAM -- WHICH IS A PROGRAM THAT'S KIND OF FUN, AT LEAST AT FIRST MAYBE.");
            Console.WriteLine("**********************************************************************************************************\n");

            while (keepLooping)
            {
                Console.WriteLine("Enter the number of the topic on which you'd like to argue.");

                while (true)
                {
                    //var topicChosen = GetType(topicDictionary, topicNames);
                    //if (topicChosen == null)
                    //{
                    //    continue;
                    //}

                    var t = topicContainer.ResolveKeyed<IArguable>(1);

                    Console.WriteLine($"The topic is {t.Topic}");
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

        static (Dictionary<int, IArguable>, List<string>) GetTypeDictionaryAndNameList<T>() where T : class
        {
            var typeNames = new List<string>();
            var types = GetTypeList<T>();
            types.ForEach(type =>
            {
                var nameArray = type.ToString().Split('.');
                var nameString = nameArray[nameArray.Length - 1];
                var name = TxtParser.PascalToStringArray(nameString)[0];
                typeNames.Add(name);
            });

            typeNames.Sort();

            var key = 1;
            var typeDict = new Dictionary<int, IArguable>();
            typeNames.ForEach(name =>
            {
                var type = types.Where(x => x.ToString().Contains(name))
                                .FirstOrDefault();

                var tp = Activator.CreateInstance(type) as IArguable;

                typeDict.Add(key++, tp);
            });

            return (typeDict, typeNames);
        }

        static List<Type> GetTypeList<T>()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(assembly => assembly.GetTypes())
                            .Where(type =>
                            {
                                if (typeof(T).IsInterface)
                                {
                                    return type.GetInterface(typeof(T).ToString()) != null;
                                }

                                return type.IsSubclassOf(typeof(T));
                            })
                            .ToList();
        }
    }
}
