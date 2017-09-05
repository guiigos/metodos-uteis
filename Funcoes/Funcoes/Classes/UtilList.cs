using System;
using System.Collections.Generic;

namespace Funcoes
{
    class UtilListException : Exception
    {
        public UtilListException(String metodo, Exception ex) :
            base("Funcoes.UtilList." + metodo + " - " + ex.Message)
        { }
    }

    public class UtilList
    {
        /// <summary>
        /// Divide lista
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lista">Lista a ser dividida</param>
        /// <param name="quantidadePorLista">Quantidade de itens por lista</param>
        /// <returns>Retorna as listas divididas</returns>
        public static List<List<T>> DivideLista<T>(List<T> lista, int quantidadePorLista)
        {
            try
            {
                var retorno = new List<List<T>>();
                for (int x = 0; x < lista.Count; x += quantidadePorLista)
                    retorno.Add(lista.GetRange(x, Math.Min(quantidadePorLista, lista.Count - x)));
                return retorno;
            }
            catch (Exception ex)
            {
                throw new UtilListException("DivideLista", ex);
            }
            
        }
    }
}
