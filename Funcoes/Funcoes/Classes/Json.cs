using System;
using System.Data;
using System.Text;

namespace Funcoes
{
    class JsonException : Exception
    {
        public JsonException(String metodo, Exception ex) :
            base("Funcoes.Json." + metodo + " - " + ex.Message) { }
    }

    public class Json
    {
        /// <summary>
        /// Converte um DataTable em uma String JSON
        /// </summary>
        /// <param name="dtb">DataTable que se pretende converter</param>
        /// <returns>String JSON</returns>
        public static string DataTabelToJSON(DataTable dtb)
        {
            try
            {
                string[] jsonArray = new string[dtb.Columns.Count];
                string headString = string.Empty;

                for (int i = 0; i < dtb.Columns.Count; i++)
                {
                    jsonArray[i] = dtb.Columns[i].Caption; // Array para todas as colunas
                    headString += "'" + jsonArray[i] + "' : '" + jsonArray[i] + i.ToString() + "%" + "',";
                }
                headString = headString.Substring(0, headString.Length - 1);

                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                if (dtb.Rows.Count > 0)
                {
                    for (int i = 0; i < dtb.Rows.Count; i++)
                    {
                        string tempString = headString;
                        sb.Append("{");
                        for (int j = 0; j < dtb.Columns.Count; j++)
                            tempString = tempString.Replace(dtb.Columns[j] + j.ToString() + "%", dtb.Rows[i][j].ToString());

                        sb.Append(tempString + "},");
                    }
                }
                else
                {
                    string tempString = headString;
                    sb.Append("{");
                    for (int j = 0; j < dtb.Columns.Count; j++)
                    {
                        tempString = tempString.Replace(dtb.Columns[j] + j.ToString() + "%", "-");
                    }
                    sb.Append(tempString + "},");
                }
                sb = new StringBuilder(sb.ToString().Substring(0, sb.ToString().Length - 1));
                sb.Append("]");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new JsonException("DataTabelToJSON", ex);
            }
        }
    }
}
