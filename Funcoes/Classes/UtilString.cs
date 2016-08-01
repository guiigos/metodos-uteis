using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funcoes
{
    class UtilStrigException : Exception
    {
        public UtilStrigException(String metodo, Exception ex) :
            base("Funcoes.UtilString." + metodo + " - " + ex.Message) { }
    }

    public class UtilString
    {
        public static string SubstringRight(String texto, Int32 tamanho)
        {
            try
            {
                return texto.Substring(texto.Length - tamanho, tamanho);
            }
            catch (Exception ex)
            {
                throw new UtilStrigException("SubstringRight", ex);
            }
        }

        public static string RemoveEspeciais(String texto)
        {
            try
            {
                texto = texto.Normalize(NormalizationForm.FormD);
                StringBuilder sb = new StringBuilder();
                foreach (char c in texto.ToCharArray())
                    if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                        sb.Append(c);

                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new UtilStrigException("RemoveCaracteresEspeciais", ex);
            }
        }

        public static string RemoveAcentos(String texto)
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç�";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCcç";

            for (int i = 0; i < comAcentos.Length; i++)
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());

            return texto;
        }

        public static string RemoverCaracteresEspeciais(String texto)
        {
            string caracteresEspec = "*!@#$%&()ªº°.";
            string novoTexto = "";
            for (int i = 0; i < caracteresEspec.Length; i++)
            {
                string teste = caracteresEspec.Substring(i, 1);
                texto = texto.Replace(teste, String.Empty);
                novoTexto = texto;
            }

            return novoTexto;
        }

        public static string SoNumeros(String texto)
        {
            string str = String.Empty, strNumero = "0123456789";

            for (int i = 0; i <= texto.Length - 1; i++)
                for (int n = 0; n <= 9; n++)
                    if (strNumero[n].ToString() == texto.Substring(i, 1)) 
                        str += texto[i];

            if (string.IsNullOrEmpty(str.Trim())) return texto;
            return str;
        }

        public static string SoLetras(String texto)
        {
            texto = texto.Trim();
            string novoTexto = "";

            for (int i = 0; i < texto.Length - 1; i++)
            {
                if (Microsoft.VisualBasic.Information.IsNumeric(texto[i].ToString()))
                {
                    novoTexto = String.Empty;
                    novoTexto = texto.Remove(i);
                }
            }

            return novoTexto;
        }

        public static string AcertoDG(String texto, String caracter, Byte direcao, Int32 tamanho, Boolean upperCase)
        {
            try
            {
                texto = texto.ToString().Trim();
                if (texto.Length >= tamanho) texto = texto.Substring(0, Convert.ToInt32(tamanho));
                else
                {
                    while (texto.Length < tamanho)
                    {
                        if (direcao == 0) texto = texto + caracter;
                        else texto = caracter + texto;
                    }
                }
                
                if (upperCase == true) return texto.ToUpper();
                else return texto;
            }
            catch (Exception ex)
            {
                throw new UtilStrigException("AcertoDG", ex);
            }
        }

        public static string ValorExtenso(String valor)
        {
            string[] wunidade = { "", " e um", " e dois", " e três", " e quatro", " e cinco", " e seis", " e sete", " e oito", " e nove" };
            string[] wdezes = { "", " e onze", " e doze", " e treze", " e quatorze", " e quinze", " e dezesseis", " e dezessete", " e dezoito", " e dezenove" };
            string[] wdezenas = { "", " e dez", " e vinte", " e trinta", " e quarenta", " e cinquenta", " e sessenta", " e setenta", " e oitenta", " e noventa" };
            string[] wcentenas = { "", " e cento", " e duzentos", " e trezentos", " e quatrocentos", " e quinhentos", " e seiscentos", " e setecentos", " e oitocentos", " e novecentos" };
            string[] wplural = { " bilhões", " milhões", " mil", "" };
            string[] wsingular = { " bilhão", " milhão", " mil", "" };
            string wextenso = "";
            string wfracao;

            try
            {
                valor = valor.Replace("R$", "");
                string wnumero = valor.Replace(",", "").Trim();
                wnumero = wnumero.Replace(".", "").PadLeft(14, '0');
                wnumero = wnumero.Replace("-", "").PadLeft(14, '0');

                string operacao = "";

                if (float.Parse(valor) < 0) operacao = " (negativo)";

                if (Int64.Parse(wnumero.Substring(0, 12)) > 0)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        wfracao = wnumero.Substring(i * 3, 3);
                        if (int.Parse(wfracao) != 0)
                        {
                            if (int.Parse(wfracao.Substring(0, 3)) == 100) wextenso += " e cem";
                            else
                            {
                                wextenso += wcentenas[int.Parse(wfracao.Substring(0, 1))];
                                if (int.Parse(wfracao.Substring(1, 2)) > 10 && int.Parse(wfracao.Substring(1, 2)) < 20) wextenso += wdezes[int.Parse(wfracao.Substring(2, 1))];
                                else
                                {
                                    wextenso += wdezenas[int.Parse(wfracao.Substring(1, 1))];
                                    wextenso += wunidade[int.Parse(wfracao.Substring(2, 1))];
                                }
                            }
                            if (int.Parse(wfracao) > 1) wextenso += wplural[i];
                            else wextenso += wsingular[i];
                        }
                    }
                    if (Int64.Parse(wnumero.Substring(0, 12)) > 1) wextenso += " reais";
                    else wextenso += " real";
                }

                wfracao = wnumero.Substring(12, 2);
                if (int.Parse(wfracao) > 0)
                {
                    if (int.Parse(wfracao.Substring(0, 2)) > 10 && int.Parse(wfracao.Substring(0, 2)) < 20) wextenso = wextenso + wdezes[int.Parse(wfracao.Substring(1, 1))];
                    else
                    {
                        wextenso += wdezenas[int.Parse(wfracao.Substring(0, 1))];
                        wextenso += wunidade[int.Parse(wfracao.Substring(1, 1))];
                    }
                    if (int.Parse(wfracao) > 1) wextenso += " centavos";
                    else wextenso += " centavo";
                }
                if (wextenso != "") wextenso += operacao;
                if (wextenso != "") wextenso = wextenso.Substring(3, 1).ToUpper() + wextenso.Substring(4);
                else wextenso = "";
                return wextenso;
            }
            catch (Exception ex)
            {
                throw new UtilStrigException("ValorExtenso", ex);
            }
        }
    }
}
