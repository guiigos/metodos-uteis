using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Funcoes
{
    class DataBaseException : Exception
    {
        public DataBaseException(String metodo, Exception ex) :
            base("Funcoes.DataBase." + metodo + " - " + ex.Message) { }
    }

    public class DataBase
    {
        /// <summary>
        /// Método que realiza o SUM de uma coluna
        /// </summary>
        /// <param name="campo">Coluna que se deseja somar</param>
        /// <param name="tabela">Tabela do campo</param>
        /// <param name="filtro">Filtro da busca</param>
        /// <param name="connection">Conexão com o banco de dados</param>
        /// <param name="transaction">Transação com o banco de dados (NÃO OBRIGATÓRIO)</param>
        /// <returns>Retorna o SUM da coluna</returns>
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

        /// <summary>
        /// Método que realiza o MAX de uma coluna
        /// </summary>
        /// <param name="campo">Coluna que deseja retornar o maior</param>
        /// <param name="tabela">Tabela do campo</param>
        /// <param name="filtro">Filtro da busca</param>
        /// <param name="connection">Conexão com o banco de dados</param>
        /// <param name="transaction">Transação com o banco de dados (NÃO OBRIGATÓRIO)</param>
        /// <returns>Retorna o MAX da coluna</returns>
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

        /// <summary>
        /// Método que realiza o MIN de uma coluna
        /// </summary>
        /// <param name="campo">Coluna que deseja retornar o menor</param>
        /// <param name="tabela">Tabela do campo</param>
        /// <param name="filtro">Filtro da busca</param>
        /// <param name="connection">Conexão com o banco de dados</param>
        /// <param name="transaction">Transação com o banco de dados (NÃO OBRIGATÓRIO)</param>
        /// <returns>Retorna o MIN da coluna</returns>
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

        /// <summary>
        /// Método que realiza o AVG de uma coluna
        /// </summary>
        /// <param name="campo">Coluna que deseja retornar a média</param>
        /// <param name="tabela">Tabela do campo</param>
        /// <param name="filtro">Filtro da busca</param>
        /// <param name="connection">Conexão com o banco de dados</param>
        /// <param name="transaction">Transação com o banco de dados (NÃO OBRIGATÓRIO)</param>
        /// <returns>Retorna o AVG da coluna</returns>
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

        /// <summary>
        /// Método que realiza o COUNT de uma coluna
        /// </summary>
        /// <param name="campo">Coluna que deseja retornar a contagem</param>
        /// <param name="tabela">Tabela do campo</param>
        /// <param name="filtro">Filtro da busca</param>
        /// <param name="connection">Conexão com o banco de dados</param>
        /// <param name="transaction">Transação com o banco de dados (NÃO OBRIGATÓRIO)</param>
        /// <returns>Retorna o COUNT da coluna</returns>
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

        /// <summary>
        /// Método que realiza o UPDATE de um registro
        /// </summary>
        /// <param name="campo">Coluna que deseja retornar a contagem</param>
        /// <param name="tabela">Tabela do campo</param>
        /// <param name="valor">Valor para que se deseja alterar</param>
        /// <param name="filtro">Filtro da alteração</param>
        /// <param name="connection">Conexão com o banco de dados</param>
        /// <param name="transaction">Transação com o banco de dados (NÃO OBRIGATÓRIO)</param>
        /// <returns>TRUE para realiado e FALSE para não realizado</returns>
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

        /// <summary>
        /// Método que realiza o DELETE de um registro
        /// </summary>
        /// <param name="tabela">Tabela do registro</param>
        /// <param name="filtro">Filtro da exclusão</param>
        /// <param name="connection">Conexão com o banco de dados</param>
        /// <param name="transaction">Transação com o banco de dados (NÃO OBRIGATÓRIO)</param>
        /// <returns>TRUE para realiado e FALSE para não realizado</returns>
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

        /// <summary>
        /// Método que realiza o SELECT de um registro
        /// </summary>
        /// <param name="campo">Coluna do registro</param>
        /// <param name="tabela">Tabela do registro</param>
        /// <param name="filtro">Filtro da seleção</param>
        /// <param name="connection">Conexão com o banco de dados</param>
        /// <param name="transaction">Transação com o banco de dados (NÃO OBRIGATÓRIO)</param>
        /// <param name="campoVazio">Campo defoult para retorno (NÃO OBRIGATÓRIO)</param>
        /// <returns>Retorna o resultado do SELECT</returns>
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

        /// <summary>
        /// Método que realiza o SELECT de um registro passando o SQL retornando um DataTable
        /// </summary>
        /// <param name="sql">SQL da consulta</param>
        /// <param name="parametros">Lista de parâmetros do SQL</param>
        /// <param name="connection">Conexão com o banco de dados</param>
        /// <param name="transaction">Transação com o banco de dados (NÃO OBRIGATÓRIO)</param>
        /// <returns>Retorna o resultado do SELECT em um DataTable</returns>
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

        /// <summary>
        /// Método que realiza o SELECT de um registro passando o SQL retornando um DataRowCollection
        /// </summary>
        /// <param name="sql">SQL da consulta</param>
        /// <param name="connection">Conexão com o banco de dados</param>
        /// <param name="transaction">Transação com o banco de dados (NÃO OBRIGATÓRIO)</param>
        /// <returns>Retorna o resultado do SELECT em um DataRowCollection</returns>
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

        /// <summary>
        /// Método que realiza o SELECT de uma coluna da tabela retornando uma lista de valores
        /// </summary>
        /// <param name="tabela">Tabela do registro</param>
        /// <param name="coluna">Coluna do registro</param>
        /// <param name="connection">Conexão com o banco de dados</param>
        /// <param name="transaction">Transação com o banco de dados (NÃO OBRIGATÓRIO)</param>
        /// <returns>Retorna uma lista de valores da coluna filtrada</returns>
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

        /// <summary>
        /// Método que criar os parametros e concactena os seus valores em suas respectivas condições
        /// </summary>
        /// <param name="sqlCommand">SqlCommand da operação</param>
        /// <param name="sql">SQL a ser executado</param>
        /// <param name="parametros">Arry com os Parametros</param>
        public static void CreateParametersFromString(SqlCommand sqlCommand, String sql, Array parametros)
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
                throw new DataBaseException("CreateParametersFromString", ex);
            }
        }

        /// <summary>
        /// Método que verifica se o existe algum parametro já adicionado no arry de parametros
        /// </summary>
        /// <param name="parametros">Arry de Parametros</param>
        /// <param name="name">Nome do parametro a ser comparado</param>
        /// <returns>TRUE para existe e FALSE para não existe</returns>
        private static bool ExisteParametro(SqlParameterCollection parametros, String name)
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
                throw new DataBaseException("ExisteParametro", ex);
            }
        }

        /// <summary>
        /// Método que cria uma String SQL pra INSERT ou UPDATE
        /// </summary>
        /// <param name="tabela">Tabela do banco</param>
        /// <param name="update">TRUE para UPDATE e FALSE para INSERT</param>
        /// <param name="where">No caso de ser um Update, passa a claúsula WHERE</param>
        /// <param name="obj">Objeto que vai ser usado para passar os valores</param>
        /// <param name="connection">Conexão com o banco de dados</param>
        /// <param name="transaction">Transação com o banco de dados (NÃO OBRIGATÓRIO)</param>
        /// <returns>retorn um string com o comando</returns>
        public static string MontaSQL(String tabela, Boolean update, String where, Object obj, SqlConnection connection, SqlTransaction transaction = null)
        {
            string consulta = string.Empty;
            string sql = @"SELECT DISTINCT OBJECT_NAME(object_id) AS Tabela, c.name AS Coluna, ISNULL(st.type, t.name) AS TipoDados
                         FROM sys.COLUMNS c
                              INNER JOIN sys.types t ON t.system_type_id = c.system_type_id 
                              LEFT JOIN 
                                    (
                                    select t.system_type_id, sd.DATA_TYPE AS type
                                    from sys.systypes st
                                    inner join sys.types t on t.system_type_id = st.xtype and t.system_type_id != t.user_type_id
                                    inner join INFORMATION_SCHEMA.DOMAINS sd on sd.domain_name = st.name
                                    ) st ON st.system_type_id=c.system_type_id
                        WHERE is_identity = 'false' AND OBJECT_NAME(object_id) = '" + tabela + @"' AND t.name <> 'sysname'
                        ORDER BY Tabela, Coluna";


            string values = "VALUES (";
            using (SqlCommand cmd = new SqlCommand(sql, connection, transaction))
            {
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (!update) consulta = "INSERT INTO " + tabela + "(";
                    else  consulta = "UPDATE " + tabela + " SET ";

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
                                Boolean existeWHERE = false;
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
                    else  consulta += " WHERE " + where;
                    return consulta;
                }
            }
        }

        /// <summary>
        /// Método responsável por preencher o objeto com um SqlDataReader
        /// </summary>
        /// <param name="reader">SqlDataReader que contem os dados das Colunas</param>
        /// <param name="objeto">Objeto que receberá os valores</param>
        public static void PreencheObjetos(SqlDataReader reader, Object objeto)
        {
            try
            {
                FieldInfo[] propriedades = objeto.GetType().GetFields();
                String nomeDoCampo = String.Empty;

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    nomeDoCampo = reader.GetName(i);

                    foreach (FieldInfo p in propriedades)
                    {
                        if (p.Name.ToUpper().Equals(nomeDoCampo.ToUpper()))
                        {
                            if (reader[i].GetType().Name == System.TypeCode.Decimal.ToString())
                            {
                                if (p.FieldType.Name == System.TypeCode.Decimal.ToString())  p.SetValue(objeto, Convert.ToDecimal(reader.GetValue(i)));
                                else p.SetValue(objeto, Convert.ToInt32(reader.GetValue(i)));
                            }
                            else if (reader[i].GetType().Name == System.TypeCode.Double.ToString() && p.FieldType.Name == System.TypeCode.Decimal.ToString())
                            {
                                p.SetValue(objeto, Convert.ToDecimal(reader.GetValue(i)));
                            }
                            else if (reader[i].GetType().Name == System.TypeCode.String.ToString())
                            {
                                if (p.FieldType.Name == System.TypeCode.Char.ToString()) p.SetValue(objeto, Convert.ToChar(reader.GetValue(i)));
                                else  p.SetValue(objeto, reader.GetValue(i).ToString().Trim());
                            }
                            else if (reader[i].GetType().Name == System.TypeCode.Single.ToString())
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
                                    if (p.FieldType.Name == System.TypeCode.String.ToString())
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
                throw new DataBaseException("preencheObjetos", ex);
            }
        }

        /// <summary>
        /// Método responsável por preencher o objeto com um DataRow
        /// </summary>
        /// <param name="reader">DataRow que contem os dados das Colunas</param>
        /// <param name="objeto">Objeto que receberá os valores</param>
        public static void PreencheObjetos(DataRow reader, Object objeto)
        {
            FieldInfo[] propriedades = objeto.GetType().GetFields();
            String nomeBusca = String.Empty;

            for (int i = 0; i < reader.Table.Columns.Count; i++)
            {
                foreach (FieldInfo p in propriedades)
                {
                    nomeBusca = reader.Table.Columns[i].ToString();

                    if (nomeBusca.ToUpper().Equals(p.Name.ToUpper()))
                    {
                        if (reader[nomeBusca].GetType().Name == System.TypeCode.Decimal.ToString())
                        {
                            if (p.FieldType.Name == System.TypeCode.Decimal.ToString()) p.SetValue(objeto, Convert.ToDecimal(reader[nomeBusca]));
                            else p.SetValue(objeto, Convert.ToInt32(reader[nomeBusca]));
                        }
                        else if (reader[nomeBusca].GetType().Name == System.TypeCode.Double.ToString() && p.FieldType.Name == System.TypeCode.Decimal.ToString())
                        {
                            p.SetValue(objeto, Convert.ToDecimal(reader[nomeBusca]));
                        }
                        else if (reader[nomeBusca].GetType().Name == System.TypeCode.String.ToString())
                        {
                            if (p.FieldType.Name == System.TypeCode.Char.ToString()) p.SetValue(objeto, Convert.ToChar(reader[nomeBusca]));
                            else p.SetValue(objeto, reader[nomeBusca].ToString().Trim());
                        }
                        else if (reader[nomeBusca].GetType().Name == System.TypeCode.Single.ToString())
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
                                if (p.FieldType.Name == System.TypeCode.String.ToString())
                                    p.SetValue(objeto, "");
                            }
                            else p.SetValue(objeto, reader[nomeBusca]);
                        }
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Método responsável por criar os parametros de um sqlCommand
        /// </summary>
        /// <param name="sqlCommand">SqlCommand onde será adicionado os parametros</param>
        /// <param name="objeto">Objeto que contem os valores dos parametros</param>
        /// <param name="parametrosExcecao">Lista de nomes de parametros que fogem à regra</param>
        public static void AddParametros(SqlCommand sqlCommand, Object objeto, Boolean assumirValorNulo, List<String> parametrosExcecao)
        {
            FieldInfo[] propriedades = objeto.GetType().GetFields();

            foreach (FieldInfo p in propriedades)
            {
                SqlParameter param = sqlCommand.CreateParameter();
                try
                {
                    System.Data.SqlClient.SqlParameter p1 = null;
                    System.ComponentModel.TypeConverter tc = null;
                    p1 = new System.Data.SqlClient.SqlParameter();
                    tc = System.ComponentModel.TypeDescriptor.GetConverter(p1.DbType);

                    if (tc.CanConvertFrom(p.FieldType))
                    {
                        p1.DbType = (System.Data.DbType)tc.ConvertFrom(p.FieldType.Name);
                    }
                    else
                    {
                        try
                        {
                            p1.DbType = (System.Data.DbType)tc.ConvertFrom(p.FieldType.Name);
                        }
                        catch (Exception ex)
                        {
                            throw new DataBaseException("addParametros", ex);
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
                else if (Microsoft.VisualBasic.Information.IsDate(parameter.Value))
                {
                    if (UtilDate.ZerarTime(Convert.ToDateTime(parameter.Value)) == new DateTime())  parameter.Value = DBNull.Value;
                    else if (UtilDate.ZerarTime(Convert.ToDateTime(parameter.Value)) == new DateTime(1900, 01, 01)) parameter.Value = DBNull.Value;
                }
            }
        }

        /// <summary>
        /// Método que trata ataques de SQL injection
        /// </summary>
        /// <param name="sql">SQL a ser executado</param>
        /// <returns>Retorna o SQL informado com o tratamento realizado</returns>
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
