using Newtonsoft.Json.Linq;
using CommonClientLib;
using System.Collections.Generic;

namespace Facade
{
    public class TextToJson
    {
        private QuestionAsker Asker = new QuestionAsker();
        private FacadeCommonLib FacadeLib = new FacadeCommonLib();

        /// <summary>
        /// Gets a json tree based on information the user provides
        /// </summary>
        /// <returns><code>JObject</code> based on user input</returns>
        public JObject GetJsonFromText()
        {
            var obj = new JObject();
            BuildJson(obj);

            return obj;
        }

        private void BuildJson(JObject obj)
        {
            var firstQuestion = "Is the value of this object an object or a property?";
            var choices = new List<string>
            {
                "Object",
                "Property"
            };

            var firstChoice = choices[Asker.GetChoiceFromList(firstQuestion, choices)];
            switch (firstChoice)
            {
                case "Object":
                    break;
                case "Property":
                    while (true)
                    {
                        var name = FacadeLib.GetNameOrValue("object", NameOrValue.Name, XmlOrJson.Json);
                        var value = FacadeLib.GetNameOrValue("object", NameOrValue.Value, XmlOrJson.Json);
                        obj.Add(new JProperty(name, value));

                        var secondQuestion = "Would you like to add another property to this object?";
                        var moreChoices = new List<string>
                        {
                            "Yes",
                            "No"
                        };

                        var secondChoice = moreChoices[Asker.GetChoiceFromList(secondQuestion, moreChoices)];
                        if (secondChoice == "No")
                        {
                            break;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
