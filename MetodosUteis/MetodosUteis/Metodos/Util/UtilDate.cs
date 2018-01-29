using System;
using System.Globalization;
using System.Reflection;

namespace MetodosUteis
{
    /*********************************************************************************
    * 
    * Classe: UtilDate
    * Descrição: Métodos complementares para DateTime
    * 
    * Guilherme Alves
    * guiigos.alves@gmail.com
    * http://guiigos.com
    * 
    *********************************************************************************/

    public class UtilDate
    {
        public static int DiferencaDias(DateTime dtInicial, DateTime dtFinal, bool semHoras)
        {
            try
            {
                if (semHoras) return dtFinal.Date.Subtract(dtInicial.Date).Days;
                return dtFinal.Subtract(dtInicial).Days;
            }
            catch (Exception ex)
            {
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static string TruncarData(DateTime data, bool inicial)
        {
            try
            {
                if (inicial) return string.Format("{0:yyyy-MM-dd}", data) + " 00:00:00.000";
                else return string.Format("{0:yyyy-MM-dd}", data) + " 23:59:59.999";
            }
            catch (Exception ex)
            {
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

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
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static string DiaSemana(DayOfWeek diaDaSemana, bool abreviado)
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
                    return string.Empty;
            }
        }

        public static DateTime ZerarTime(DateTime data)
        {
            return new DateTime(data.Year, data.Month, data.Day);
        }
    }
}
