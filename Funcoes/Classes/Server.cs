using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Funcoes
{
    public class Server
    {
        class ServerException : Exception
        {
            public ServerException(String metodo, Exception ex) :
                base("Funcoes.Server." + metodo + " - " + ex.Message) { }
        }

        public static string MontaSOAP(String url, String xml, String method, String action, String SOAPaction, String soapEnvelope, X509Certificate2 certificado, WebProxy proxy)
        {
            HttpWebRequest myHttpWebRequest = null;
            HttpWebResponse myHttpWebResponse = null;
            XmlDocument myXMLDocument = null;
            XmlTextReader myXMLReader = null;

            try
            {
                ASCIIEncoding encoding = new ASCIIEncoding();

                if (soapEnvelope == String.Empty)
                {
                    soapEnvelope += "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
                    soapEnvelope += "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xd=\"http://www.w3.org/2000/09/xmldsig#\">";
                    soapEnvelope += "<soapenv:Header/><soapenv:Body>";
                    soapEnvelope += xml.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", String.Empty);
                    soapEnvelope += "</soapenv:Body></soapenv:Envelope>";
                }
                byte[] soap = encoding.GetBytes(soapEnvelope);

                myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                myHttpWebRequest.Method = method;
                myHttpWebRequest.Timeout = 300000;
                myHttpWebRequest.Headers.Add("SOAPAction", SOAPaction);
                myHttpWebRequest.ContentType = "text/xml; encoding='utf-8'";
                myHttpWebRequest.ClientCertificates.Add(certificado);
                myHttpWebRequest.Proxy = proxy;
                myHttpWebRequest.ContentLength = soap.Length;

                Stream stream = myHttpWebRequest.GetRequestStream();
                stream.Write(soap, 0, soap.Length);
                stream.Close();

                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                stream = myHttpWebResponse.GetResponseStream();

                myXMLDocument = new XmlDocument();
                myXMLReader = new XmlTextReader(stream);
                myXMLDocument.Load(myXMLReader);

                return myXMLDocument.InnerXml;
            }
            catch (Exception ex)
            {
                throw new ServerException("MontaSOAP", ex);
            }
        }

        public static bool UploadArquivoFTP(String url, String usuario, String senha, String path)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                request.Credentials = new NetworkCredential(usuario, senha);
                byte[] bytes = System.IO.File.ReadAllBytes(path);

                request.ContentLength = bytes.Length;
                using (Stream request_stream = request.GetRequestStream())
                {
                    request_stream.Write(bytes, 0, bytes.Length);
                    request_stream.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new ServerException("UploadArquivoFTP", ex);
            }
        }
    }
}
