using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace MVCWebMockup
{
    class Util
    {
        public static int parseInt(string input, int defaultVal)
        {
            try
            {
                return Convert.ToInt32(input);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return defaultVal;
            }
        }

        /// <summary>
        /// save element
        /// </summary>
        public static void SerializeElements<ArrayList>(ArrayList serializableObject, string fileName)
        {
            if (serializableObject == null) { return; }
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(typeof(ArrayList), new Type[] { typeof(AnyButton), typeof(AnyImage), typeof(AnyText), typeof(AnyLine) });
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, serializableObject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(fileName);
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Load element
        /// </summary>
        public static ArrayList DeSerializeElements(string fileName)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(fileName);
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ArrayList), new Type[] { typeof(AnyButton), typeof(AnyImage), typeof(AnyText), typeof(AnyLine) });
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        return (ArrayList) serializer.Deserialize(reader);
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return null;
        }
    }
}
