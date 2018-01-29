using System.IO;
using System.Text;

namespace MetodosUteis
{
    /*********************************************************************************
    * 
    * Classe: CustomStringWriter
    * Descrição: StringWriter personalizada
    * 
    * Guilherme Alves
    * guiigos.alves@gmail.com
    * http://guiigos.com
    * 
    *********************************************************************************/

    public class CustomStringWriter : StringWriter
    {
        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }
}
