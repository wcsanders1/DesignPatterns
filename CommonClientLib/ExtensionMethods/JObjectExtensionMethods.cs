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
        public static void AddAndPrint(this JObject jObject, JProperty property, Action print = null)
        {
            jObject.Add(property);

            if (print != null)
            {
                print.Invoke();

                return;
            }

            Console.WriteLine(jObject.Root);
        }

        /// <summary>
        /// Gets the name of the parent.
        /// </summary>
        /// <returns>Name of parent</returns>
        public static string GetParentName(this JObject jObject)
        {
            var parent = (JProperty)jObject.Parent;

            return parent?.Name ?? "root";
        }
    }
}
