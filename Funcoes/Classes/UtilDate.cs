using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funcoes
{
    class UtilDateException : Exception
    {
        public UtilDateException(String metodo, Exception ex) :
            base("Funcoes.UtilDate." + metodo + " - " + ex.Message) { }
    }

    public class UtilDate
    {
        /// <summary>
        /// Retorna a diferença entre dias
        /// </summary>
        /// <param name="dtInicial">Data inicial</param>
        /// <param name="dtFinal">Data final</param>
        /// <param name="semHoras">TRUE para contar as horas, FALSE para ignorar</param>
        /// <returns>Retorna a diferença entre dias</returns>
        public static int DiferencaDias(DateTime dtInicial, DateTime dtFinal, Boolean semHoras)
        {
            if (semHoras) return dtFinal.Date.Subtract(dtInicial.Date).Days;
            return dtFinal.Subtract(dtInicial).Days;
        }

        /// <summary>
        /// Truca um DateTime
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="inicial">TRUE para trucar para inicial</param>
        /// <returns>Retorna a data truncada</returns>
        public static string TruncarData(DateTime data, Boolean inicial)
        {
            try
            {
                if (inicial) return string.Format("{0:yyyy-MM-dd}", data) + " 00:00:00.000";
                else return string.Format("{0:yyyy-MM-dd}", data) + " 23:59:59.999";
            }
            catch (Exception ex)
            {
                throw new UtilDateException("TruncarData", ex);
            }
        }

        /// <summary>
        /// Retorna a data por extenso
        /// </summary>
        /// <param name="data">Data</param>
        /// <returns>Retorna uma string com a data por extenso</returns>
        public static string DataExenso(DateTime data)
        {
            try
            {
                CultureInfo culture = new CultureInfo("pt-BR");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;

                int dia = data.Day;
                int ano = data.Year;
                string mes = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(data.Month));
                string diasemana = culture.TextInfo.ToTitleCase(dtfi.GetDayName(data.DayOfWeek));
                string dt = diasemana + " dia " + dia + " de " + mes + " de " + ano;

                return dt;
            }
            catch (Exception ex)
            {
                throw new UtilDateException("DataExenso", ex);
            }
        }

        /// <summary>
        /// Retorna o dia da semana
        /// </summary>
        /// <param name="diaDaSemana">Dia da semana</param>
        /// <param name="abreviado">Abreviado ou não</param>
        /// <returns>Retorna o dia da semana</returns>
        public static string DiaSemana(DayOfWeek diaDaSemana, Boolean abreviado)
        {
            switch (diaDaSemana)
            {
                case DayOfWeek.Sunday:
                    return abreviado ? "Dom" : "Domingo";
                case DayOfWeek.Monday:
                    return abreviado ? "Seg" : "Segunda-Feira";
                case DayOfWeek.Tuesday:
                    return abreviado ? "Ter" : "Terça-Feira";
                case DayOfWeek.Wednesday:
                    return abreviado ? "Qua" : "Quarta-Feira";
                case DayOfWeek.Thursday:
                    return abreviado ? "Qui" : "Quinta-Feira";
                case DayOfWeek.Friday:
                    return abreviado ? "Sex" : "Sexta-Feira";
                case DayOfWeek.Saturday:
                    return abreviado ? "Sab" : "Sábado";
                default:
                    return String.Empty;
            }
        }

        /// <summary>
        /// Zera o time da data
        /// </summary>
        /// <param name="data">Data</param>
        /// <returns>Retorna a data com o time zerado</returns>
        public static DateTime ZerarTime(DateTime data)
        {
            return new DateTime(data.Year, data.Month, data.Day);
        }
    }
}
