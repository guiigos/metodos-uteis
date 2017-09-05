using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Funcoes
{
    class EmailException : Exception
    {
        public EmailException(String metodo, Exception ex) :
            base("Funcoes.Email." + metodo + " - " + ex.Message) { }

        public EmailException(String metodo) :
            base("Funcoes.Email." + metodo) { }
    }

    public class Email
    {
        #region atributos
        public string emailAssinatura;
        public string emailMensagem;
        public string emailUsuarioAutenticacao;
        public string emailSenhaAutenticacao;
        public string emailRemetente;
        public string emailNomeRemetente;
        public string emailAssunto;
        public string emailCaminhaImagemAssinatura;
        public string emailDestinatario;
        public string emailEnderecoSmtp;
        public int emailPortaSmtp;
        public bool emailCopiaRemetente;
        public bool emailCopiaOculta;
        public bool emailConfirmacao;
        public bool emailSsl;
        public bool emailDeleteAnexos;
        public bool emailAutenticacao;

        public List<string> emailListAnexos;
        #endregion

        #region construtor
        public Email()
        {
            emailAssinatura = String.Empty;
            emailMensagem = String.Empty;
            emailUsuarioAutenticacao = String.Empty;
            emailSenhaAutenticacao = String.Empty;
            emailRemetente = String.Empty;
            emailNomeRemetente = String.Empty;
            emailAssunto = String.Empty;
            emailCaminhaImagemAssinatura = String.Empty;
            emailDestinatario = String.Empty;
            emailEnderecoSmtp = String.Empty;
            emailPortaSmtp = 0;
            emailCopiaRemetente = false;
            emailCopiaOculta = false;
            emailConfirmacao = false;
            emailSsl = false;
            emailDeleteAnexos = false;
            emailAutenticacao = false;
            emailListAnexos = new List<string>();
        }
        #endregion

        #region metodos
        private string corrigeHTMLEmail(string mensagem)
        {
            while (mensagem.IndexOf("SIZE=\"") > -1)
            {
                string tamanho = mensagem.Substring(mensagem.IndexOf("SIZE=\"") + 6, 2).Replace("\"", "");
                mensagem = mensagem.Replace("SIZE=\"" + tamanho + "\"", "STYLE=\"font-size: " + tamanho + "px;\"");
            }
            return mensagem;
        }

        public bool EnviarEmail()
        {
            SmtpClient clientSmtp = new SmtpClient();
            MailMessage message = new MailMessage();

            try
            {
                // monta assinatura para o e-mail
                if (!String.IsNullOrEmpty(this.emailAssinatura))
                {
                    this.emailAssinatura = this.emailAssinatura.Replace("\n", "<br/>");
                    this.emailMensagem = this.emailMensagem.Trim() + "<br/><br/><br/><br/><br/>" + this.emailAssinatura;
                }

                // credenciais
                System.Net.NetworkCredential credenciais = new System.Net.NetworkCredential(this.emailUsuarioAutenticacao.Trim(), this.emailSenhaAutenticacao.Trim());

                // altera o modo como define o tamanho das fontes;
                this.emailMensagem = corrigeHTMLEmail(this.emailMensagem);

                // cria uma nova mensagem
                message.Priority = MailPriority.Normal;
                message.IsBodyHtml = true;

                message.From = new MailAddress(this.emailRemetente, this.emailNomeRemetente);
                message.Subject = this.emailAssunto.Trim();

                // se possuir imagem de assinatura, o modo como a mensagem é construída é diferente, para que a imagem seja apresentada no corpo do email,
                // caso não tenha imagem de assinatura, a mensagem do email continua sendo feita como antes
                if (!string.IsNullOrEmpty(this.emailCaminhaImagemAssinatura))
                {
                    //Atenção, o suporte a CSS é limitado em vários Webmails, 
                    //utilize o CSS INLINE que é compatível com a maioria dos WEBMAILS, OUTLOOK e outros.
                    StringBuilder body = new StringBuilder();
                    body.Append("<html>");
                    body.Append(this.emailMensagem.Trim());
                    body.Append("<br/><br/><img src=\"cid:assinatura\" title=\"assinatura\" />");

                    AlternateView av = AlternateView.CreateAlternateViewFromString(body.ToString(), new System.Net.Mime.ContentType("text/html"));

                    LinkedResource lr = new LinkedResource(this.emailCaminhaImagemAssinatura);
                    lr.ContentId = "assinatura";
                    av.LinkedResources.Add(lr);
                    message.AlternateViews.Add(av);
                }
                else message.Body = this.emailMensagem.Trim();

                if (this.emailCopiaRemetente)
                    this.emailDestinatario += " ;" + this.emailRemetente;

                // adiciona destinatários
                string[] dest = this.emailDestinatario.Split(';');
                if (Validators.ValidaEnderecoEmail(this.emailDestinatario)) message.To.Add(dest[0].ToString().Trim());
                else throw new EmailException("Email do destinatário inválido - " + this.emailDestinatario);

                for (int i = 1; i < dest.Length; i++)
                {
                    if (!dest[i].ToString().Trim().Equals(String.Empty))
                    {
                        if (this.emailCopiaOculta)
                        {
                            if (Validators.ValidaEnderecoEmail(this.emailDestinatario)) message.Bcc.Add(dest[i].ToString().Trim());
                            else throw new EmailException("Email do destinatário inválido - " + this.emailDestinatario);
                        }
                        else
                        {
                            if (Validators.ValidaEnderecoEmail(this.emailDestinatario)) message.To.Add(dest[i].ToString().Trim());
                            else throw new EmailException("Email do destinatário inválido - " + this.emailDestinatario);
                        }
                    }
                }

                // solicita confirmacao de leitura do e-mail
                if (this.emailConfirmacao) message.Headers.Add("Disposition-Notification-To", emailRemetente);

                // anexos
                foreach (string anexo in this.emailListAnexos)
                {
                    Attachment anexado = new Attachment(anexo, MediaTypeNames.Application.Octet);
                    message.Attachments.Add(anexado);
                }

                // cria um novo cliente smtp
                if (this.emailEnderecoSmtp == String.Empty)
                    throw new Exception("Endereco Smtp não informado!");
                clientSmtp.Host = this.emailEnderecoSmtp;
                clientSmtp.Port = this.emailPortaSmtp;
                clientSmtp.EnableSsl = this.emailSsl;
                clientSmtp.UseDefaultCredentials = false;

                if (this.emailAutenticacao)
                {
                    clientSmtp.UseDefaultCredentials = true;
                    clientSmtp.Credentials = credenciais;
                }

                // envia
                clientSmtp.Send(message);
                // encerra
                message.Dispose();

                if (this.emailDeleteAnexos)
                {
                    // exclui anexos
                    foreach (string anexo in this.emailListAnexos)
                    {
                        if (System.IO.File.Exists(anexo))
                        {
                            try
                            {
                                System.IO.File.Delete(anexo);
                            }
                            catch (Exception) { }
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                message.Dispose();
                throw new EmailException("EnviarEmail", ex);
            }
        }
        #endregion
    }
}
