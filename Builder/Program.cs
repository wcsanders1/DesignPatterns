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

                PrintCharacters();

                keepLooping = KeepGoing();
            }            
        }

        static void PrintCharacters()
        {
            var characters = AppDomain.CurrentDomain.GetAssemblies()
                                .SelectMany(assembly => assembly.GetTypes())
                                .Where(type => type.IsSubclassOf(typeof(AbstractCharacterBuilder)))
                                .ToList();

            characters.ForEach(character =>
            {
                Console.WriteLine($"{character.ToString()}\n");
            });
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
