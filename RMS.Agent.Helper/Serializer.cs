using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using RMS.Common.Exception;

namespace RMS.Agent.Helper
{
    public class Serializer
    {
        #region UTF8 Converter

        private static String UTF8ByteArrayToString(Byte[] characters)
        {

            UTF8Encoding encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        private static Byte[] StringToUTF8ByteArray(String pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }

        #endregion

        #region XmlSerializerHelper

        public class XML
        {
            #region XML Document Serializers - not used anymore

            private static XmlDocument GetobjAsXmlDocument(object obj)
            {
                XmlDocument retVal = new XmlDocument();
                XmlSerializer ser = new XmlSerializer(obj.GetType(), new XmlRootAttribute(obj.GetType().Name));
                StringBuilder sb = new StringBuilder();
                TextWriter writer = new StringWriter(sb);

                ser.Serialize(writer, obj);
                StringReader reader = new StringReader(writer.ToString());
                //Read the Xml from the StringReader object
                DataSet dst = new DataSet();

                dst.ReadXml(reader);

                retVal.LoadXml(dst.GetXml());
                return retVal;
            }

            private static object GetXmlDocumentAsObject(XmlNodeReader xmlNodeReader, Type type)
            {
                // Construct an instance of the XmlSerializer with the type
                // of object that is being deserialized.
                XmlSerializer mySerializer = new XmlSerializer(type, new XmlRootAttribute(type.Name));
                // Call the Deserialize method and cast to the object type.
                return mySerializer.Deserialize(xmlNodeReader);
            }

            #endregion

            #region XML Serializers

            public static string SerializeObject<T>(T obj)
            {
                try
                {
                    //                    XmlSerializer xs = new XmlSerializer(obj.GetType());
                    //                    StringWriter stringWriter = new StringWriter();
                    //                    xs.Serialize(stringWriter, obj);
                    //                    stringWriter.Flush();
                    //                    return stringWriter.ToString();


                    String XmlizedString = null;
                    MemoryStream memoryStream = new MemoryStream();
                    XmlSerializer xs = new XmlSerializer(obj.GetType());
                    XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                    xs.Serialize(xmlTextWriter, obj);
                    memoryStream = (MemoryStream) xmlTextWriter.BaseStream;
                    XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
                    return XmlizedString.Trim();
                }
                catch (Exception ex)
                {
                    throw new RMSAppException("SerializeObject<T>(T obj) failed. " + ex.Message, ex, false);
                }
            }

            public static string SerializeObject<T>(T obj, Type[] types)
            {
                try
                {
                    //                    String XmlizedString = null;
                    //                    MemoryStream memoryStream = new MemoryStream();
                    XmlSerializer xs = new XmlSerializer(obj.GetType(), types);
                    StringWriter stringWriter = new StringWriter();
                    //                    XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                    xs.Serialize(stringWriter, obj);
                    //                    memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                    //                    XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
                    stringWriter.Flush();
                    return stringWriter.ToString().Trim();
                }
                catch (Exception ex)
                {
                    throw new RMSAppException("SerializeObject<T>(T obj, Type[] types) failed. " + ex.Message, ex, false);
                }
            }

            public static T DeserializeObject<T>(String pXmlizedString)
            {
                try
                {
                    XmlSerializer xs = new XmlSerializer(typeof(T));
                    StringReader stringReader = new StringReader(pXmlizedString);
                    //                    MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString));
                    return (T)xs.Deserialize(stringReader);
                }
                catch (Exception ex)
                {
                    throw new RMSAppException("DeserializeObject<T>(String pXmlizedString) failed. " + ex.Message, ex, false);
                }
            }

            public static T DeserializeObject<T>(String pXmlizedString, Type[] types)
            {
                try
                {
                    XmlSerializer xs = new XmlSerializer(typeof(T), types);
                    StringReader stringReader = new StringReader(pXmlizedString);
                    //                    MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString));
                    return (T)xs.Deserialize(stringReader);
                }
                catch (Exception ex)
                {
                    throw new RMSAppException("DeserializeObject<T>(String pXmlizedString, Type[] types) failed. " + ex.Message, ex, false);
                }
            }

            public static object CastObjAsObj<T1>(T1 objSource)
            {
                String XmlizedString = SerializeObject(objSource);
                if (XmlizedString != null)
                {
                    return DeserializeObject<object>(XmlizedString); ;
                }
                else
                    return null;
            }

            #endregion
        }

        #endregion

        #region BinarySerializerHelper

        public class Binary
        {
            public static byte[] SerializeObject<T>(T input)
            {
                try
                {
                    IFormatter formatter = new BinaryFormatter();
                    MemoryStream memoryStream = new MemoryStream();
                    formatter.Serialize(memoryStream, input);
                    memoryStream.Position = 0;
                    byte[] output = memoryStream.ToArray();
                    memoryStream.Close();
                    return output;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            public static T DeserializeObject<T>(byte[] input)
            {
                try
                {
                    IFormatter formatter = new BinaryFormatter();
                    MemoryStream memoryStream = new MemoryStream(input);
                    T output = (T)formatter.Deserialize(memoryStream);
                    memoryStream.Close();
                    return output;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        #endregion

        #region SOAPSerializerHelper

        public class SOAP
        {
            public static string SerializeObject<T>(T request)
            {
                try
                {
                    SoapFormatter serializer = new SoapFormatter();
                    MemoryStream memoryStream = new MemoryStream();
                    XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                    serializer.Serialize(memoryStream, request);
                    memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                    return UTF8ByteArrayToString(memoryStream.ToArray()); ;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            public static T DeSerializeSOAP<T>(string soapString)
            {
                try
                {
                    MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(soapString));
                    SoapFormatter deserializer = new SoapFormatter();
                    T output = (T)deserializer.Deserialize(memoryStream);
                    memoryStream.Close();
                    return output;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        #endregion

    }
}
