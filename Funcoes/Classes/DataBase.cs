using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Funcoes
{
    class DataBaseException : Exception
    {
        public DataBaseException(String metodo, Exception ex) :
            base("Funcoes.DataBase." + metodo + " - " + ex.Message) { }
    }

    public class DataBase
    {
        public static object Sum(String campo, String tabela, String filtro, SqlConnection connection, SqlTransaction transaction = null)
        {
            object resultado = null;
            string sql = @"SELECT Sum(@campo) As Dado FROM @tabela @where";

            sql = sql.Replace("@tabela", tabela);
            sql = sql.Replace("@campo", campo);
            sql = sql.Replace("@where", string.IsNullOrEmpty(filtro) ? string.Empty : "WHERE " + filtro);

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection, transaction))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        resultado = reader["Dado"].DefaultObject();
                    }
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new DataBaseException("Sum", ex);
            }
        }

        public static object Max(String campo, String tabela, String filtro, SqlConnection connection, SqlTransaction transaction = null)
        {
            object resultado = null;
            string sql = @"SELECT Max(@campo) As Dado FROM @tabela @where";

            sql = sql.Replace("@tabela", tabela);
            sql = sql.Replace("@campo", campo);
            sql = sql.Replace("@where", string.IsNullOrEmpty(filtro) ? string.Empty : "WHERE " + filtro);

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection, transaction))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        resultado = reader["Dado"].DefaultObject();
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new DataBaseException("Max", ex);
            }
        }

        public static object Min(String campo, String tabela, String filtro, SqlConnection connection, SqlTransaction transaction = null)
        {
            object resultado = null;
            string sql = @"SELECT Min(@campo) As Dado FROM @tabela @where";

            sql = sql.Replace("@tabela", tabela);
            sql = sql.Replace("@campo", campo);
            sql = sql.Replace("@where", string.IsNullOrEmpty(filtro) ? string.Empty : "WHERE " + filtro);

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection, transaction))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        resultado = reader["Dado"].DefaultObject();
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new DataBaseException("Min", ex);
            }
        }

        public static object Avg(String campo, String tabela, String filtro, SqlConnection connection, SqlTransaction transaction = null)
        {
            object resultado = null;
            string sql = @"SELECT Avg(@campo) As Dado FROM @tabela @where";

            sql = sql.Replace("@tabela", tabela);
            sql = sql.Replace("@campo", campo);
            sql = sql.Replace("@where", string.IsNullOrEmpty(filtro) ? string.Empty : "WHERE " + filtro);

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection, transaction))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        resultado = reader["Dado"].DefaultObject();
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new DataBaseException("Avg", ex);
            }
        }

        public static object Count(String campo, String tabela, String filtro, SqlConnection connection, SqlTransaction transaction = null)
        {
            object resultado = null;
            string sql = @"SELECT Count(@campo) As Dado FROM @tabela @where";

            sql = sql.Replace("@tabela", tabela);
            sql = sql.Replace("@campo", campo);
            sql = sql.Replace("@where", string.IsNullOrEmpty(filtro) ? string.Empty : "WHERE " + filtro);

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection, transaction))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        resultado =reader["Dado"].DefaultObject();
                    }
                }
                return resultado;

            }
            catch (Exception ex)
            {
                throw new DataBaseException("Count", ex);
            }
        }

        public static bool Update(String campo, String tabela, Object valor, String filtro, SqlConnection connection, SqlTransaction transaction = null)
        {
            string sql = @"UPDATE @tabela SET @campo = @valor @where";

            sql = sql.Replace("@tabela", tabela);
            sql = sql.Replace("@campo", campo);
            sql = sql.Replace("@where", string.IsNullOrEmpty(filtro) ? string.Empty : "WHERE " + filtro);

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection, transaction))
                {
                    cmd.Parameters.AddWithValue("@valor", valor);
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new DataBaseException("Update", ex);
            }
        }

        public static bool Delete(String tabela, String filtro, SqlConnection connection, SqlTransaction transaction = null)
        {
            string sql = @"DELETE @tabela @where";

            sql = sql.Replace("@tabela", tabela);
            sql = sql.Replace("@where", string.IsNullOrEmpty(filtro) ? string.Empty : "WHERE " + filtro);

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection, transaction))
                    cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                throw new DataBaseException("Delete", ex);
            }
        }

        public static object Select(String campo, String tabela, String filtro, SqlConnection connection, SqlTransaction transaction = null, String campoVazio = "")
        {
            object resultado = null;
            string sql = @"SELECT @campo As Dado FROM @tabela @where";

            sql = sql.Replace("@campo", campo);
            sql = sql.Replace("@tabela", tabela);
            sql = sql.Replace("@where", string.IsNullOrEmpty(filtro) ? string.Empty : "WHERE " + filtro);

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection, transaction))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            resultado = reader["Dado"].DefaultObject();
                            if (!string.IsNullOrEmpty(campoVazio))
                                if (string.IsNullOrEmpty(resultado.ToString()))
                                    resultado = campoVazio;
                        }
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new DataBaseException("Select", ex);
            }
        }

        public static DataTable Select(String sql, List<SqlParameter> parametros, SqlConnection connection, SqlTransaction transaction = null)
        {
            DataTable tabela = new DataTable("Tabela");
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection, transaction))
                {
                    cmd.Parameters.AddRange(parametros.ToArray());

                    using (tabela = new DataTable("Tabela"))
                        tabela.Load(cmd.ExecuteReader());
                }
                return tabela;
            }
            catch (Exception ex)
            {
                throw new DataBaseException("Select", ex);
            }
        }

        public static DataRowCollection Select(String sql, SqlConnection connection, SqlTransaction transaction = null)
        {
            DataTable tabela = new DataTable("Tabela");
            using (SqlCommand cmd = new SqlCommand(sql, connection, transaction))
            {
                using (tabela = new DataTable("Tabela"))
                {
                    try
                    {
                        tabela.Load(cmd.ExecuteReader());
                    }
                    catch (Exception ex)
                    {
                        throw new DataBaseException("Select", ex);
                    }
                }
            }
            return tabela.Rows;
        }

        public static List<String> ColunasTabela(String tabela, String coluna, SqlConnection connection, SqlTransaction transaction = null)
        {
            List<String> listColunas = new List<String>();

            string sql = @"SELECT @coluna As col FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @tabela";

            sql = sql.Replace("@coluna", coluna);
            sql = sql.Replace("@tabela", tabela);

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection, transaction))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string col = dr["col"].ToString();
                            listColunas.Add(coluna);
                        }
                    }
                }
                return listColunas;
            }
            catch (Exception ex)
            {
                throw new DataBaseException("ColunasTabela", ex);
            }
        }

        public static string ContraSqlInjection(String sql)
        {
            if (sql == string.Empty || sql == "")
                return sql;

            string sValue = sql;

            sValue = sValue.Replace("'", "''");
            sValue = sValue.Replace("--", " ");
            sValue = sValue.Replace("/*", " ");
            sValue = sValue.Replace("*/", " ");
            sValue = sValue.Replace(" or ", "");
            sValue = sValue.Replace(" and ", "");
            sValue = sValue.Replace("update", "");
            sValue = sValue.Replace("-shutdown", "");
            sValue = sValue.Replace("--", "");
            sValue = sValue.Replace("'or'1'='1'", "");
            sValue = sValue.Replace("insert", "");
            sValue = sValue.Replace("drop", "");
            sValue = sValue.Replace("delete", "");
            sValue = sValue.Replace("xp_", "");
            sValue = sValue.Replace("sp_", "");
            sValue = sValue.Replace("select", "");
            sValue = sValue.Replace("1 union select", "");

            return sValue;
        }
    }
}
