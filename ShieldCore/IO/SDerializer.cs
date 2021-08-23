using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Shield.IO
{
    public class SDerializer
    {
        public static T XmlToObject<T>(string xmlFilePath)
        {
            T resultObj;

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFilePath);

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (XmlReader reader = new XmlNodeReader(doc))
            {
                resultObj = (T)serializer.Deserialize(reader);
            }

            return resultObj;
        }

        public static T XmlStringToObject<T>(string input) where T : class
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            using (StringReader sr = new StringReader(input))
            {
                return (T)ser.Deserialize(sr);
            }
        }

        public static string ObjectToXml<T>(T ObjectToSerialize)
        {
            XmlSerializer ser = new XmlSerializer(ObjectToSerialize.GetType());
            using (StringWriter sw = new StringWriter())
            {
                ser.Serialize(sw, ObjectToSerialize);
                return sw.ToString();
            }
        }
    }
}