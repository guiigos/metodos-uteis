using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MetodosUteis
{
    /*********************************************************************************
    * 
    * Classe: DataBaseORM
    * Descrição: Métodos facilitadores de banco de dados
    * 
    * Guilherme Alves
    * guiigos.alves@gmail.com
    * http://guiigos.com
    * 
    *********************************************************************************/

    public class DataBaseORM
    {
        public static string MontaSQL(string tabela, bool update, string where, object obj, SqlConnection connection, SqlTransaction transaction = null)
        {
            try
            {
                string consulta = string.Empty;
                string sql =
                    @"SELECT DISTINCT OBJECT_NAME(object_id) AS Tabela, c.name AS Coluna, ISNULL(st.type, t.name) AS TipoDados
                    FROM sys.COLUMNS c
                    INNER JOIN sys.types t ON t.system_type_id = c.system_type_id 
                    LEFT JOIN 
                    (
                        SELECT t.system_type_id, sd.DATA_TYPE AS type
                        FROM sys.systypes st
                        INNER JOIN sys.types t ON t.system_type_id = st.xtype AND t.system_type_id != t.user_type_id
                        INNER JOIN INFORMATION_SCHEMA.DOMAINS sd ON sd.domain_name = st.name
                    ) st ON st.system_type_id = c.system_type_id
                    WHERE is_identity = 'false' AND OBJECT_NAME(object_id) = '" + tabela + @"' AND t.name <> 'sysname'
                    ORDER BY Tabela, Coluna";

                string values = "VALUES (";
                using (SqlCommand cmd = new SqlCommand(sql, connection, transaction))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (!update) consulta = "INSERT INTO " + tabela + "(";
                        else consulta = "UPDATE " + tabela + " SET ";

                        while (dr.Read())
                        {
                            bool existeAtributo = false;
                            if (obj != null)
                            {
                                FieldInfo[] propriedades = obj.GetType().GetFields();
                                foreach (FieldInfo p in propriedades)
                                    if (p.Name == dr["Coluna"].ToString())
                                        existeAtributo = true;
                            }
                            else existeAtributo = dr["Coluna"].ToString().ToUpper().IndexOf("LEGADO") < 0;

                            if (existeAtributo)
                            {
                                if (!update)
                                {
                                    consulta += dr["Coluna"].ToString() + ", ";
                                    values += "@" + dr["Coluna"].ToString() + ", ";
                                }
                                else
                                {
                                    string[] clausula = where.Split();
                                    bool existeWHERE = false;
                                    foreach (string str in clausula)
                                    {
                                        if (str == dr["Coluna"].ToString())
                                        {
                                            existeWHERE = true;
                                            break;
                                        }
                                    }
                                    if (!existeWHERE)
                                        consulta += dr["Coluna"].ToString() + " = @" + dr["Coluna"].ToString() + ", ";
                                }
                            }
                        }

                        consulta = consulta.Remove(consulta.LastIndexOf(','), 1);
                        if (!update)
                        {
                            values = values.Remove(values.LastIndexOf(','), 1);
                            consulta += ") " + values + ")";
                        }
                        else consulta += " WHERE " + where;
                        return consulta;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static void PreencheObjetos(SqlDataReader reader, object objeto)
        {
            try
            {
                FieldInfo[] propriedades = objeto.GetType().GetFields();
                string nomeDoCampo = string.Empty;

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    nomeDoCampo = reader.GetName(i);

                    foreach (FieldInfo p in propriedades)
                    {
                        if (p.Name.ToUpper().Equals(nomeDoCampo.ToUpper()))
                        {
                            if (reader[i].GetType().Name == TypeCode.Decimal.ToString())
                            {
                                if (p.FieldType.Name == TypeCode.Decimal.ToString()) p.SetValue(objeto, Convert.ToDecimal(reader.GetValue(i)));
                                else p.SetValue(objeto, Convert.ToInt32(reader.GetValue(i)));
                            }
                            else if (reader[i].GetType().Name == TypeCode.Double.ToString() && p.FieldType.Name == TypeCode.Decimal.ToString())
                            {
                                p.SetValue(objeto, Convert.ToDecimal(reader.GetValue(i)));
                            }
                            else if (reader[i].GetType().Name == TypeCode.String.ToString())
                            {
                                if (p.FieldType.Name == TypeCode.Char.ToString()) p.SetValue(objeto, Convert.ToChar(reader.GetValue(i)));
                                else p.SetValue(objeto, reader.GetValue(i).ToString().Trim());
                            }
                            else if (reader[i].GetType().Name == TypeCode.Single.ToString())
                            {
                                p.SetValue(objeto, Convert.ToDecimal(reader.GetValue(i)));
                            }
                            else if (reader[i].GetType().Name == "TimeSpan")
                            {
                                p.SetValue(objeto, reader.GetValue(i));
                            }
                            else
                            {
                                if (reader.GetValue(i) == DBNull.Value)
                                {
                                    if (p.FieldType.Name == TypeCode.String.ToString())
                                        p.SetValue(objeto, "");
                                }
                                else p.SetValue(objeto, reader.GetValue(i));
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static void PreencheObjetos(DataRow reader, object objeto)
        {
            try
            {
                FieldInfo[] propriedades = objeto.GetType().GetFields();
                string nomeBusca = string.Empty;

                for (int i = 0; i < reader.Table.Columns.Count; i++)
                {
                    foreach (FieldInfo p in propriedades)
                    {
                        nomeBusca = reader.Table.Columns[i].ToString();

                        if (nomeBusca.ToUpper().Equals(p.Name.ToUpper()))
                        {
                            if (reader[nomeBusca].GetType().Name == TypeCode.Decimal.ToString())
                            {
                                if (p.FieldType.Name == TypeCode.Decimal.ToString()) p.SetValue(objeto, Convert.ToDecimal(reader[nomeBusca]));
                                else p.SetValue(objeto, Convert.ToInt32(reader[nomeBusca]));
                            }
                            else if (reader[nomeBusca].GetType().Name == TypeCode.Double.ToString() && p.FieldType.Name == TypeCode.Decimal.ToString())
                            {
                                p.SetValue(objeto, Convert.ToDecimal(reader[nomeBusca]));
                            }
                            else if (reader[nomeBusca].GetType().Name == TypeCode.String.ToString())
                            {
                                if (p.FieldType.Name == TypeCode.Char.ToString()) p.SetValue(objeto, Convert.ToChar(reader[nomeBusca]));
                                else p.SetValue(objeto, reader[nomeBusca].ToString().Trim());
                            }
                            else if (reader[nomeBusca].GetType().Name == TypeCode.Single.ToString())
                            {
                                p.SetValue(objeto, Convert.ToDecimal(reader[nomeBusca]));
                            }
                            else if (reader[nomeBusca].GetType().Name == "TimeSpan")
                            {
                                p.SetValue(objeto, reader[nomeBusca]);
                            }
                            else
                            {
                                if (reader[nomeBusca] == DBNull.Value)
                                {
                                    if (p.FieldType.Name == TypeCode.String.ToString())
                                        p.SetValue(objeto, "");
                                }
                                else p.SetValue(objeto, reader[nomeBusca]);
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static void AddParametros(SqlCommand sqlCommand, object objeto, bool assumirValorNulo, List<string> parametrosExcecao)
        {
            FieldInfo[] propriedades = objeto.GetType().GetFields();

            foreach (FieldInfo p in propriedades)
            {
                SqlParameter param = sqlCommand.CreateParameter();

                try
                {
                    SqlParameter p1 = null;
                    TypeConverter tc = null;
                    p1 = new SqlParameter();
                    tc = TypeDescriptor.GetConverter(p1.DbType);

                    if (tc.CanConvertFrom(p.FieldType))
                    {
                        p1.DbType = (DbType)tc.ConvertFrom(p.FieldType.Name);
                    }
                    else
                    {
                        try
                        {
                            p1.DbType = (DbType)tc.ConvertFrom(p.FieldType.Name);
                        }
                        catch (Exception ex)
                        {
                            throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
                        }
                    }

                    param.SqlDbType = p1.SqlDbType;
                }
                catch
                {
                    continue;
                }
                param.ParameterName = "@" + p.Name;
                param.Value = p.GetValue(objeto);

                sqlCommand.Parameters.Add(param);
            }

            foreach (SqlParameter parameter in sqlCommand.Parameters)
            {
                if (parameter.Value == null)
                {
                    parameter.Value = DBNull.Value;
                }
                else if (parameter.Value.ToString() == string.Empty)
                {
                    parameter.Value = DBNull.Value;
                }
                else if (parameter.SqlDbType == SqlDbType.Int)
                {
                    Boolean eExcecao = false;
                    if (parametrosExcecao != null)
                    {
                        for (int i = 0; i < parametrosExcecao.Count; i++)
                        {
                            if (parameter.ParameterName == parametrosExcecao[i])
                            {
                                eExcecao = true;
                                parametrosExcecao.RemoveAt(i);
                                break;
                            }
                        }
                    }

                    if ((assumirValorNulo && !eExcecao) || (!assumirValorNulo && eExcecao))
                        if (Convert.ToInt32(parameter.Value) == 0)
                            parameter.Value = DBNull.Value;
                }
                else if (Information.IsDate(parameter.Value))
                {
                    if (UtilDate.ZerarTime(Convert.ToDateTime(parameter.Value)) == new DateTime()) parameter.Value = DBNull.Value;
                    else if (UtilDate.ZerarTime(Convert.ToDateTime(parameter.Value)) == new DateTime(1900, 01, 01)) parameter.Value = DBNull.Value;
                }
            }
        }

        public static void CreateParametersFromString(SqlCommand sqlCommand, string sql, Array parametros)
        {
            try
            {
                Regex expressao = new Regex("[@]+[A-z0-9]*");
                Match match = expressao.Match(sql);

                int i = 0;
                while (match.Success)
                {
                    if (!ExisteParametro(sqlCommand.Parameters, match.Value))
                    {
                        sqlCommand.Parameters.AddWithValue(match.Value, parametros.GetValue(i));
                        i++;
                    }
                    match = match.NextMatch();
                }
            }
            catch (Exception ex)
            {
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private static bool ExisteParametro(SqlParameterCollection parametros, string name)
        {
            try
            {
                foreach (SqlParameter item in parametros)
                    if (item.ParameterName == name)
                        return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }
    }
}
