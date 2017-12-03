using System;
using System.Linq;
using System.Xml.Linq;

namespace CommonClientLib
{
    public static class XElementExtensionMethods
    {
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
