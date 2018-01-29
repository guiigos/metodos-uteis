using Microsoft.VisualBasic;
using System;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace MetodosUteis
{
    /*********************************************************************************
    * 
    * Classe: UtilString
    * Descrição: Métodos complementares para String
    * 
    * Guilherme Alves
    * guiigos.alves@gmail.com
    * http://guiigos.com
    * 
    *********************************************************************************/

    public class UtilString
    {
        public static string SubstringRight(string texto, int tamanho)
        {
            try
            {
                return texto.Substring(texto.Length - tamanho, tamanho);
            }
            catch (Exception ex)
            {
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static string RemoveAcentos(string texto)
        {
            try
            {
                string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç�";
                string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCcç";

                for (int i = 0; i < comAcentos.Length; i++)
                    texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());

                return texto;
            }
            catch (Exception ex)
            {
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static string RemoveEspeciaisNormalizar(string texto)
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
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static string RemoveEspeciaiRegex(string texto)
        {
            try
            {
                string caracteresEspec = "*!@#$%&()ªº°.";
                string novoTexto = "";
                for (int i = 0; i < caracteresEspec.Length; i++)
                {
                    string teste = caracteresEspec.Substring(i, 1);
                    texto = texto.Replace(teste, string.Empty);
                    novoTexto = texto;
                }

                return novoTexto;
            }
            catch (Exception ex)
            {
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static string SoNumeros(string texto)
        {
            try
            {
                string str = string.Empty, strNumero = "0123456789";

                for (int i = 0; i <= texto.Length - 1; i++)
                    for (int n = 0; n <= 9; n++)
                        if (strNumero[n].ToString() == texto.Substring(i, 1))
                            str += texto[i];

                if (string.IsNullOrEmpty(str.Trim())) return texto;
                return str;
            }
            catch (Exception ex)
            {
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static string SoLetras(string texto)
        {
            try
            {
                texto = texto.Trim();
                string novoTexto = "";

                for (int i = 0; i < texto.Length - 1; i++)
                {
                    if (Information.IsNumeric(texto[i].ToString()))
                    {
                        novoTexto = string.Empty;
                        novoTexto = texto.Remove(i);
                    }
                }

                return novoTexto;
            }
            catch (Exception ex)
            {
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static string AcertoDG(string texto, string caracter, byte direcao, int tamanho, bool upperCase)
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
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }
    }
}
