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
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();
        private static readonly List<string> XmlOrJson = new List<string>
        {
            "xml",
            "json"
        };

        private const string TypeObjectQuestion = "What type of object do you want to make?";

        static void Main(string[] args)
        {
            while (true)
            {
                var typeObjectChoice = XmlOrJson[Asker.GetChoiceFromList(TypeObjectQuestion, XmlOrJson)];
                XDocument xDocument;
                JObject jObject;

                switch (typeObjectChoice)
                {
                    case "xml":
                        xDocument = XmlJsonUtil.GetXml();
                        break;
                    case "json":
                        jObject = XmlJsonUtil.GetJson();
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
