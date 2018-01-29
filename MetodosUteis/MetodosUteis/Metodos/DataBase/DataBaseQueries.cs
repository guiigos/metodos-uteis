using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace MetodosUteis
{
    /*********************************************************************************
    * 
    * Classe: BasicQueries
    * Descrição: Métodos uteis para queries básicas
    * 
    * Guilherme Alves
    * guiigos.alves@gmail.com
    * http://guiigos.com
    * 
    *********************************************************************************/

    public class DataBaseQueries
    {
        public static object Sum(string campo, string tabela, string filtro, SqlConnection connection, SqlTransaction transaction = null)
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
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static object Max(string campo, string tabela, string filtro, SqlConnection connection, SqlTransaction transaction = null)
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
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static object Min(string campo, string tabela, string filtro, SqlConnection connection, SqlTransaction transaction = null)
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
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static object Avg(string campo, string tabela, string filtro, SqlConnection connection, SqlTransaction transaction = null)
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
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static object Count(string campo, string tabela, string filtro, SqlConnection connection, SqlTransaction transaction = null)
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
                        resultado = reader["Dado"].DefaultObject();
                    }
                }
                return resultado;

            }
            catch (Exception ex)
            {
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static bool Update(string campo, string tabela, object valor, string filtro, SqlConnection connection, SqlTransaction transaction = null)
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
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static bool Delete(string tabela, string filtro, SqlConnection connection, SqlTransaction transaction = null)
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
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static object Select(string campo, string tabela, string filtro, SqlConnection connection, SqlTransaction transaction = null, string campoVazio = "")
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
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static DataTable Select(string sql, List<SqlParameter> parametros, SqlConnection connection, SqlTransaction transaction = null)
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
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static DataRowCollection Select(string sql, SqlConnection connection, SqlTransaction transaction = null)
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
                        throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
                    }
                }
            }
            return tabela.Rows;
        }

        public static List<string> ColunasTabela(string tabela, string coluna, SqlConnection connection, SqlTransaction transaction = null)
        {
            List<string> listColunas = new List<string>();

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
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }
    }
}
