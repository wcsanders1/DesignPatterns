using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;

namespace Facade
{
    public class XmlJsonUtility
    {
        private static TextToXml TextToXml = new TextToXml();
        private static TextToJson TextToJson = new TextToJson();

        public XDocument GetXml()
        {
            return TextToXml.GetXmlFromText();
        }

        public JObject GetJson()
        {
            return TextToJson.GetJsonFromText();
        }

        public XDocument ConvertJsonToXml(JObject jObject)
        {
            var xmlString = JsonConvert.DeserializeXmlNode(jObject.ToString()).OuterXml;

            return XDocument.Parse(xmlString);
        }

        public JObject ConvertXmlToJson(XDocument xml)
        {
            var json = JsonConvert.SerializeXNode(xml, Formatting.Indented, true);

            return JObject.Parse(json);
        }
    }
}
