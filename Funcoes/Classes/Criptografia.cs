using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Funcoes
{
    class CriptografiaException : Exception
    {
        public CriptografiaException(String metodo, Exception ex) :
            base("Funcoes.Criptografia." + metodo + " - " + ex.Message) { }
    }

    public class Criptografia
    {
        #region constantes
        private const int keysize = 256;
        private static readonly byte[] initVectorBytes = Encoding.ASCII.GetBytes("tu89geji340t89u2");
        #endregion

        public static string EncryptSHA(String texto)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] arrayData = System.Text.Encoding.Default.GetBytes(texto);
                byte[] arrayHash = sha1.ComputeHash(arrayData);

                for (int i = 0; i <= arrayHash.Length - 1; i++) sb.Append(arrayHash[i].ToString("x2"));

                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new CriptografiaException("EncryptSHA", ex);
            }
        }

        public static string EncryptASCHII(String texto)
        {
            try
            {
                Byte[] by = System.Text.ASCIIEncoding.ASCII.GetBytes(texto);
                string encrytada = Convert.ToBase64String(by);
                return encrytada;
            }
            catch (Exception ex)
            {
                throw new CriptografiaException("EncryptASCHII", ex);
            }
        }

        public static string DecryptASCHII(String texto)
        {
            try
            {
                Byte[] by = Convert.FromBase64String(texto);
                string decrypt = System.Text.ASCIIEncoding.ASCII.GetString(by);

                return decrypt;
            }
            catch (Exception ex)
            {
                throw new CriptografiaException("DecryptASCHII", ex);
            }
        }

        public static string EncryptMD5(String texto)
        {
            try
            {
                MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
                byte[] by = null;
                string hash = string.Empty;

                by = provider.ComputeHash(System.Text.Encoding.UTF8.GetBytes(texto));
                provider.Clear();

                hash = BitConverter.ToString(by).Replace("-", string.Empty);

                return hash.ToString().ToLower();
            }
            catch (Exception ex)
            {
                throw new CriptografiaException("EncryptMD5", ex);
            }
        }

        public static string EncryptPass(String text, String pass)
        {
            try
            {
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(text);
                using (PasswordDeriveBytes password = new PasswordDeriveBytes(pass, null))
                {
                    byte[] keyBytes = password.GetBytes(keysize / 8);
                    using (RijndaelManaged symmetricKey = new RijndaelManaged())
                    {
                        symmetricKey.Mode = CipherMode.CBC;
                        using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes))
                        using (MemoryStream memoryStream = new MemoryStream())
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                            cryptoStream.FlushFinalBlock();
                            byte[] cipherTextBytes = memoryStream.ToArray();
                            return Convert.ToBase64String(cipherTextBytes);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CriptografiaException("EncryptPass", ex);
            }
        }

        public static string DecryptPass(String text, String pass)
        {
            try
            {
                byte[] cipherTextBytes = Convert.FromBase64String(text);
                using (PasswordDeriveBytes password = new PasswordDeriveBytes(pass, null))
                {
                    byte[] keyBytes = password.GetBytes(keysize / 8);
                    using (RijndaelManaged symmetricKey = new RijndaelManaged())
                    {
                        symmetricKey.Mode = CipherMode.CBC;
                        using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes))
                        using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CriptografiaException("DecryptPass", ex);
            }
        }
    }
}
