using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Funcoes
{
    class XmlException : Exception
    {
        public XmlException(String metodo, Exception ex) :
            base("Funcoes.Xml." + metodo + " - " + ex.Message) { }
    }

    public class Xml
    {
        private static List<String> errValidation;

        public static XmlDocument StringToXmlDocument(String dados)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(new StringReader(dados));

                return doc;
            }
            catch (Exception ex)
            {
                throw new XmlException("StringToXmlDocument", ex);
            }
        }

        public static XmlDocument ObjectToXmlDocument(Object obj)
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
                throw new XmlException("ObjectToXmlDocument", ex);
            }
        }

        public static Object StringToObject(String xml, Type type)
        {
            try
            {
                StringReader reader = new StringReader(xml);
                XmlSerializer serializer = new XmlSerializer(type);
                return serializer.Deserialize(reader);
            }
            catch (Exception ex)
            {
                throw new XmlException("StringToObject", ex);
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
                throw new XmlException("XmlDocumentToString", ex);
            }
        }

        public static string ObjectToStringXML(Object Objeto)
        {
            try
            {
                StringWriter writer = new StringWriter();
                XmlSerializer serializer = new XmlSerializer(Objeto.GetType());
                serializer.Serialize(writer, Objeto);
                return writer.ToString();
            }
            catch (Exception ex)
            {
                throw new XmlException("ObjectToStringXML", ex);
            }
        }

        public static bool ValidarDocumentoXml(String caminhoXSD, String arquivoXML, out List<String> erros)
        {
            errValidation = new List<String>();
            erros = new List<String>();
            if (caminhoXSD.Equals(String.Empty)) return true;

            try
            {
                XmlTextReader reader = new XmlTextReader(new System.IO.StringReader(arquivoXML));
                reader.Read();

                XmlSchema schema = XmlSchema.Read(new XmlTextReader(caminhoXSD), null);

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                settings.ValidationEventHandler += new ValidationEventHandler(xmlSettingsValidationEventHandler);
                settings.Schemas.Add(schema);

                XmlReader validatingReader = XmlReader.Create(reader, settings);
                while (validatingReader.Read()) { };


                //XmlSchemaSet schemas = new XmlSchemaSet();
                //schemas.Add(null, caminho);

                //XmlReaderSettings settings = new XmlReaderSettings();
                //settings.ValidationType = ValidationType.Schema;
                //settings.Schemas = schemas;
                //settings.ValidationEventHandler += new ValidationEventHandler();
                //settings.ValidationEventHandler += xmlSettingsValidationEventHandler;

                //settings.ValidationFlags =
                //XmlSchemaValidationFlags.ReportValidationWarnings |
                //XmlSchemaValidationFlags.ProcessIdentityConstraints |
                //XmlSchemaValidationFlags.ProcessInlineSchema |
                //XmlSchemaValidationFlags.ProcessSchemaLocation;

                //XmlReader xml = XmlReader.Create(reader, settings);
                //while (xml.Read()) { }
                //xml.Close();

                erros = errValidation;
                return errValidation.Count == 0;
            }
            catch (Exception ex)
            {
                throw new XmlException("ValidarDocumentoXml", ex);
            }
        }

        private static void xmlSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            errValidation.Add("Erro Schema - " + e.Message);

            //if (e.Severity == XmlSeverityType.Warning) 
            //if (e.Severity == XmlSeverityType.Error)
        } 

    }
}
