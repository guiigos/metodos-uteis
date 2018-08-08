using System;
using System.Collections.Generic;
using System.Linq;

namespace MetodosUteis
{
    /*********************************************************************************
    * 
    * Classe: ValuesComplement
    * Descrição: Complementos de valores
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
            return string.Format("'{0}'", value);
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
