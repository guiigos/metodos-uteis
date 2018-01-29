using System;
using System.Collections.Generic;
using System.Linq;

namespace MetodosUteis
{
    /*********************************************************************************
    * 
    * Classe: ValuesComplement
    * Descrição: Complement de valores
    * 
    * Guilherme Alves
    * guiigos.alves@gmail.com
    * http://guiigos.com
    * 
    *********************************************************************************/

    public static class ValuesComplement
    {
        public static string QueryString(this string value)
        {
            return "'" + value + "'";
        }

        public static string QueryDate(this DateTime value)
        {
            return QueryString(string.Format("{0:yyyyMMdd}", Convert.ToDateTime(value)));
        }

        public static string MaskCpfCpnj(this string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value.Trim())) return string.Empty;
                if (value.Length == 14) return value.Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-");
                if (value.Length == 11) return value.Insert(3, ".").Insert(7, ".").Insert(11, "-");
                return value;
            }
            catch (Exception)
            {
                return value;
            }
        }

        public static string MaskPhone(this string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value.Trim())) return string.Empty;
                if (value.IndexOf("0", 0) == 0) value = value.Remove(0, 1);
                return value.Insert(0, "(").Insert(3, ")").Insert(8, "-");
            }
            catch (Exception)
            {
                return value;
            }
        }

        public static string ValorExtenso(this string value)
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
                value = value.Replace("R$", "");
                string wnumero = value.Replace(",", "").Trim();
                wnumero = wnumero.Replace(".", "").PadLeft(14, '0');
                wnumero = wnumero.Replace("-", "").PadLeft(14, '0');

                string operacao = "";

                if (float.Parse(value) < 0) operacao = " (negativo)";

                if (int.Parse(wnumero.Substring(0, 12)) > 0)
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
                    if (int.Parse(wnumero.Substring(0, 12)) > 1) wextenso += " reais";
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
            catch (Exception)
            {
                return value;
            }
        }

        public static string Limit(this string value, int maxLength)
        {
            if (value.Length <= maxLength) return value;
            return string.Concat(value.Substring(0, maxLength).Trim(), string.Empty);
        }

        public static bool Contains(this string text, string value)
        {
            return text.IndexOf(value) >= 0;
        }

        public static DateTime ZerarTime(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day);
        }

        public static IEnumerable<string> SplitTrim(this string value, params char[] separator)
        {
            return value.Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim());
        }
    }
}
