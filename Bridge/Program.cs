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
        private const string INVALID_CHOICE_MESSAGE = "You entered an invalid choice. I'm not mad; just disappointed. Let's try again I guess.\n";

        static void Main(string[] args)
        {
            var keepLooping = true;

            Console.WriteLine("**********************************************************************************************************");
            Console.WriteLine("    WELCOME TO THE BRIDGE PROGRAM -- WHICH IS A BORING PROGRAM THAT SORT OF DOES CONVERSIONS");
            Console.WriteLine("**********************************************************************************************************\n");

            while (keepLooping)
            {
                var (converters, converterNames) = TypParser.GetTypeDictionaryAndNameList<AbstractConverter>();
                Console.WriteLine("Enter the number of the measurement from which you want to convert.");
                TxtParser.PrintStringList(converterNames);

                var strConverterChoice = TxtParser.GetTextFromConsole();
                if (!TypParser.TryGetType(strConverterChoice, converters, out var converter))
                {
                    Console.WriteLine(INVALID_CHOICE_MESSAGE);
                    continue;
                }

                var (formatters, formatterNames) = TypParser.GetInstantiatedTypeDictionaryAndNameList<IFormatter>();
                Console.WriteLine("Enter the number of the way you want the output formatted.");
                TxtParser.PrintStringList(formatterNames);

                var strFormatterChoice = TxtParser.GetTextFromConsole();
                if (!TypParser.TryGetType(strFormatterChoice, formatters, out var formatter))
                {
                    Console.WriteLine(INVALID_CHOICE_MESSAGE);
                    continue;
                }

                var instantiatedConverter = Activator.CreateInstance(converter, formatter) as AbstractConverter;

                Console.WriteLine("Enter the value that you want converted");

                var strValue = TxtParser.GetTextFromConsole();
                if (!decimal.TryParse(strValue, out var value))
                {
                    Console.WriteLine("That's not a value that can be converted. Let's try this again I guess.");
                    continue;
                }

                instantiatedConverter.Convert(value);

                keepLooping = ContinuationDeterminer.GoAgain();
            }
        }
    }
}
