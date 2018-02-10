using System.Xml.Linq;
using CommonClientLib;

namespace Facade
{
    public class TextToXml
    {
        private QuestionAsker Asker = new QuestionAsker();
        private FacadeCommonLib FacadeLib = new FacadeCommonLib();

        /// <summary>
        /// Gets an XML tree based on information the user provides
        /// </summary>
        /// <returns><code>XDocument</code> based on user input</returns>
        public XDocument GetXmlFromText()
        {
            var rootName = FacadeLib.GetNameOrValue("root", NameOrValue.Name, XmlOrJson.Xml);
            var element = new XElement(rootName);
            var xmlTree = BuildXmlTree(element);

            return new XDocument(xmlTree);
        }

        private XElement BuildXmlTree(XElement element)
        {
            var anotherElementQuestion = $"Is the value of the {element.Name.LocalName} element another xml element?";            
            var anotherElementChoice = FacadeLib.YesOrNo[Asker.GetChoiceFromList(anotherElementQuestion, FacadeLib.YesOrNo)];

            switch (anotherElementChoice)
            {
                case "Yes":
                    var newName = FacadeLib.GetNameOrValue($"child of the {element.Name}", NameOrValue.Name, XmlOrJson.Xml);
                    var newElement = new XElement(newName);
                    element.AddAndPrint(newElement);
                    BuildXmlTree(newElement);

                    while (true)
                    {
                        var addElementQuestion = $"Do you want to add another xml element to the {element.Name.LocalName} xml element?";
                        var addElementChoice = FacadeLib.YesOrNo[Asker.GetChoiceFromList(addElementQuestion, FacadeLib.YesOrNo)];
                        if (addElementChoice == "No")
                        {
                            break;
                        }
                        
                        var anotherName = FacadeLib.GetNameOrValue($"child of the {element.Name}", NameOrValue.Name, XmlOrJson.Xml);
                        var anotherNewElement = new XElement(anotherName);
                        element.AddAndPrint(anotherNewElement);
                        BuildXmlTree(anotherNewElement);
                    }
                    break;
                case "No":
                    var value = FacadeLib.GetNameOrValue(element.Name.LocalName, NameOrValue.Value, XmlOrJson.Xml);
                    element.AddAndPrint(value);
                    break;
                default:
                    return null;
            }

            return element;
        }
    }
}
