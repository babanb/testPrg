using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Infrastructure.Helpers
{
    public static class SerializationHelper
    {
        public static string SerializeEntityToXml<T>(T obj)
        {
            try
            {
                if (Equals(default(T), obj))
                {
                    return null;
                }

                using (var write = new StringWriter())
                {
                    var xmlSerializer = CreateOverrider(obj.GetType());
                    xmlSerializer.Serialize(write, obj);
                    return write.ToString();
                }
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        public static T DeserializeEntityFromXml<T>(string xmlContent)
        {
            if (xmlContent == null)
            {
                return default(T);
            }

            try
            {
                var serializeResponse = CreateOverrider(typeof(T));
                return (T)serializeResponse.Deserialize(new StringReader(xmlContent));
            }
            catch (Exception e)
            {
                return default(T);
            }
        }

        private static XmlSerializer CreateOverrider(Type type)
        {
            var xOver = new XmlAttributeOverrides();

            var attrs = new XmlAttributes { XmlIgnore = true };

            var pInfos = type.GetProperties()
                .Where(p =>
                    (typeof(IEnumerable).IsAssignableFrom(p.PropertyType) && p.PropertyType != typeof(string)) ||
                    (p.PropertyType.Namespace == type.Namespace && p.PropertyType.Name != "EncryptedData"))
                .ToList();

            foreach (var pi in pInfos)
            {
                xOver.Add(type, pi.Name, attrs);
            }

            return new XmlSerializer(type, xOver);
        } 
    }

}
