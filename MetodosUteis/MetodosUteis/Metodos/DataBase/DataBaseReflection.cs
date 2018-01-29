using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace MetodosUteis
{
    /*********************************************************************************
    * 
    * Classe: DataBaseReflection
    * Descrição: Reflections ORM
    * 
    * Guilherme Alves
    * guiigos.alves@gmail.com
    * http://guiigos.com
    * 
    *********************************************************************************/

    public class DataBaseReflection<T> : IDisposable
    {
        #region variáveis
        private FieldInfo[] _fieldList;
        private Func<T> _instanceCreator;
        #endregion

        #region construtor
        public DataBaseReflection()
        {
            var typeClass = typeof(T);
            _fieldList = typeClass.GetFields();

            var ctor = Expression.New(typeClass.GetConstructor(Type.EmptyTypes));
            _instanceCreator = Expression.Lambda<Func<T>>(ctor).Compile();
        }
        #endregion

        /// <summary>
        /// Leitura dinâmica de um reader classe<typeparamref name="T"/>.
        /// </summary>
        /// <example> 
        /// Exemplo de aplicação do método <see cref="ReadLine"/>.
        /// <code>
        /// var result = new List&lt;<typeparamref name="T"/>&gt;();
        /// var cmdText = "Select * from Table";
        /// using (var cmd = new SqlCommand(cmdText, conn))
        /// using (var reader = cmd.ExecuteReader())
        /// using (var dbUtils = new DbUtils&lt;<typeparamref name="T"/>&gt;())
        /// {
        ///     while (reader.Read())
        ///     {
        ///         var instance = dbUtils.ReadLine(reader);
        ///         result.Add(instance);
        ///     }
        /// }
        /// </code>
        /// </example> 
        /// <param name="dr">IDataReader para se realizar a leitura da linha</param>
        /// <returns>Retorna registro do tipo <typeparamref name="T"/> com os dados encontrados no <paramref name="dr"</returns>
        public T ReadLine(IDataReader dr)
        {
            T instance = _instanceCreator();

            for (int i = 0; i < dr.FieldCount; i++)
            {
                var nomeDoCampo = dr.GetName(i).Replace("_", "");
                foreach (var field in _fieldList)
                {
                    // Compara nome dos campos
                    if (!string.Equals(nomeDoCampo, field.Name, StringComparison.OrdinalIgnoreCase)) continue;

                    // Verifica se valor retornado é nulo
                    if (dr.IsDBNull(i))
                    {
                        // Caso null e property do tipo string carrega string vazia (Não sei porque, mas era feito no preencheObjetos)
                        if (field.FieldType.Equals(typeof(string)))
                        {
                            field.SetValue(instance, string.Empty);
                        }
                        break;
                    }
                    // Carrega tipo do registro no reader
                    var from = dr[i].GetType();

                    // Carrega tipo da property nas classe
                    var to = field.FieldType;

                    // Caso valores do mesmo tipo, atribui-se diretamente
                    if (from.Equals(to))
                    {
                        field.SetValue(instance, dr.GetValue(i));
                        break;
                    }

                    // Compara tipo dos campos
                    if ((from == typeof(double) && (to != typeof(decimal) || to != typeof(int))) ||
                        (from == typeof(string) && to != typeof(char)) ||
                        (from == typeof(decimal) && to != typeof(int)) ||
                        (from == typeof(Single) && to == typeof(decimal)))
                    {
                        break;
                    }

                    // Seta valor a coluna
                    field.SetValue(instance, Convert.ChangeType(dr.GetValue(i), field.FieldType));
                    break;
                }
            }
            return instance;
        }

        /// <summary>
        /// Faz a leitura dos registros do reader para a classe <typeparamref name="T"/>.
        /// </summary>
        /// <example> 
        /// Exemplo de aplicação do método <see cref="ReadLines"/>.
        /// <code>
        /// List&lt;<typeparamref name="T"/>&gt;() result;
        /// var cmdText = "Select * from Table";
        /// using (var cmd = new SqlCommand(cmdText, conn))
        /// using (var reader = cmd.ExecuteReader())
        /// using (var dbUtils = new DbUtils&lt;<typeparamref name="T"/>&gt;())
        /// {
        ///     result = dbUtils.ReadLine(reader);
        /// }
        /// </code>
        /// </example>
        /// <param name="dr">IDataReader para se realizar a leitura dos registros.</param>
        /// <returns>Retorna uma lista de registro do tipo <typeparamref name="T"/> com os dados encontrados no <paramref name="dr"/>.</returns>
        public List<T> ReadLines(IDataReader dr)
        {
            var list = new List<T>();

            // Retorna caso não possua dados
            if (!dr.Read()) return list;

            // Lista de fields para carregar valores
            var fieldsToFill = new List<FieldInfo>();

            // Pega colunas do reader e seus tipos
            var schema = dr.GetSchemaTable();
            var dataColumnList = schema.Columns.Cast<DataColumn>();
            var columnName = dataColumnList.First(dcl => dcl.ColumnName.Equals("ColumnName"));
            var columnOrdinal = dataColumnList.First(dcl => dcl.ColumnName.Equals("ColumnOrdinal"));

            // private class Coluna
            // {
            //    public string ColumnName { get; set; }
            //    public Type FieldType { get; set; }
            // }
            // var colunas = schema.Rows.Cast<DataRow>().Select(x => new Coluna()
            // {
            //    ColumnName = x[columnName].ToString(),
            //    FieldType = dr.GetFieldType(Convert.ToInt32(x[columnOrdinal]))
            // });

            var colunas = schema.Rows.Cast<DataRow>().Select(x => new
            {
                ColumnName = x[columnName].ToString(),
                FieldType = dr.GetFieldType(Convert.ToInt32(x[columnOrdinal]))
            });

            // Separa fields para realizar leitura
            foreach (var coluna in colunas)
            {
                var nomeDoCampo = coluna.ColumnName.Replace("_", string.Empty);
                foreach (var field in _fieldList)
                {
                    // Compara nome dos campos
                    if (!string.Equals(nomeDoCampo, field.Name, StringComparison.OrdinalIgnoreCase)) continue;

                    // Carrega tipo do registro no reader
                    var from = coluna.FieldType;

                    // Carrega tipo da property nas classe
                    var to = field.FieldType;

                    // Caso valores do mesmo tipo
                    if (from.Equals(to))
                    {
                        fieldsToFill.Add(field);
                        break;
                    }

                    // Compara tipo dos campos
                    if ((from == typeof(double) && (to != typeof(decimal) || to != typeof(int))) ||
                        (from == typeof(string) && to != typeof(char)) ||
                        (from == typeof(decimal) && to != typeof(int)) ||
                        (from == typeof(Single) && to == typeof(decimal)))
                    {
                        break;
                    }

                    // Armazena campo na lista
                    fieldsToFill.Add(field);
                    break;
                }
            }

            T instance;
            do
            {
                instance = _instanceCreator();
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    var nomeDoCampo = dr.GetName(i).Replace("_", "");
                    foreach (var field in fieldsToFill)
                    {
                        // Compara nome dos campos
                        if (!string.Equals(nomeDoCampo, field.Name, StringComparison.OrdinalIgnoreCase)) continue;

                        // Verifica se valor retornado é nulo
                        if (dr.IsDBNull(i))
                        {
                            // Caso null e property do tipo string carrega string vazia
                            if (field.FieldType.Equals(typeof(string)))
                            {
                                field.SetValue(instance, string.Empty);
                            }
                            break;
                        }

                        // Seta valor a propriedade, convertendo automaticamente
                        field.SetValue(instance, Convert.ChangeType(dr.GetValue(i), field.FieldType));
                        break;
                    }
                }
                list.Add(instance);
            }
            while (dr.Read());
            return list;
        }

        public void Dispose()
        {
            _fieldList = null;
            _instanceCreator = null;
        }
    }
}
