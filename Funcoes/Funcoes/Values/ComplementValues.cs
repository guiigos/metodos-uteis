using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ComplementValues
{
    public static String QueryString(this String value)
    {
        return "'" + value + "'";
    }

    public static String QueryDate(this DateTime value)
    {
        return QueryString(string.Format("{0:yyyyMMdd}", Convert.ToDateTime(value)));
    }

    public static String MaskCpfCpnj(this String numCpfCnpj)
    {
        if (numCpfCnpj != null && numCpfCnpj != String.Empty)
        {
            if (numCpfCnpj.Length == 14) return numCpfCnpj.Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-");
            else if (numCpfCnpj.Length == 11) return numCpfCnpj.Insert(3, ".").Insert(7, ".").Insert(11, "-");
            else return numCpfCnpj;
        }

        return String.Empty;
    }

    public static String MaskPhone(this String numberPhone)
    {
        if (numberPhone != null && numberPhone != String.Empty)
        {
            if (numberPhone.IndexOf("0", 0) == 0)
                numberPhone = numberPhone.Remove(0, 1);

            return numberPhone.Insert(0, "(").Insert(3, ")").Insert(8, "-");
        }

        return String.Empty;
    }

    public static String Limit(this String value, int maxLength)
    {
        if (value.Length <= maxLength)  return value;

        return string.Concat(value.Substring(0, maxLength).Trim(), String.Empty);
    } 

    public static DateTime ZerarTime(this DateTime data)
    {
        return new DateTime(data.Year, data.Month, data.Day);
    }

    public static Boolean Contains(this String text, String value)
    {
        return text.IndexOf(value) >= 0;
    }

    public static Boolean IsNullOrEmpty(this String value)
    {
        return String.IsNullOrEmpty(value);
    }

    public static IEnumerable<String> SplitTrim(this String value, params Char[] separator)
    {
        return value.Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim());
    }
}
