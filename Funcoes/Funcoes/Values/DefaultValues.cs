using System;

public static class DefaultValues
{
    public static String DefaultString(this Object value, String defaultValue = "")
    {
        if (value == Convert.DBNull) return Convert.ToString(defaultValue);
        if (value == null) return Convert.ToString(defaultValue);
        return Convert.ToString(value).Trim();
    }

    public static Boolean DefaultBoolean(this Object value, Boolean defaultValue = false)
    {
        if (value == Convert.DBNull) return Convert.ToBoolean(defaultValue);
        return Convert.ToBoolean(value);
    }

    public static Int32 DefaultInt32(this Object value, Int32 defaultValue = 0)
    {
        if (value == Convert.DBNull ||
            string.IsNullOrEmpty(Convert.ToString(value)))
            return Convert.ToInt32(defaultValue);
        return Convert.ToInt32(value);
    }

    public static Double DefaultDouble(this Object value, Double defaultValue = 0)
    {
        if (value == Convert.DBNull) return Convert.ToDouble(defaultValue);
        return Convert.ToDouble(value);
    }

    public static Decimal DefaultDecimal(this Object value, Decimal defaultValue = 0M)
    {
        if (value == Convert.DBNull) return Convert.ToDecimal(defaultValue);
        return Convert.ToDecimal(value);
    }

    public static Object DefaultObject(this Object value, Object defaultValue = null)
    {
        return value == DBNull.Value ? defaultValue : value;
    }

    public static DateTime DefaultDateTime(this Object value, DateTime defaultValue = default(DateTime))
    {
        DateTime dd;
        if (defaultValue == default(DateTime)) dd = Convert.ToDateTime("01/01/1900");
        else dd = Convert.ToDateTime(defaultValue);

        if (value == Convert.DBNull ||
            string.IsNullOrEmpty(Convert.ToString(value)) ||
            value == null)
            return dd;
        return Convert.ToDateTime(value);
    }
}
