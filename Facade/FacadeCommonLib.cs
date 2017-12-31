using System;
using System.Collections.Generic;

namespace Facade
{
    public class FacadeCommonLib
    {
        private const int MaxNameLength = 20;

        public readonly List<string> YesOrNo = new List<string>
        {
            "Yes",
            "No"
        };


        public string GetNameOrValue(string name, NameOrValue nameOrValue, XmlOrJson xmlOrJson)
        {
            string entry;
            switch (xmlOrJson)
            {
                case XmlOrJson.Xml:
                    entry = "xml element";
                    break;
                case XmlOrJson.Json:
                    entry = "json property";
                    break;
                default:
                    entry = "";
                    break;
            }

            while (true)
            {
                Console.WriteLine($"\nWhat is the {nameOrValue.ToString().ToLower()} of the {name} {entry}?\n");

                var newName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(newName))
                {
                    Console.WriteLine($"Nope. The name must be fewer than {MaxNameLength} characters, with no spaces.\n");
                    continue;
                }
                Console.WriteLine();

                return newName;
            }
        }
    }
}
