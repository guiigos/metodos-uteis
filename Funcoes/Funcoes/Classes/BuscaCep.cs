using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Funcoes
{
    class BuscaCepException : Exception
    {
        public BuscaCepException(String metodo, Exception ex) :
            base("Funcoes.BuscaCep." + metodo + " - " + ex.Message) { }
        public BuscaCepException(String metodo, String mensagem) :
            base("Funcoes.BuscaCep." + metodo + " - " + mensagem) { }
    }

    [DataContractAttribute]
    public class BuscaCep
    {
        #region atributos
        [DataMemberAttribute]
        public string cep;
        [DataMemberAttribute]
        public string logradouro;
        [DataMemberAttribute]
        public string complemento;
        [DataMemberAttribute]
        public string bairro;
        [DataMemberAttribute]
        public string localidade;
        [DataMemberAttribute]
        public string uf;
        [DataMemberAttribute]
        public string unidade;
        [DataMemberAttribute]
        public string ibge;
        [DataMemberAttribute]
        public string gia;
        #endregion

        #region buscadores
        public enum Buscadores
        {
            ViaCEP
        }
        #endregion

        #region construtor
        public BuscaCep(int cep, Buscadores buscador = Buscadores.ViaCEP)
        {
            this.cep = cep.ToString();
            if (cep.ToString().Length != 8)
                throw new BuscaCepException("Buscar", "CEP inválido.");

            switch (buscador)
            {
                case Buscadores.ViaCEP: ViaCEP(); break;
            }
        }
        #endregion

        #region metodos
        private void ViaCEP()
        {
            String url = "https://viacep.com.br/ws/@cep/json".Replace("@cep", cep.ToString());
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";

            try
            {
                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            string response = responseReader.ReadToEnd();

                            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(BuscaCep));
                            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(response));
                            BuscaCep busca = (BuscaCep)ser.ReadObject(ms);

                            this.cep = busca.cep;
                            this.logradouro = busca.logradouro;
                            this.complemento = busca.complemento;
                            this.bairro = busca.bairro;
                            this.localidade = busca.localidade;
                            this.uf = busca.uf;
                            this.unidade = busca.unidade;
                            this.ibge = busca.ibge;
                            this.gia = busca.gia;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new BuscaCepException("Buscar", ex.Message);
            }
        }
        #endregion
    }
}
