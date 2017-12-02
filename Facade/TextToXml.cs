using System;
using System.Collections.Generic;
using System.Xml.Linq;
using CommonClientLib;

namespace Facade
{
    public class TextToXml
    {
        private enum NameOrValue
        {
            Name,
            Value
        }

        private QuestionAsker Asker = new QuestionAsker();
        private const int MaxNameLength = 20;

        /// <summary>
        /// Gets an XML tree based on information the user provides
        /// </summary>
        /// <returns><code>XDocument</code> based on user input</returns>
        public XDocument GetXDocument()
        {
            var rootName = GetNameOrValue("root", NameOrValue.Name);
            var rootElement = new XElement(rootName);
            var xmlTree = BuildXmlTree(rootElement);

            return new XDocument(xmlTree);
        }

        private XElement BuildXmlTree(XElement element)
        {
            var firstQuestion = $"Is the value of the {element.Name.LocalName} element another xml element?";
            var choices = new List<string>
            {
                "Yes",
                "No"
            };

            var firstChoice = choices[Asker.GetChoiceFromList(firstQuestion, choices)];
            switch (firstChoice)
            {
                case "Yes":
                    var newName = GetNameOrValue($"child of the {element.Name}", NameOrValue.Name);
                    var newElement = new XElement(newName);
                    element.Add(newElement);
                    BuildXmlTree(newElement);

                    while (true)
                    {
                        var secondQuestion = $"Do you want to add another xml element to the {element.Name.LocalName} xml element?";
                        var secondChoice = choices[Asker.GetChoiceFromList(secondQuestion, choices)];
                        if (secondChoice == "No")
                        {
                            break;
                        }
                        
                        var anotherName = GetNameOrValue($"child of the {element.Name}", NameOrValue.Name);
                        var anotherNewElement = new XElement(anotherName);
                        element.Add(anotherNewElement);
                        BuildXmlTree(anotherNewElement); 
                    }

                    break;
                case "No":
                    var value = GetNameOrValue(element.Name.LocalName, NameOrValue.Value);
                    element.Add(value);
                    break;
                default:
                    return null;
            }

            return element;
        }

        private string GetNameOrValue(string name, NameOrValue nameOrValue)
        {
            while (true)
            {
                Console.WriteLine($"What is the {nameOrValue.ToString().ToLower()} of the {name} xml element?");

                var newName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(newName))
                {
                    Console.WriteLine($"Nope. The name must be fewer than {MaxNameLength} characters, with no spaces.");
                    continue;
                }

                return newName;
            }
        }
    }
}
