using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace Aibidia.Common.Utils
{
    public static class XmlHelper
    {
        public static string SerializeToString<T>(T obj, bool prettyPrint = false, Encoding encoding = null)
        {
            var serializer = new XmlSerializer(typeof(T));
            string xmlstr;

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            var settings = new XmlWriterSettings();
            if (prettyPrint)
            {
                settings.Indent = true;
                settings.Encoding = encoding;
            }

            using (var sw = new Utf8StringWriter())
            using (var xw = XmlWriter.Create(sw, settings))
            {
                serializer.Serialize(xw, obj);
                xmlstr = sw.ToString();
            }

            return xmlstr;
        }

        public static string SerializeToString<T>(T obj, string prefix, string xmlnamespace, Encoding encoding = null) where T : class
        {
            return SerializeToString(obj, encoding, new PrefixTuple(prefix, xmlnamespace));
        }

        public static string SerializeToString<T>(T obj, Encoding encoding, params PrefixTuple[] prefixes)
            where T : class
        {
            using (var sw = new EncodedStringWriter(encoding ?? Encoding.UTF8))
            using (var xw = XmlWriter.Create(sw, new XmlWriterSettings { OmitXmlDeclaration = true, NewLineOnAttributes = true, Indent = true }))
            {
                return SerializeToString(obj, encoding, xw, sw, prefixes);
            }
        }

        public static string SerializeToStringStrictly<T>(T obj, Encoding encoding, params PrefixTuple[] prefixes)
            where T : class
        {
            using (var sw = new StrictStringWriter(encoding ?? Encoding.UTF8))
            using (var xw = new StrictXmlWriter(sw, new XmlWriterSettings { OmitXmlDeclaration = true }))
            {
                return SerializeToString(obj, encoding, xw, sw, prefixes);
            }
        }
        
        public static TResult Deserialize<TResult>(string xmlString)
        {
            var stringReader = new StringReader(xmlString);
            var serializer = new XmlSerializer(typeof(TResult));

            var obj = (TResult)serializer.Deserialize(stringReader);

            return obj;
        }

        private static string SerializeToString<T>(T obj, Encoding encoding, XmlWriter xmlWriter, StringWriter stringWriter, params PrefixTuple[] prefixes)
            where T : class
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            if (prefixes != null)
            {
                foreach (PrefixTuple tuple in prefixes)
                {
                    ns.Add(tuple.Prefix, tuple.XmlNamespace);
                }
            }

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            serializer.Serialize(xmlWriter, obj, ns);
            return stringWriter.ToString();
        }

        private class EncodedStringWriter : StringWriter
        {
            private Encoding _encoding;

            public EncodedStringWriter(Encoding encoding)
            {
                _encoding = encoding;
            }

            public override Encoding Encoding { get { return _encoding; } }
        }

        private class StrictStringWriter : EncodedStringWriter
        {
            public StrictStringWriter(Encoding encoding): base(encoding)
            {
            }

            public override string ToString()
            {
                string xml = base.ToString()
                    .Replace("###REPLACESINGLEQUOTE###", "&apos;")
                    .Replace("###REPLACEDOUBLEQUOTE###", "&quot;")
                    ;

                return xml;
            }
        }

        public struct PrefixTuple
        {
            public PrefixTuple(string prefix, string xmlnamespace)
            {
                Prefix = prefix;
                XmlNamespace = xmlnamespace;
            }

            public string Prefix { get; set; }
            public string XmlNamespace { get; set; }
        }
    }
}