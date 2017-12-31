using Newtonsoft.Json.Linq;
using CommonClientLib;
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
            BuildJson(obj, "root");

            return obj;
        }

        private void BuildJson(JObject obj, string objName)
        {
            while (true)
            {
                var addPropQuestion = $"Would you like to add a property to the {objName} object?";
                var addPropChoice = FacadeLib.YesOrNo[Asker.GetChoiceFromList(addPropQuestion, FacadeLib.YesOrNo)];

                switch (addPropChoice)
                {
                    case "No":
                        return;
                    case "Yes":
                        var name = FacadeLib.GetNameOrValue("new", NameOrValue.Name, XmlOrJson.Json);
                        var newProp = new JProperty(name);

                        obj.AddAndPrint(newProp);

                        var addObjQuestion = $"Is the value of the {name} property another object?";
                        var addObjChoice = FacadeLib.YesOrNo[Asker.GetChoiceFromList(addObjQuestion, FacadeLib.YesOrNo)];

                        if (addObjChoice == "Yes")
                        {
                            var newObj = new JObject();
                            obj[name] = newObj;
                            BuildJson(newObj, name);
                        }
                        else
                        {
                            var value = FacadeLib.GetNameOrValue($"{name}", NameOrValue.Value, XmlOrJson.Json);
                            obj[name] = value;
                            Console.WriteLine(obj.Root);
                        }
                        break;
                    default:
                        break;
                }
            }            
        }
    }
}
