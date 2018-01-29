using System;
using System.Collections.Generic;
using System.Reflection;

namespace MetodosUteis
{
    /*********************************************************************************
    * 
    * Classe: UtilList
    * Descrição: Métodos complementares para List
    * 
    * Guilherme Alves
    * guiigos.alves@gmail.com
    * http://guiigos.com
    * 
    *********************************************************************************/

    public class UtilList
    {
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
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }
    }
}
