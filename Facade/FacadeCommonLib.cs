﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Facade
{
    public class FacadeCommonLib
    {
        private const int MaxNameLength = 20;

        public string GetNameOrValue(string name, NameOrValue nameOrValue, XmlOrJson xmlOrJson)
        {
            string entry;
            switch (xmlOrJson)
            {
                case XmlOrJson.Xml:
                    entry = "xml element";
                    break;
                case XmlOrJson.Json:
                    entry = "json entry";
                    break;
                default:
                    entry = "";
                    break;
            }

            while (true)
            {
                Console.WriteLine($"What is the {nameOrValue.ToString().ToLower()} of the {name} {entry}?\n");

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
