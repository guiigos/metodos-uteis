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
        private static string ConverteColunaToAtributo(String nomeNoBanco)
        {
            String primeiroCaractere = String.Empty;
            primeiroCaractere = nomeNoBanco.Substring(0, 1).ToLower();
            return primeiroCaractere + nomeNoBanco.Remove(0, 1).Replace("_", "");
        }

        private static SqlDbType GetDbType(System.Type theType)
        {
            System.Data.SqlClient.SqlParameter p1 = null;
            System.ComponentModel.TypeConverter tc = null;
            p1 = new System.Data.SqlClient.SqlParameter();
            tc = System.ComponentModel.TypeDescriptor.GetConverter(p1.DbType);
            if (tc.CanConvertFrom(theType))
            {
                p1.DbType = (System.Data.DbType)tc.ConvertFrom(theType.Name);
            }
            else
            {
                try
                {
                    p1.DbType = (System.Data.DbType)tc.ConvertFrom(theType.Name);
                }
                catch (Exception ex)
                {
                    throw new DataBaseException("GetDbType", ex);
                }
            }
            return p1.SqlDbType;
        }


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

        public static List<T> Select<T>(String sql, T objeto, SqlConnection connection, SqlTransaction transaction = null)
        {
            try
            {
                List<T> resultado = new List<T>();
                if (!string.IsNullOrEmpty(sql))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, connection, transaction))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    T obj = (T)Activator.CreateInstance(objeto.GetType());
                                    PreencheObjetos(reader, obj);
                                    resultado.Add(obj);
                                }
                            }
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

        public static string MontaSql(String tabela, Boolean update, String where, SqlConnection connection, SqlTransaction transaction = null, Object objetoCompara = null)
        {
            string consulta = String.Empty;
            string values = "VALUES (";
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
                        WHERE is_identity = 'false' AND OBJECT_NAME(object_id) = @tabela AND t.name <> 'sysname'
                        ORDER BY Tabela, Coluna";

            sql = sql.Replace("@tabela", tabela.QueryString());


            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection, transaction))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (!update) consulta = "INSERT INTO " + tabela + "(";
                        else consulta = "UPDATE " + tabela + " SET ";

                        while (dr.Read())
                        {
                            Boolean prossegue = true;
                            if (objetoCompara != null)
                            {
                                prossegue = false;
                                FieldInfo[] propriedades = objetoCompara.GetType().GetFields();
                                foreach (FieldInfo p in propriedades)
                                    if (p.Name == ConverteColunaToAtributo(dr["Coluna"].ToString()))
                                        prossegue = true;
                            }


                            if (prossegue)
                            {
                                if (!update)
                                {
                                    consulta += dr["Coluna"].ToString() + ", ";
                                    values += "@" + ConverteColunaToAtributo(dr["Coluna"].ToString()) + ", ";
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
                                    if (!existeWHERE) consulta += dr["Coluna"].ToString() + " = @" + ConverteColunaToAtributo(dr["Coluna"].ToString()) + ", ";
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
                throw new DataBaseException("MontaSql", ex);
            }
        }

        public static void AddParametros(SqlCommand cmd, Object objeto, Boolean assumirValorNulo, List<String> parametrosExcecao)
        {
            try
            {
                // obtem os atributos da instancia do objeto, no caso das propriedades usaria objeto.GetType().GetProperties()
                FieldInfo[] propriedades = objeto.GetType().GetFields();

                // percorro os atributos do objeto
                foreach (FieldInfo p in propriedades)
                {
                    // crio o parametro
                    SqlParameter param = cmd.CreateParameter();
                    try
                    {
                        // obtenho o tipo correspondente no banco String = nvarchar, etc.
                        param.SqlDbType = GetDbType(p.FieldType);
                    }
                    catch
                    {
                        continue; // se a funcao GetDBType não conseguir encontrar uma tipagem do banco correspondente, aqui é ignorado o parametro e passado para o proximo atributo
                    }

                    param.ParameterName = "@" + p.Name;
                    param.Value = p.GetValue(objeto);

                    // adiciono o parametro ao cmd
                    cmd.Parameters.Add(param);
                }

                // percorro os parametros gerados
                foreach (SqlParameter parameter in cmd.Parameters)
                {
                    if (parameter.Value == null)
                    {
                        parameter.Value = DBNull.Value;
                    }
                    //se o parametro tiver no value uma string vazia, é setado nulo
                    else if (parameter.Value.ToString() == string.Empty)
                    {
                        parameter.Value = DBNull.Value;
                    }
                    //por padrão essa sobrecarga de método verifica quando o parametro é do tipo int, 
                    //analisa uma lista de excessoes que não aderem a regra(quando o valor inteiro for igual a zero estabelecer nulo, devido utilização de foreign key)
                    //Caso o parametro assumir nulo for passado como true, os valores inteiros zerados assumirão nulo, desde que não estejam na lista de excessões.
                    //Caso o parametro assumir nulo for passado como false, os valores inteiros zerados continuarão zerados, desde que não estejam na lista de excessões, nesse caso assumirá nulo
                    else if (parameter.SqlDbType == SqlDbType.Int)
                    {
                        Boolean eExcecao = false;
                        for (int i = 0; i < parametrosExcecao.Count; i++)
                        {
                            if (parameter.ParameterName == parametrosExcecao[i])
                            {
                                eExcecao = true;
                                parametrosExcecao.RemoveAt(i);
                                break;
                            }
                        }
                        if ((assumirValorNulo && !eExcecao) || (!assumirValorNulo && eExcecao))
                        {
                            if (Convert.ToInt32(parameter.Value) == 0) { parameter.Value = DBNull.Value; }
                        }
                    }
                    //Utiliza uma funcao do VB para analisar se o parametro caracteriza data
                    else if (Microsoft.VisualBasic.Information.IsDate(parameter.Value))
                    {
                        //se a data for igual a data padrão do C#, isso quer dizer que não corresponte a um valor válido, então é assumido nulo
                        if (Convert.ToDateTime(parameter.Value).ZerarTime() == new DateTime())
                        {
                            parameter.Value = DBNull.Value;
                        }
                        //se a data for igual a 01/01/1900(data minima do banco) também é consideradao nulo
                        else if (Convert.ToDateTime(parameter.Value).ZerarTime() == new DateTime(1900, 01, 01))
                        {
                            parameter.Value = DBNull.Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataBaseException("AddParametros", ex);
            }
        }

        public static void PreencheObjetos(SqlDataReader reader, Object objeto)
        {
            try
            {
                // obtem os atributos da instancia do objeto, no caso das propriedades usaria objeto.GetType().GetProperties()
                FieldInfo[] propriedades = objeto.GetType().GetFields();
                String nomeDoCampo = String.Empty;
                String primeiroCaractere = String.Empty;

                // percorro as colunas do meu reader
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    nomeDoCampo = reader.GetName(i);
                    primeiroCaractere = nomeDoCampo.Substring(0, 1).ToLower();
                    nomeDoCampo = primeiroCaractere + nomeDoCampo.Remove(0, 1).Replace("_", "");

                    // percorro os atributos do objeto
                    foreach (FieldInfo p in propriedades)
                    {
                        //se o campo do banco corresponde ao atributo
                        if (p.Name.ToUpper().Equals(nomeDoCampo.ToUpper()))
                        {
                            // se o campo do banco se apresenta como decimal
                            if (reader[i].GetType().Name == System.TypeCode.Decimal.ToString())
                            {
                                //se o atributo da classe corresponde a decimal tambem
                                if (p.FieldType.Name == System.TypeCode.Decimal.ToString())
                                {
                                    p.SetValue(objeto, Convert.ToDecimal(reader.GetValue(i)));
                                }
                                // se o campo da classe for diferente de decimal considero que o campo seja numeric(15,0) entao converto pra INT32
                                else
                                {
                                    p.SetValue(objeto, Convert.ToInt32(reader.GetValue(i)));
                                }
                            }
                            else if (reader[i].GetType().Name == System.TypeCode.Double.ToString() &&
                               p.FieldType.Name == System.TypeCode.Decimal.ToString())
                            {
                                p.SetValue(objeto, Convert.ToDecimal(reader.GetValue(i)));
                            }

                            // se o campo do banco se apresentar como string
                            else if (reader[i].GetType().Name == System.TypeCode.String.ToString())
                            {
                                // se o atributo da classe for char, converto
                                if (p.FieldType.Name == System.TypeCode.Char.ToString())
                                {
                                    p.SetValue(objeto, Convert.ToChar(reader.GetValue(i)));
                                }
                                else
                                {
                                    p.SetValue(objeto, reader.GetValue(i).ToString().Trim());
                                }
                            }

                            // se o campo do corresponder a Single, quer dizer que o campo no banco é real, então converto para Decimal (padrão)
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
                                // caso não corresponda a nenhum dos tipo acima vejo se o campo é nulo
                                if (reader.GetValue(i) == DBNull.Value)
                                {
                                    // se for string, jogo uma string vazia, pra não deixá-la nula
                                    if (p.FieldType.Name == System.TypeCode.String.ToString())
                                    {
                                        p.SetValue(objeto, String.Empty);
                                    }
                                }
                                //caso o campo não for nulo, então deixo que o próprio metodo SetValue defina automaticamente
                                //Caso o tipo do campo do banco, não corresponda ao tipo do atributo poderá estourar uma excessão
                                else
                                {
                                    p.SetValue(objeto, reader.GetValue(i));
                                }
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataBaseException("PreencheObjetos", ex);
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
