using CommonClientLib;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Facade
{
    class Program
    {
        private static XmlJsonUtility XmlJsonUtil = new XmlJsonUtility();
        private static QuestionAsker Asker = new QuestionAsker();
        private static TextPrinter TxtPrinter = new TextPrinter();
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();
        private static readonly List<string> XmlOrJson = new List<string>
        {
            "xml",
            "json"
        };

        private const string TypeObjectQuestion = "What type of object do you want to make?";

        static void Main(string[] args)
        {
            TxtPrinter.PrintInformation("WELCOME TO THE FACADE PROGRAM -- WHICH DOES SOME MILDLY INTERESTING THINGS");

            while (true)
            {
                var typeObjectChoice = XmlOrJson[Asker.GetChoiceFromList(TypeObjectQuestion, XmlOrJson)];
                XDocument xDocument;
                JObject jObject;

                switch (typeObjectChoice)
                {
                    case "xml":
                        xDocument = XmlJsonUtil.GetXml();

                        TxtPrinter.PrintInformation("Here is the final xml:", '-', ConsoleColor.DarkRed);
                        Console.WriteLine($"\n{xDocument}\n");

                        var convertedToJObject = XmlJsonUtil.ConvertXmlToJson(xDocument);
                        TxtPrinter.PrintInformation("And here is the xml converted to json:", '-', ConsoleColor.DarkRed);
                        Console.WriteLine($"\n{convertedToJObject}\n");

                        break;
                    case "json":
                        jObject = XmlJsonUtil.GetJson();

                        TxtPrinter.PrintInformation("Here is the final json:", '-', ConsoleColor.DarkRed);
                        Console.WriteLine($"\n{jObject}\n");

                        var convertedToXDocument = XmlJsonUtil.ConvertJsonToXml(jObject);
                        TxtPrinter.PrintInformation("And here is the json converted to xml:", '-', ConsoleColor.DarkRed);
                        Console.WriteLine($"\n{convertedToXDocument}\n");

                        break;
                    default:
                        break;
                }

                if (!ContinuationDeterminer.GoAgain())
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
