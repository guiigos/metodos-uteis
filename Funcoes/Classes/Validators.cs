using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Funcoes
{
    class ValidatorsException : Exception
    {
        public ValidatorsException(String metodo, Exception ex) :
            base("Funcoes.Validators." + metodo + " - " + ex.Message) { }

        public ValidatorsException(String metodo) :
            base("Funcoes.Validators." + metodo) { }
    }

    public class Validators
    {
        public static bool ValidaEnderecoEmail(String enderecoEmail)
        {
            try
            {
                string texto_Validar = enderecoEmail;
                Regex expressaoRegex = new Regex(@"\w+@[a-zA-Z_-]+?\.[a-zA-Z]{2,3}");

                if (expressaoRegex.IsMatch(texto_Validar)) return true;
                else return false;
            }
            catch (Exception ex)
            {
                throw new ValidatorsException("ValidaEnderecoEmail", ex);
            }
        }

        public static bool ValidaCnpj(String cnpj)
        {
            try
            {
                int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

                int soma;
                int resto;
                string digito;
                string tempCnpj;

                cnpj = cnpj.Trim();
                cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
                
                if (cnpj.Length != 14) return false;
                
                tempCnpj = cnpj.Substring(0, 12);
                soma = 0;
                
                for (int i = 0; i < 12; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
                
                resto = (soma % 11);
                if (resto < 2) resto = 0;
                else resto = 11 - resto;

                digito = resto.ToString();
                tempCnpj = tempCnpj + digito;
                soma = 0;

                for (int i = 0; i < 13; i++) soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
                
                resto = (soma % 11);
                if (resto < 2) resto = 0;
                else resto = 11 - resto;
                digito = digito + resto.ToString();
                return cnpj.EndsWith(digito);
            }
            catch (Exception ex)
            {
                throw new ValidatorsException("ValidaCnpj", ex);
            }
        }

        public static bool ValidaCpf(String cpf)
        {
            try
            {
                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

                string tempCpf;
                string digito;
                int soma;
                int resto;

                cpf = cpf.Trim();
                cpf = cpf.Replace(".", "").Replace("-", "");

                if (cpf.Length != 11) return false;
                tempCpf = cpf.Substring(0, 9);
                soma = 0;

                for (int i = 0; i < 9; i++) soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                
                resto = soma % 11;
                if (resto < 2) resto = 0;
                else resto = 11 - resto;

                digito = resto.ToString();
                tempCpf = tempCpf + digito;
                soma = 0;
                for (int i = 0; i < 10; i++) soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
               
                resto = soma % 11;
                if (resto < 2) resto = 0;
                else resto = 11 - resto;

                digito = digito + resto.ToString();
                return cpf.EndsWith(digito);
            }
            catch (Exception ex)
            {
                throw new ValidatorsException("ValidaCpf", ex);
            }
        }

        public static bool ValidaPis(String pis)
	    {
            try
            {
                int[] multiplicador = new int[10] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int soma;
                int resto;
                
                if (pis.Trim().Length != 11) return false;
                pis = pis.Trim();
                pis = pis.Replace("-", "").Replace(".", "").PadLeft(11, '0');

                soma = 0;
                for (int i = 0; i < 10; i++) soma += int.Parse(pis[i].ToString()) * multiplicador[i];
                resto = soma % 11;
                
                if (resto < 2) resto = 0;
                else resto = 11 - resto;
                return pis.EndsWith(resto.ToString());
            }
            catch (Exception ex)
            {
                throw new ValidatorsException("ValidaPis", ex);
            }
		}
    }
}
