using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class EncryptionUtils
    {


        public static byte[] EncryptWithSymmetryKey(byte[] plainTextBytes, byte[] key, byte[] IV)
        {

            using (Aes symmetricKey = Aes.Create())
            {
                using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(key, IV))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                            cryptoStream.FlushFinalBlock();
                            byte[] cipherTextBytes = memoryStream.ToArray();

                            return cipherTextBytes;
                        }
                    }
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "dummy")]
        internal unsafe static int DecryptInternal(byte[] cipherTextBytes, byte[] key, byte[] IV, byte[] plainData)
        {
            using (Aes symmetricKey = Aes.Create())
            {
                using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(key, IV))
                {
                    using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

                            fixed (byte* dummy = plainTextBytes)
                            {
                                    Array.Copy(plainTextBytes, plainData, decryptedByteCount);
                                    return decryptedByteCount;
                            }
                        }
                    }
                }
            }
        }
    }    
}
