using Bridge.Converters;
using CommonClientLib;
using System;

namespace Bridge
{
    class Program
    {
        private static TextParser TxtParser = new TextParser();
        private static TypeParser TypParser = new TypeParser(TxtParser);
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();

        static void Main(string[] args)
        {
            var keepLooping = true;

            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("                  WELCOME TO THE BRIDGE PROGRAM -- WHICH IS A BORING PROGRAM THAT DOES CONVERSIONS");
            Console.WriteLine("**********************************************************************************************************\n");

            while (keepLooping)
            {
                var (converters, converterNames) = TypParser.GetInstantiatedTypeDictionaryAndNameList<AbstractConverter>();
                Console.WriteLine("Enter the number of the measurement that you want to convert.");
                TxtParser.PrintStringList(converterNames);


                //TODO: Make method in the library that returns list of non-instantiated types, because the converters need 
                //      arguments in their constructors.

                var strChoice = Console.ReadLine();
                if (!TypParser.TryGetType(strChoice, converters, out var converter))
                {
                    Console.WriteLine("You entered an invalid choice. I'm not mad; just disappointed. Let's try again I guess.");
                    continue;
                }

                keepLooping = ContinuationDeterminer.GoAgain();
            }
        }
    }
}
