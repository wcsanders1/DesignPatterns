using CommonClientLib;
using Newtonsoft.Json.Linq;
using Observer.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Observer
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();
        private static ContinuationDeterminer ContinuationDeterminer = new ContinuationDeterminer();
        private static string NewsItemsPath = "NewsItems.json";

        static void Main(string[] args)
        {
            TxtPrinter.PrintInformation("WELCOME TO THE OBSERVER PROGRAM -- WHICH IS SOMEWHAT FUNNY");

            var news = new List<News>();
            try
            {
                using (var reader = new StreamReader(NewsItemsPath))
                {
                    var json = reader.ReadToEnd();
                    var jObject = JObject.Parse(json);
                    foreach (var obj in jObject)
                    {
                        switch (obj.Key)
                        {
                            case "good":
                                news.AddRange(((JArray)obj.Value).Select(n => n.ToNews(NewsType.Good)));
                                break;
                            case "bad":
                                news.AddRange(((JArray)obj.Value).Select(n => n.ToNews(NewsType.Bad)));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to load news items from {NewsItemsPath}. Exception: {ex.Message}");
                Environment.Exit(1);
            }

            while (true)
            {
                if (!ContinuationDeterminer.GoAgain())
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
