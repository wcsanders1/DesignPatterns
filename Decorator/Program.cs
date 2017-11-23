using CommonClientLib;
using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Decorator
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();
        private static TextParser TxtParser = new TextParser();
        private static TypeParser TypParser = new TypeParser(TxtParser);
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();
        private static QuestionAsker QuestionAsker = new QuestionAsker();

        private const string InfoFile = "LocationInfo.json";

        static void Main(string[] args)
        {
            TxtPrinter.PrintInformation("WELCOME TO THE DECORATOR PROGRAM -- WHICH IS SORT OF COOL, I GUESS");

            while(true)
            {
                JObject locationInfo;
                try
                {
                    using (var reader = new StreamReader(InfoFile))
                    {
                        var json = reader.ReadToEnd();
                        locationInfo = JObject.Parse(json);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unable to parse {InfoFile}.");
                    Console.WriteLine($"Exception message: {ex.Message}");
                    Environment.Exit(1);
                }



                if (!ContinuationDeterminer.GoAgain())
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
