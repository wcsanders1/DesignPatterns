using Bridge.Converters;
using Bridge.Formatters;
using CommonClientLib;
using System;

namespace Bridge
{
    class Program
    {
        private static TextParser TxtParser = new TextParser();
        private static TypeParser TypParser = new TypeParser(TxtParser);
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();
        private const string INVALID_CHOICE_MESSAGE = "You entered an invalid choice. I'm not mad; just disappointed. Let's try again I guess.";

        static void Main(string[] args)
        {
            var keepLooping = true;

            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("    WELCOME TO THE BRIDGE PROGRAM -- WHICH IS A BORING PROGRAM THAT SORT OF DOES CONVERSIONS");
            Console.WriteLine("**********************************************************************************************************\n");

            while (keepLooping)
            {
                var (converters, converterNames) = TypParser.GetTypeDictionaryAndNameList<AbstractConverter>();
                Console.WriteLine("Enter the number of the measurement that you want to convert.");
                TxtParser.PrintStringList(converterNames);

                var strConverterChoice = Console.ReadLine();
                if (!TypParser.TryGetType(strConverterChoice, converters, out var converter))
                {
                    Console.WriteLine(INVALID_CHOICE_MESSAGE);
                    continue;
                }

                var (formatters, formatterNames) = TypParser.GetInstantiatedTypeDictionaryAndNameList<IFormatter>();
                Console.WriteLine("Enter the number of the way you want your conversion formatted.");
                TxtParser.PrintStringList(formatterNames);

                var strFormatterChoice = Console.ReadLine();
                if (!TypParser.TryGetType(strFormatterChoice, formatters, out var formatter))
                {
                    Console.WriteLine(INVALID_CHOICE_MESSAGE);
                    continue;
                }

                var instantiatedConverter = Activator.CreateInstance(converter, formatter);

                keepLooping = ContinuationDeterminer.GoAgain();
            }
        }
    }
}
