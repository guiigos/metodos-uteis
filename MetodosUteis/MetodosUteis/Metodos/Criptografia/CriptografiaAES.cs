using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace MetodosUteis
{
    /*********************************************************************************
    * 
    * Classe: CriptografiaAES
    * Descrição: Métodos de criptografia AES
    * 
    * Guilherme Alves
    * guiigos.alves@gmail.com
    * http://guiigos.com
    * 
    *********************************************************************************/

    public class CriptografiaAES
    {
        #region variáveis
        private string _password;
        private CryptProvider _cryptProvider;
        private SymmetricAlgorithm _symmetricAlgorithm;
        #endregion

        #region enum
        public enum CryptProvider
        {
            Rijndael,
            RC2,
            DES,
            TripleDES
        }
        #endregion

        #region construtor
        public CriptografiaAES(string password, CryptProvider cryptProvider = CryptProvider.Rijndael)
        {
            switch (cryptProvider)
            {
                case CryptProvider.Rijndael:
                    _symmetricAlgorithm = new RijndaelManaged();
                    _symmetricAlgorithm.Mode = CipherMode.CBC;
                    _symmetricAlgorithm.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9, 0x5, 0x46, 0x9c, 0xea, 0xa8, 0x4b, 0x73, 0xcc };

                    _cryptProvider = CryptProvider.Rijndael;
                    break;
                case CryptProvider.RC2:
                    _symmetricAlgorithm = new RC2CryptoServiceProvider();
                    _symmetricAlgorithm.Mode = CipherMode.CBC;
                    _symmetricAlgorithm.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9 };

                    _cryptProvider = CryptProvider.RC2;
                    break;
                case CryptProvider.DES:
                    _symmetricAlgorithm = new DESCryptoServiceProvider();
                    _symmetricAlgorithm.Mode = CipherMode.CBC;
                    _symmetricAlgorithm.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9 };

                    _cryptProvider = CryptProvider.DES;
                    break;
                case CryptProvider.TripleDES:
                    _symmetricAlgorithm = new TripleDESCryptoServiceProvider();
                    _symmetricAlgorithm.Mode = CipherMode.CBC;
                    _symmetricAlgorithm.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9 };

                    _cryptProvider = CryptProvider.TripleDES;
                    break;
            }
            _password = password;

            string salt = string.Empty;
            if (_symmetricAlgorithm.LegalKeySizes.Length > 0)
            {
                int keySize = _password.Length * 8;
                int minSize = _symmetricAlgorithm.LegalKeySizes[0].MinSize;
                int maxSize = _symmetricAlgorithm.LegalKeySizes[0].MaxSize;
                int skipSize = _symmetricAlgorithm.LegalKeySizes[0].SkipSize;

                if (keySize > maxSize) _password = _password.Substring(0, maxSize / 8);
                else if (keySize < maxSize)
                {
                    int validSize = (keySize <= minSize) ? minSize : (keySize - keySize % skipSize) + skipSize;
                    if (keySize < validSize)
                        _password = _password.PadRight(validSize / 8, '*');
                }
            }
            PasswordDeriveBytes key = new PasswordDeriveBytes(_password, ASCIIEncoding.ASCII.GetBytes(salt));
            byte[] keyByte = key.GetBytes(_password.Length);
            _symmetricAlgorithm.Key = keyByte;
        }
        #endregion

        #region métodos
        public virtual string Encrypt(string texto)
        {
            try
            {
                byte[] plainByte = Encoding.UTF8.GetBytes(texto);
                ICryptoTransform cryptoTransform = _symmetricAlgorithm.CreateEncryptor();
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write);

                cryptoStream.Write(plainByte, 0, plainByte.Length);
                cryptoStream.FlushFinalBlock();

                byte[] cryptoByte = memoryStream.ToArray();

                return Convert.ToBase64String(cryptoByte, 0, cryptoByte.GetLength(0));
            }
            catch (Exception ex)
            {
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public virtual string Decrypt(string texto)
        {
            try
            {
                byte[] cryptoByte = Convert.FromBase64String(texto);
                ICryptoTransform cryptoTransform = _symmetricAlgorithm.CreateDecryptor();
                MemoryStream memoryStream = new MemoryStream(cryptoByte, 0, cryptoByte.Length);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read);

                StreamReader streamReader = new StreamReader(cryptoStream);
                return streamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw new CustomException(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        #endregion
    }
}
