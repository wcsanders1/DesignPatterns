using System;

namespace Facade
{
    class Program
    {
        private static TextToXml TextToXml = new TextToXml();
        private static TextToJson TextToJson = new TextToJson();

        static void Main(string[] args)
        {
            var jObj = TextToJson.GetJsonFromText();
            //var xDoxument = TextToXml.GetXmlFromText();
            //Console.WriteLine(xDoxument);

            Console.ReadLine();
        }
    }
}
