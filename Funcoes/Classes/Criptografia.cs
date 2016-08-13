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
            int keysize = 256;
            byte[] initVectorBytes = Encoding.ASCII.GetBytes("tu89geji340t89u2");

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
            int keysize = 256;
            byte[] initVectorBytes = Encoding.ASCII.GetBytes("tu89geji340t89u2");

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

        public static string EncryptAES(String text, String pass)
        {
            byte[] key, iv;
            byte[] salt = new byte[8];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(salt);

            Criptografia.AESKeyAndIV(pass, salt, out key, out iv);

            MemoryStream msEncrypt;
            RijndaelManaged aesAlg = null;

            try
            {
                aesAlg = new RijndaelManaged { Mode = CipherMode.CBC, KeySize = 256, BlockSize = 128, Key = key, IV = iv };
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                msEncrypt = new MemoryStream();

                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(text);
                        swEncrypt.Flush();
                        swEncrypt.Close();
                    }
                }
            }
            finally
            {
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            byte[] encryptedBytes = msEncrypt.ToArray();
            byte[] encryptedBytesWithSalt = new byte[salt.Length + encryptedBytes.Length + 8];
            Buffer.BlockCopy(Encoding.ASCII.GetBytes("Salted__"), 0, encryptedBytesWithSalt, 0, 8);
            Buffer.BlockCopy(salt, 0, encryptedBytesWithSalt, 8, salt.Length);
            Buffer.BlockCopy(encryptedBytes, 0, encryptedBytesWithSalt, salt.Length + 8, encryptedBytes.Length);

            return Convert.ToBase64String(encryptedBytesWithSalt);
        }

        public static string DecryptAES(String text, String pass)
        {
            byte[] encryptedBytesWithSalt = Convert.FromBase64String(text);

            byte[] salt = new byte[8];
            byte[] encryptedBytes = new byte[encryptedBytesWithSalt.Length - salt.Length - 8];
            Buffer.BlockCopy(encryptedBytesWithSalt, 8, salt, 0, salt.Length);
            Buffer.BlockCopy(encryptedBytesWithSalt, salt.Length + 8, encryptedBytes, 0, encryptedBytes.Length);

            byte[] key, iv;
            Criptografia.AESKeyAndIV(pass, salt, out key, out iv);

            RijndaelManaged aesAlg = null;
            string plaintext;

            try
            {
                aesAlg = new RijndaelManaged { Mode = CipherMode.CBC, KeySize = 256, BlockSize = 128, Key = key, IV = iv };
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(encryptedBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                            srDecrypt.Close();
                        }
                    }
                }
            }
            finally
            {
                if (aesAlg != null)
                    aesAlg.Clear();
            }
            return plaintext;
        }

        private static void AESKeyAndIV(string pass, byte[] salt, out byte[] key, out byte[] iv)
        {
            List<byte> concatenatedHashes = new List<byte>(48);
            byte[] password = Encoding.UTF8.GetBytes(pass);
            byte[] currentHash = new byte[0];
            MD5 md5 = MD5.Create();
            bool enoughBytesForKey = false;

            while (!enoughBytesForKey)
            {
                int preHashLength = currentHash.Length + password.Length + salt.Length;
                byte[] preHash = new byte[preHashLength];
                Buffer.BlockCopy(currentHash, 0, preHash, 0, currentHash.Length);
                Buffer.BlockCopy(password, 0, preHash, currentHash.Length, password.Length);
                Buffer.BlockCopy(salt, 0, preHash, currentHash.Length + password.Length, salt.Length);
                currentHash = md5.ComputeHash(preHash);
                concatenatedHashes.AddRange(currentHash);
                if (concatenatedHashes.Count >= 48)
                    enoughBytesForKey = true;
            }

            key = new byte[32];
            iv = new byte[16];
            concatenatedHashes.CopyTo(0, key, 0, 32);
            concatenatedHashes.CopyTo(32, iv, 0, 16);
            md5.Clear();
            md5 = null;
        }
    }
}
