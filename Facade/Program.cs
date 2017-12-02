using System;

namespace Facade
{
    class Program
    {
        private static TextToXml TextToXml = new TextToXml();

        static void Main(string[] args)
        {
            var xDoxument = TextToXml.GetXDocument();
            Console.WriteLine(xDoxument);

            Console.ReadLine();
        }
    }
}
