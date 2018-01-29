using System;

namespace MetodosUteis
{
    /*********************************************************************************
    * 
    * Classe: CustomException
    * Descrição: Exceção personalizada
    * 
    * Guilherme Alves
    * guiigos.alves@gmail.com
    * http://guiigos.com
    * 
    *********************************************************************************/

    class CustomException : Exception
    {
        public CustomException(string className, string functionName, Exception ex) :
            base(string.Format("Funcoes.{0}.{1}: {2}", className, functionName, ex.Message))
        { }
    }
}
