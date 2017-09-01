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

        /// <summary>
        /// Converte uma STRING em um XmlDocument
        /// </summary>
        /// <param name="dados">String XML</param>
        /// <returns>Retorna um XmlDocument convertido</returns>
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

        /// <summary>
        /// Converte um OBJETO em um XmlDocument
        /// </summary>
        /// <param name="obj">Objeto que pretende-se converter</param>
        /// <returns>Retorna o XmlDocument convertido</returns>
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

        /// <summary>
        /// Converte uma String XML em um Objeto 
        /// </summary>
        /// <param name="xml">String XML</param>
        /// <param name="type">Type do objeto</param>
        /// <returns>Retorna o objeto preenchido</returns>
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

        /// <summary>
        /// Converte um XmlDocument para uma String XML
        /// </summary>
        /// <param name="xml">XmlDocument que se pretende converter</param>
        /// <returns>String XML</returns>
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

        /// <summary>
        /// Converte um Objeto para uma String XML
        /// </summary>
        /// <param name="Objeto">Objeto que se pretende converter</param>
        /// <returns>String XML</returns>
        public static string ObjectToStringXML(Object Objeto)
        {
            try
            {
                StringWriter writer = new StringWriter();
                XmlSerializer serializer = new XmlSerializer(Objeto.GetType());

                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add(String.Empty, String.Empty);

                serializer.Serialize(writer, Objeto, namespaces);
                return writer.ToString();
            }
            catch (Exception ex)
            {
                throw new XmlException("ObjectToStringXML", ex);
            }
        }

        /// <summary>
        /// Realiza a validação de um XML conforme sua XSD
        /// </summary>
        /// <param name="caminhoXSD">Caminho do arquivo XSD</param>
        /// <param name="arquivoXML">String XML</param>
        /// <param name="erros">Retorno dos erros</param>
        /// <returns>TRUE para válido, FALSE para com erros</returns>
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

        /// <summary>
        /// Preenche os erros na lista errValidation
        /// </summary>
        /// <param name="sender">Object sender do ValidarDocumentoXml</param>
        /// <param name="e">Event do ValidarDocumentoXml</param>
        private static void xmlSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            errValidation.Add("Erro Schema - " + e.Message);

            //if (e.Severity == XmlSeverityType.Warning) 
            //if (e.Severity == XmlSeverityType.Error)
        } 
    }
}
