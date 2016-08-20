using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Funcoes
{
    class CriptografiaCryptoJsException : Exception
    {
        public CriptografiaCryptoJsException(String metodo, Exception ex) :
            base("Funcoes.CriptografiaCryptoJsAES." + metodo + " - " + ex.Message) { }
    }

    public class CriptografiaCryptoJs
    {
        private RijndaelManaged rijndaelManaged;
        private const string _iv = "e84ad660c4721ae0e84ad660c4721ae0";
        private const string _salt = "insight123resultxyz";
        private const int _iterations = 1000;

        #region construtor
        public CriptografiaCryptoJs(String senha)
        {
            rijndaelManaged = new RijndaelManaged();
            rijndaelManaged.BlockSize = 128;
            rijndaelManaged.KeySize = 128;

            dynamic iv = new byte[_iv.Length / 2];
            for (int i = 0; i <= _iv.Length - 1; i += 2) 
                iv[i / 2] = Convert.ToByte(Convert.ToInt32(_iv.Substring(i, 2), 16));

            rijndaelManaged.IV = iv;
            rijndaelManaged.Padding = PaddingMode.PKCS7;
            rijndaelManaged.Mode = CipherMode.CBC;

            Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(System.Text.Encoding.UTF8.GetBytes(senha), System.Text.Encoding.UTF8.GetBytes(_salt), _iterations);

            rijndaelManaged.Key = rfc2898.GetBytes(128 / 8);
        }
        #endregion

        #region metodos
        public string Encrypt(string strPlainText)
        {
            try
            {
                byte[] strText = new System.Text.UTF8Encoding().GetBytes(strPlainText);
                ICryptoTransform transform = rijndaelManaged.CreateEncryptor();
                byte[] cipherText = transform.TransformFinalBlock(strText, 0, strText.Length);

                return Convert.ToBase64String(cipherText);
            }
            catch (Exception ex)
            {
                throw new CriptografiaCryptoJsException("Decrypt", ex);
            }
        }

        public string Decrypt(string CipherText)
        {
            try
            {
                byte[] cipherTextBytes = Convert.FromBase64String(CipherText);
                ICryptoTransform transform = rijndaelManaged.CreateDecryptor(rijndaelManaged.Key, rijndaelManaged.IV);
                return System.Text.Encoding.ASCII.GetString(transform.TransformFinalBlock(cipherTextBytes, 0, cipherTextBytes.Length));
            }
            catch (Exception ex)
            {
                throw new CriptografiaCryptoJsException("Decrypt", ex);
            }
        }
        #endregion

        #region javacript
        //Método utilizado para descriptograr no JS utilizando o CryptoJs

        /*
         * function Decrypt(texto, senha) {
         *   try {
         *       var iv = CryptoJS.enc.Hex.parse('e84ad660c4721ae0e84ad660c4721ae0');
         *       var Pass = CryptoJS.enc.Utf8.parse(senha);
         *       var Salt = CryptoJS.enc.Utf8.parse("insight123resultxyz");
         *       var key128Bits1000Iterations = CryptoJS.PBKDF2(Pass.toString(CryptoJS.enc.Utf8), Salt, { keySize: 128 / 32, iterations: 1000 });
         *
         *       var cipherParams = CryptoJS.lib.CipherParams.create({
         *           ciphertext: CryptoJS.enc.Base64.parse(texto)
         *       });
         *
         *       var decrypted = CryptoJS.AES.decrypt(cipherParams, key128Bits1000Iterations, { mode: CryptoJS.mode.CBC, iv: iv, padding: CryptoJS.pad.Pkcs7 });
         *       var plaintext = decrypted.toString(CryptoJS.enc.Utf8);
         *       return plaintext;
         *   }
         *   catch (err) {
         *       return "";
         *   } 
         * }
         */

        //Método utilizado para criptografar no JS utilizando o CryptoJs

        /* function EncryptData(texto, senha) {
         *   try {
         *       var iv = CryptoJS.enc.Hex.parse('e84ad660c4721ae0e84ad660c4721ae0');
         *       var Pass = CryptoJS.enc.Utf8.parse(senha);
         *       var Salt = CryptoJS.enc.Utf8.parse("insight123resultxyz");
         *       var key128Bits1000Iterations = CryptoJS.PBKDF2(Pass.toString(CryptoJS.enc.Utf8), Salt, { keySize: 128 / 32, iterations: 1000 });
         *
         *       var cipherParams = CryptoJS.lib.CipherParams.create({
         *           ciphertext: CryptoJS.enc.Utf8.parse(texto)
         *       });
         *
         *       var encrypted = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(texto), key128Bits1000Iterations, { mode: CryptoJS.mode.CBC, iv: iv, padding: CryptoJS.pad.Pkcs7 });
         *       //var plaintext = decrypted.toString(CryptoJS.enc.Base64);
         *       var plaintext = encrypted.ciphertext.toString(CryptoJS.enc.Base64);
         *       //var plaintext = encrypted.toString();
         *       return plaintext;
         *   }
         *   catch (err) {
         *       return "";
         *   } 
         * }
         */
        #endregion
    }
}
