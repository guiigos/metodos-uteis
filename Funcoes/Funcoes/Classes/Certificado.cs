using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;

namespace Funcoes
{
    class CertificadoException : Exception
    {
        public CertificadoException(String metodo, Exception ex) : 
            base("Funcoes.Certificado." + metodo + " - " + ex.Message) { }
    }

    public class Certificado
    {
        public static string AssinarXmlPorElemento(X509Certificate2 certificado, String conteudoXML, String tag, Boolean assinarTagRps)
        {
            try
            {
                bool tagId = false;
                XmlDocument doc = Xml.StringToXmlDocument(conteudoXML);

                XmlNodeList listaTags = doc.GetElementsByTagName(tag);
                SignedXml signedXml;

                foreach (XmlElement infNfse in listaTags)
                {
                    string id = String.Empty;
                    if (infNfse.HasAttribute("id"))
                    {
                        id = infNfse.Attributes.GetNamedItem("id").Value;
                    }
                    else if (infNfse.HasAttribute("Id"))
                    {
                        id = infNfse.Attributes.GetNamedItem("Id").Value;
                        tagId = true;
                    }
                    else if (infNfse.HasAttribute("ID"))
                    {
                        id = infNfse.Attributes.GetNamedItem("ID").Value;
                    }
                    else if (infNfse.HasAttribute("iD"))
                    {
                        id = infNfse.Attributes.GetNamedItem("iD").Value;
                    }
                    else
                    {
                        tagId = false;

                        if (assinarTagRps)
                            continue;
                    }

                    signedXml = new SignedXml(infNfse);
                    signedXml.SigningKey = certificado.PrivateKey;

                    Reference reference = new Reference("#" + id);
                    if (!tagId) { reference.Uri = string.Empty; }

                    reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
                    reference.AddTransform(new XmlDsigC14NTransform());

                    signedXml.AddReference(reference);
                    KeyInfo keyInfo = new KeyInfo();
                    keyInfo.AddClause(new KeyInfoX509Data(certificado));
                    signedXml.KeyInfo = keyInfo;

                    signedXml.ComputeSignature();

                    XmlElement xmlSignature = doc.CreateElement("Signature", "http://www.w3.org/2000/09/xmldsig#");

                    if (tagId)
                    {
                        XmlAttribute xmlAttribute = doc.CreateAttribute("Id");
                        xmlAttribute.Value = "Ass_" + id.Replace(":", "_");
                        xmlSignature.Attributes.InsertAfter(xmlAttribute, xmlSignature.GetAttributeNode("xmlns"));
                    }

                    XmlElement xmlSignedInfo = signedXml.SignedInfo.GetXml();
                    XmlElement xmlKeyInfo = signedXml.KeyInfo.GetXml();
                    XmlElement xmlSignatureValue = doc.CreateElement("SignatureValue", xmlSignature.NamespaceURI);
                    string signBase64 = Convert.ToBase64String(signedXml.Signature.SignatureValue);

                    XmlText xmlText = doc.CreateTextNode(signBase64);
                    xmlSignatureValue.AppendChild(xmlText);

                    xmlSignature.AppendChild(doc.ImportNode(xmlSignedInfo, true));
                    xmlSignature.AppendChild(xmlSignatureValue);
                    xmlSignature.AppendChild(doc.ImportNode(xmlKeyInfo, true));

                    if (assinarTagRps) infNfse.ParentNode.ParentNode.AppendChild(xmlSignature);
                    else infNfse.ParentNode.AppendChild(xmlSignature);
                }

                String conteudoXmlAssinado = Xml.XmlDocumentToString(doc);
                return conteudoXmlAssinado;
            }
            catch (Exception ex)
            {
                throw new CertificadoException("AssinarXmlPorElemento", ex);
            }
        }

        public static string AssinarXML(X509Certificate2 certificado, String conteudoXML)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                System.Security.Cryptography.RSACryptoServiceProvider key = new System.Security.Cryptography.RSACryptoServiceProvider();

                System.Security.Cryptography.Xml.SignedXml signedDocument;
                System.Security.Cryptography.Xml.KeyInfo keyInfo = new System.Security.Cryptography.Xml.KeyInfo();

                doc.LoadXml(conteudoXML);
                key = (System.Security.Cryptography.RSACryptoServiceProvider)certificado.PrivateKey;

                keyInfo.AddClause(new System.Security.Cryptography.Xml.KeyInfoX509Data(certificado));
                signedDocument = new System.Security.Cryptography.Xml.SignedXml(doc);
                signedDocument.SigningKey = key;
                signedDocument.KeyInfo = keyInfo;

                System.Security.Cryptography.Xml.Reference reference = new System.Security.Cryptography.Xml.Reference();
                reference.Uri = string.Empty;

                reference.AddTransform(new System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform());
                reference.AddTransform(new System.Security.Cryptography.Xml.XmlDsigC14NTransform(false));

                signedDocument.AddReference(reference);
                signedDocument.ComputeSignature();

                System.Xml.XmlElement xmlDigitalSignature = signedDocument.GetXml();
                doc.DocumentElement.AppendChild(doc.ImportNode(xmlDigitalSignature, true));

                return doc.OuterXml;
            }
            catch (Exception ex)
            {
                throw new CertificadoException("AssinarXML", ex);
            }
        }

        public static string X509CertificateToBase64(X509Certificate2 certificado)
        {
            try
            {
                byte[] rsaPublicKey = certificado.RawData;
                String base64 = Convert.ToBase64String(rsaPublicKey);

                return base64;
            }
            catch (Exception ex)
            {
                throw new CertificadoException("X509CertificateToBase64", ex);
            }
        }

        public static string EncryptMessage(X509Certificate2 certificado, String mensagem)
        {
            try
            {
                if (!certificado.Verify()) return String.Empty;

                RSACryptoServiceProvider rsaEncryptor = (RSACryptoServiceProvider)certificado.PrivateKey;
                byte[] cipherData = rsaEncryptor.Encrypt(Encoding.UTF8.GetBytes(mensagem), true);
                return Convert.ToBase64String(cipherData);
            }
            catch (Exception ex)
            {
                throw new CertificadoException("EncryptMessage", ex);
            }
        }

        public static string DescryptMessage(X509Certificate2 certificado, String mensagem)
        {
            try
            {
                if (!certificado.Verify()) return String.Empty;

                RSACryptoServiceProvider rsaEncryptor = (RSACryptoServiceProvider)certificado.PrivateKey;
                byte[] plainData = rsaEncryptor.Decrypt(Convert.FromBase64String(mensagem), true);
                return Encoding.UTF8.GetString(plainData);
            }
            catch (Exception ex)
            {
                throw new CertificadoException("DescryptMessage", ex);
            }
        }

        public static string RetornarThumbprint()
        {
            try
            {
                X509Certificate2 _X509Cert = new X509Certificate2();
                X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

                X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
                X509Certificate2Collection collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                X509Certificate2Collection collection2 = (X509Certificate2Collection)collection.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, false);

                X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(collection2, "Certificado(s) Digital(is) disponível(is)", "Selecione o Certificado Digital para uso no aplicativo", X509SelectionFlag.SingleSelection);
                if (scollection.Count == 0) _X509Cert.Reset();
                else _X509Cert = scollection[0];
                store.Close();

                return _X509Cert.Thumbprint;
            }
            catch (Exception ex)
            {
                throw new CertificadoException("RetornarThumbprint", ex);
            }
        }

        public static string RetornarNome()
        {
            try
            {
                X509Certificate2 _X509Cert = new X509Certificate2();
                X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

                X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
                X509Certificate2Collection collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                X509Certificate2Collection collection2 = (X509Certificate2Collection)collection.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, false);

                X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(collection2, "Certificado(s) Digital(is) disponível(is)", "Selecione o Certificado Digital para uso no aplicativo", X509SelectionFlag.SingleSelection);
                if (scollection.Count == 0) _X509Cert.Reset();
                else _X509Cert = scollection[0];
                store.Close();

                return _X509Cert.Subject;
            }
            catch (Exception ex)
            {
                throw new CertificadoException("RetornarNome", ex);
            }
        }

        public static bool Vencido(X509Certificate2 certificado)
        {
            try
            {
                String subject = certificado.Subject;
                DateTime validadeInicial = certificado.NotBefore;
                DateTime validadeFinal = certificado.NotAfter;

                return DateTime.Compare(DateTime.Now, validadeFinal) > 0;
            }
            catch (Exception ex)
            {
                throw new CertificadoException("Vencido", ex);
            }
        }

        public static X509Certificate2 SelecionarPorNome(String nome)
        {
            try
            {
                X509Certificate2 _X509Cert = new X509Certificate2();
                X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

                X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
                X509Certificate2Collection collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                X509Certificate2Collection collection2 = (X509Certificate2Collection)collection.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, false);

                if (nome.Equals(String.Empty))
                {
                    X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(collection2, "Certificado(s) Digital(is) disponível(is)", "Selecione o Certificado Digital para uso no aplicativo", X509SelectionFlag.SingleSelection);
                    if (scollection.Count == 0) _X509Cert.Reset();
                    else _X509Cert = scollection[0];
                }
                else
                {
                    X509Certificate2Collection scollection =
                        (X509Certificate2Collection)collection2.Find(X509FindType.FindBySubjectDistinguishedName, nome, false);

                    if (scollection.Count == 0) _X509Cert.Reset();
                    else _X509Cert = scollection[0];
                }

                store.Close();
                return _X509Cert;
            }
            catch (Exception ex)
            {
                throw new CertificadoException("SelecionarPorNome", ex);
            }
        }

        public static X509Certificate2 SelecionarPorThumbprint(String thumbprint)
        {
            try
            {
                X509Certificate2 _X509Cert = new X509Certificate2();
                X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

                X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
                X509Certificate2Collection collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                X509Certificate2Collection collection2 = (X509Certificate2Collection)collection1.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, false);

                if (thumbprint.Equals(String.Empty))
                {
                    X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(collection2, "Certificado(s) Digital(is) disponível(is)", "Selecione o Certificado Digital para uso no aplicativo", X509SelectionFlag.SingleSelection);

                    if (scollection.Count == 0) _X509Cert.Reset();
                    else _X509Cert = scollection[0];
                }
                else
                {
                    X509Certificate2Collection scollection = (X509Certificate2Collection)collection2.Find(X509FindType.FindByThumbprint, thumbprint, false);
                    if (scollection.Count == 0) _X509Cert.Reset();
                    else _X509Cert = scollection[0];
                }

                store.Close();
                return _X509Cert;
            }
            catch (Exception ex)
            {
                throw new CertificadoException("SelecionarPorThumbprint", ex);
            }
        }

        public static X509Certificate2 Base64ToX509Certificate(String certificado)
        {
            try
            {
                byte[] data = Convert.FromBase64String(certificado);
                X509Certificate2 x509certificate = new X509Certificate2(data);
                return x509certificate;
            }
            catch (Exception ex)
            {
                throw new CertificadoException("Base64ToX509Certificate", ex);
            }
        }
    }
}
