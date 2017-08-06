using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            var keepLooping = true;

            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("                  WELCOME TO THE CHARACTER BUILDER PROGRAM -- WHICH IS A PRETTY NEAT PROGRAM!");
            Console.WriteLine("**********************************************************************************************************\n");

            while (keepLooping)
            {
                Console.WriteLine("Enter the number of the character that you want to build.");

                var (characterBuilders, characterNames) = GetCharacterBuildersAndNames();
                PrintCharacters(characterNames);

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
                var name = nameArray[nameArray.Length - 1];
                characterNames.Add(name);
            });

            characterNames.Sort();

            var key = 1;
            characterNames.ForEach(name =>
            {
                var builder = characters.Where(x => x.ToString().Contains(name)).FirstOrDefault();
                characterBuilders.Add(key, builder);
                key++;
            });

            return (characterBuilders, characterNames);
        }

        static bool KeepGoing()
        {
            Console.WriteLine("Press 1 if you want to build another character, or press anything else ");
            var result = Console.ReadLine();

            if (result == "1")
            {
                return true;
            }

            return false;
        }
    }
}
