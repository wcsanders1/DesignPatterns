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
            var jObj = new JObject();

            return jObj;
        }

        private void BuildJson<T>(T obj) where T : JToken
        {
            var firstQuestion = $"Is the value of the {typeof(T).Name.Substring(1).ToLower()} a key-value pair, an object, or an array?";
            var choices = new List<string>
            {
                "Key-value pair",
                "Object",
                "Array"
            };

            var firstChoice = choices[Asker.GetChoiceFromList(firstQuestion, choices)];
            switch (firstChoice)
            {
                case "Key-value pair":
                    break;
                case "Object":
                    break;
                case "Array":
                    break;
                default:
                    break;
            }
        }
    }
}
