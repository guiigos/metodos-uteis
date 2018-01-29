using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace MetodosUteis
{
    /*********************************************************************************
     * 
     * Classe: Xml
     * Descrição: Realiza a manipulação de arquivos .xml
     * 
     * Guilherme Alves
     * guiigos.alves@gmail.com
     * http://guiigos.com
     * 
     *********************************************************************************/

    public class Xml
    {
        public static XmlDocument StringToXmlDocument(string dados)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(new StringReader(dados));

                return doc;
            }
            catch (Exception ex)
            {
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static XmlDocument ObjectToXmlDocument(object obj)
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(obj.GetType(), "");
                XmlDocument xd = null;

                using (MemoryStream memStm = new MemoryStream())
                {
                    ser.Serialize(memStm, obj);

                    memStm.Position = 0;

                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.IgnoreWhitespace = true;

                    using (var xtr = XmlReader.Create(memStm, settings))
                    {
                        xd = new XmlDocument();
                        xd.Load(xtr);
                    }
                }
                return xd;
            }
            catch (Exception ex)
            {
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static object StringToObject(string xml, Type type)
        {
            try
            {
                StringReader reader = new StringReader(xml);
                XmlSerializer serializer = new XmlSerializer(type);
                return serializer.Deserialize(reader);
            }
            catch (Exception ex)
            {
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static string XmlDocumentToString(XmlDocument xml)
        {
            try
            {
                StringWriter sw = new StringWriter();
                XmlTextWriter xtw = new XmlTextWriter(sw);
                xml.WriteTo(xtw);

                return sw.ToString();
            }
            catch (Exception ex)
            {
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static string ObjectToStringXML(object Objeto)
        {
            try
            {
                StringWriter writer = new StringWriter();
                XmlSerializer serializer = new XmlSerializer(Objeto.GetType());

                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add(string.Empty, string.Empty);

                serializer.Serialize(writer, Objeto, namespaces);
                return writer.ToString();
            }
            catch (Exception ex)
            {
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }
    }
}
