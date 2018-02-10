using System;
using System.Linq;
using System.Xml.Linq;

namespace CommonClientLib
{
    public static class XElementExtensionMethods
    {
        /// <summary>
        /// Adds an object to an <code>XElement</code> and then prints the entire object from the root.
        /// </summary>
        /// <param name="obj">The <code>XElement</code> to be added</param>
        /// <param name="print">Custom print function</param>
        public static void AddAndPrint(this XElement element, object obj, Action print = null)
        {
            element.Add(obj);

            if (print != null)
            {
                print.Invoke();

                return;
            }

            var rootElement = element
                .AncestorsAndSelf()
                .Reverse()
                .First();

            Console.WriteLine($"\n{rootElement}\n");
        }
    }
}
