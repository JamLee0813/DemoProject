using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Formatting = Newtonsoft.Json.Formatting;

namespace DataCenter.Common.Helper
{
    /// <summary>
    ///     Json帮助类
    /// </summary>
    public static class JsonHelper
    {
        private static readonly JsonSerializerSettings JsonCamelSettings;
        private static readonly JsonSerializerSettings JsonSettings;

        static JsonHelper()
        {
            var datetimeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };

            JsonSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            JsonCamelSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            JsonSettings.Converters.Add(datetimeConverter);
            JsonCamelSettings.Converters.Add(datetimeConverter);
        }

        /// <summary>
        ///     反序列化为对象
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="s">对象序列化后的Xml字符串</param>
        /// <returns></returns>
        public static object Deserialize(Type type, string s)
        {
            using (var sr = new StringReader(s))
            {
                var xz = new XmlSerializer(type);
                return xz.Deserialize(sr);
            }
        }

        /// <summary>
        ///     将指定的Camel JSON 数据反序列化成指定对象。
        /// </summary>
        /// <typeparam name="T">对象类型。</typeparam>
        /// <param name="json">JSON 数据。</param>
        /// <returns></returns>
        public static T FromCamelJson<T>(this string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json, JsonCamelSettings);
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        ///     将指定的 JSON 数据反序列化成指定对象。
        /// </summary>
        /// <typeparam name="T">对象类型。</typeparam>
        /// <param name="json">JSON 数据。</param>
        /// <returns></returns>
        public static T FromJson<T>(this string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json, JsonSettings);
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        ///     将object对象序列化成XML
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ObjectToXml<T>(T t, Encoding encoding)
        {
            var ser = new XmlSerializer(t.GetType());
            using (var mem = new MemoryStream())
            {
                using (var writer = new XmlTextWriter(mem, encoding))
                {
                    var ns = new XmlSerializerNamespaces();
                    ns.Add(string.Empty, string.Empty);
                    ser.Serialize(writer, t, ns);
                    return encoding.GetString(mem.ToArray()).Trim();
                }
            }
        }

        /// <summary>
        ///     SOAP反序列化为对象
        /// </summary>
        /// <returns></returns>
        /// <summary>
        ///     静态扩展
        /// </summary>
        /// <typeparam name="T">需要序列化的对象类型，必须声明[Serializable]特征</typeparam>
        /// <param name="obj">需要序列化的对象</param>
        /// <param name="omitXmlDeclaration">true:省略XML声明;否则为false.默认false，即编写 XML 声明。</param>
        /// <returns></returns>
        public static string SerializeToXmlStr<T>(this T obj, bool omitXmlDeclaration = false)
        {
            return XmlSerialize(obj, omitXmlDeclaration);
        }

        /// <summary>
        ///     将指定的对象序列化成 Camel命名JSON 数据。
        /// </summary>
        /// <param name="obj">要序列化的对象。</param>
        /// <returns></returns>
        public static string ToCamelJson(this object obj)
        {
            try
            {
                return null == obj ? null : JsonConvert.SerializeObject(obj, Formatting.None, JsonCamelSettings);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///     将指定的对象序列化成 JSON 数据。
        /// </summary>
        /// <param name="obj">要序列化的对象。</param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            try
            {
                return null == obj ? null : JsonConvert.SerializeObject(obj, Formatting.None, JsonSettings);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///     使用XmlSerializer反序列化对象
        /// </summary>
        /// <param name="xmlOfObject">需要反序列化的xml字符串</param>
        /// <returns>反序列化后的对象</returns>
        public static T XmlDeserialize<T>(string xmlOfObject) where T : class
        {
            var xmlReader = XmlReader.Create(new StringReader(xmlOfObject), new XmlReaderSettings());
            return (T)new XmlSerializer(typeof(T)).Deserialize(xmlReader);
        }

        /// <summary>
        ///     使用XmlSerializer序列化对象
        /// </summary>
        /// <typeparam name="T">需要序列化的对象类型，必须声明[Serializable]特征</typeparam>
        /// <param name="obj">需要序列化的对象</param>
        /// <param name="omitXmlDeclaration">true:省略XML声明;否则为false.默认false，即编写 XML 声明。</param>
        /// <returns>序列化后的字符串</returns>
        public static string XmlSerialize<T>(T obj, bool omitXmlDeclaration = false)
        {
            var stream = new MemoryStream();
            var xmlwriter = XmlWriter.Create(stream /*writer*/,
                new XmlWriterSettings
                {
                    OmitXmlDeclaration = omitXmlDeclaration,
                    Encoding = new UTF8Encoding(false)
                }); //这里如果直接写成：Encoding = Encoding.UTF8 会在生成的xml中加入BOM(Byte-order Mark) 信息(Unicode 字节顺序标记) ， 所以new System.Text.UTF8Encoding(false)是最佳方式，省得再做替换的麻烦
            var xmlns = new XmlSerializerNamespaces();
            xmlns.Add(string.Empty, string.Empty); //在XML序列化时去除默认命名空间xmlns:xsd和xmlns:xsi
            var ser = new XmlSerializer(typeof(T));
            ser.Serialize(xmlwriter, obj, xmlns);

            return Encoding.UTF8.GetString(stream.ToArray());
        }
    }
}