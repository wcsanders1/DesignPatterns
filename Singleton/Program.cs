using CommonClientLib;
using Singleton.Topics;
using System;
using System.Collections.Generic;
using Autofac;

namespace Singleton
{
    class Program
    {
        private static TextParser TxtParser = new TextParser();
        private static TypeParser TypParser = new TypeParser(TxtParser);
        private static Arguer Arguer        = new Arguer(new Random());
        private static Container Container  = new Container(new ContainerBuilder());
        private static ForOrAgainst CurrentPosition;

        private static string InvalidChoiceMessage = "\nThat isn't one of the available topics. So, let's try this again, eh?";

        static void Main(string[] args)
        {
            var keepLooping                   = true;
            var (topicDictionary, topicNames) = TypParser.GetInstantiatedTypeDictionaryAndNameList<IArguable>();
            var topicContainer                = Container.GetContainer(topicDictionary);

            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("    WELCOME TO THE ARGUMENT PROGRAM -- WHICH IS A PROGRAM THAT'S KIND OF FUN, AT LEAST AT FIRST MAYBE.");
            Console.WriteLine("**********************************************************************************************************\n");

            while (keepLooping)
            {
                var topic = GetTopic(topicNames, topicContainer);
                if (topic == null)
                {
                    break;
                }

                Arguer.Topic = topic;

                switch (Argue(topic))
                {
                    case Choice.ChangeTopic:
                        continue;
                    default:
                        keepLooping = false;
                        break;
                }
            }
        }

        static IArguable GetTopic(List<string> topicNames, IContainer topicContainer)
        {
            while (true)
            {
                Console.WriteLine("\nEnter the number of the topic on which you'd like to argue, or press 0 to exit the program.");
                TxtParser.PrintStringList(topicNames);
                
                if (!Int32.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine(InvalidChoiceMessage);
                    continue;
                }

                if (choice == 0)
                {
                    return null;
                }

                try
                {
                    using (var scope = topicContainer.BeginLifetimeScope())
                    {
                        return scope.ResolveKeyed<IArguable>(choice);
                    }
                }
                catch
                {
                    Console.WriteLine(InvalidChoiceMessage);
                }
            }
        }

        static ForOrAgainst GetPosition(string topic)
        {
            while (true)
            {
                Console.WriteLine($"\nChose the position you will take with regard to the topic of {topic}.");
                TxtParser.PrintEnum<ForOrAgainst>();

                if (!Int32.TryParse(Console.ReadLine(), out int positionChosen) ||
                    !Enum.IsDefined(typeof(ForOrAgainst), positionChosen))
                {
                    Console.WriteLine("That input isn't valid. Let's try again.\n");
                    continue;
                }

                return (ForOrAgainst)positionChosen;
            }
        }

        static Choice Argue(IArguable topic)
        {
            CurrentPosition = GetPosition(topic.Topic);

            while (true)
            {
                Console.WriteLine($"\nType your argument {CurrentPosition.ToString()} {topic.Topic} here, or:");
                Console.WriteLine($"enter P to change your position on the topic of {topic.Topic}, X to choose another topic, 0 to quit.\n");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        return Choice.Quit;
                    case "X":
                        return Choice.ChangeTopic;
                    case "P":
                        CurrentPosition = GetPosition(topic.Topic);
                        break;
                    default:
                        Console.WriteLine($"{Arguer.GetArgument(new Argument { Position = CurrentPosition })}\n");
                        break;
                }
            }
        }
    }
}
