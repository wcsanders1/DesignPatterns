using CommonClientLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Mediator
{
    class Program
    {
        private static TextPrinter TxtPrinter = new TextPrinter();
        private static List<string> Arguments = new List<string>();
        private static string ArgumentsPath = "Arguments.json";

        static void Main(string[] args)
        {
            TxtPrinter.PrintInformation("WELCOME TO THE MEDIATOR PROGRAM -- WHICH IS A MILDLY ENTERTAINING PROGRAM");

            try
            {
                using (var reader = new StreamReader(ArgumentsPath))
                {
                    var json = reader.ReadToEnd();
                    Arguments.AddRange(JsonConvert.DeserializeObject<List<string>>(json));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to retrieve arguments from {ArgumentsPath}.\n " +
                    $"Using default arguments. Exception message: {ex.Message}\n");

            }
        }
    }
}
