using Newtonsoft.Json.Linq;
using System;

namespace CommonClientLib.ExtensionMethods
{
    public static class JObjectExtensionMethods
    {
        /// <summary>
        /// Adds a <code>JProperty</code> to a <code>JToken</code> and then prints the entire object from the root.
        /// </summary>
        /// <param name="property">The <code>JProperty</code> to be added</param>
        /// <param name="print">Custom print function</param>
        public static void AddAndPrint(this JObject jToken, JProperty property, Action print = null)
        {
            jToken.Add(property);

            if (print != null)
            {
                print.Invoke();

                return;
            }

            Console.WriteLine($"{jToken.Root}");
        }
    }
}
