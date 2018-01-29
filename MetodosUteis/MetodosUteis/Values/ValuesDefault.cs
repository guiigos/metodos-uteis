using System;

namespace MetodosUteis
{
    /*********************************************************************************
    * 
    * Classe: DefaultValues
    * Descrição: Valores padrão
    * 
    * Guilherme Alves
    * guiigos.alves@gmail.com
    * http://guiigos.com
    * 
    *********************************************************************************/

    public static class DefaultValues
    {
        public static string DefaultString(this object value, string defaultValue = "")
        {
            if (value == Convert.DBNull) return Convert.ToString(defaultValue);
            if (value == null) return Convert.ToString(defaultValue);
            return Convert.ToString(value).Trim();
        }

        public static bool DefaultBoolean(this object value, bool defaultValue = false)
        {
            if (value == Convert.DBNull) return Convert.ToBoolean(defaultValue);
            return Convert.ToBoolean(value);
        }

        public static int DefaultInt32(this object value, int defaultValue = 0)
        {
            if (value == Convert.DBNull || string.IsNullOrEmpty(Convert.ToString(value))) return Convert.ToInt32(defaultValue);
            return Convert.ToInt32(value);
        }

        public static double DefaultDouble(this object value, double defaultValue = 0)
        {
            if (value == Convert.DBNull) return Convert.ToDouble(defaultValue);
            return Convert.ToDouble(value);
        }

        public static decimal DefaultDecimal(this object value, decimal defaultValue = 0M)
        {
            if (value == Convert.DBNull) return Convert.ToDecimal(defaultValue);
            return Convert.ToDecimal(value);
        }

        public static object DefaultObject(this object value, object defaultValue = null)
        {
            return value == DBNull.Value ? defaultValue : value;
        }

        public static DateTime DefaultDateTime(this object value, DateTime defaultValue = default(DateTime))
        {
            DateTime dd;
            if (defaultValue == default(DateTime)) dd = Convert.ToDateTime("01/01/1900");
            else dd = Convert.ToDateTime(defaultValue);

            if (value == Convert.DBNull || string.IsNullOrEmpty(Convert.ToString(value)) || value == null) return dd;
            return Convert.ToDateTime(value);
        }
    }
}
