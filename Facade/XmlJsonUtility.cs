using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
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
            XDocument xDocument;

            try
            {
                xDocument = XDocument.Parse(xmlString);
            }
            catch (Exception ex)
            {
                xDocument = new XDocument(new XElement("exception", ex.Message));
            }

            return xDocument;
        }

        public JObject ConvertXmlToJson(XDocument xml)
        {
            var json = JsonConvert.SerializeXNode(xml, Formatting.Indented, false);
            JObject jObject;

            try
            {
                jObject = JObject.Parse(json);
            }
            catch (Exception ex)
            {

                jObject = new JObject(new JProperty("exception", ex.Message));
            }

            return jObject;
        }
    }
}
