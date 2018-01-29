using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;

namespace MetodosUteis
{
    /*********************************************************************************
     * 
     * Classe: Xsd
     * Descrição: Realiza validação de arquivos .xml confrontando com arquivos .xsd
     * 
     * Guilherme Alves
     * guiigos.alves@gmail.com
     * http://guiigos.com
     * 
     *********************************************************************************/

    public class Xsd
    {
        private static List<string> errValidation;

        private static void xmlSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            errValidation.Add("Erro Schema - " + e.Message);

            //if (e.Severity == XmlSeverityType.Warning) 
            //if (e.Severity == XmlSeverityType.Error)
        }

        public static bool ValidarDocumentoXml(string caminhoXSD, string arquivoXML, out List<string> erros)
        {
            errValidation = new List<string>();
            erros = new List<string>();
            if (caminhoXSD.Equals(string.Empty)) return true;

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
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }
    }
}
