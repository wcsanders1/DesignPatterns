using Newtonsoft.Json.Linq;
using CommonClientLib;
using System.Collections.Generic;
using CommonClientLib.ExtensionMethods;
using System;

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
            while(true)
            {
                var name = FacadeLib.GetNameOrValue("new", NameOrValue.Name, XmlOrJson.Json);
                var newProp = new JProperty(name);
                obj.AddAndPrint(newProp);
                var question = $"Is the value of the {name} property another object?";
                var choices = new List<string>
                {
                    "Yes",
                    "No"
                };

                var choice = choices[Asker.GetChoiceFromList(question, choices)];
                if (choice == "Yes")
                {
                    var newObj = new JObject();
                    obj[name] = newObj;
                    BuildJson(newObj);
                }
                if (choice == "No")
                {
                    var value = FacadeLib.GetNameOrValue($"{name}", NameOrValue.Value, XmlOrJson.Json);
                    obj[name] = value;
                    Console.WriteLine(obj.Root);
                    
                    var secondQuesion = $"Would you like to add another property to the {obj.GetParentName()} object?";
                    var secondChoice = choices[Asker.GetChoiceFromList(secondQuesion, choices)];
                    if (secondChoice == "No")
                    {
                        break;
                    }
                }
            }
            
        }
    }
}
