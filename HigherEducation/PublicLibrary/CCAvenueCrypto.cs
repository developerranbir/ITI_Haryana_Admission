using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace HigherEducation.PublicLibrary
{
    public class CCAvenueCrypto
    {
        public string Encrypt(string plainText, string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 16));
            byte[] ivBytes = keyBytes;
            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = ivBytes;
                ICryptoTransform encryptor = aes.CreateEncryptor();
                byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] encrypted = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
                return Convert.ToBase64String(encrypted);
            }
        }

        public string Decrypt(string cipherText, string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 16));
            byte[] ivBytes = keyBytes;
            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = ivBytes;
                ICryptoTransform decryptor = aes.CreateDecryptor();
                byte[] inputBytes = Convert.FromBase64String(cipherText);
                byte[] decrypted = decryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
                return Encoding.UTF8.GetString(decrypted);
            }
        }
    }
}