using System;
using System.IO;
using System.Security.Cryptography;

namespace ProgramSelector
{
    class stringEncryption
    {
        public static string Encrypt(string plainText, string password)
        {
            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            byte[] saltBytes = GetRandomBytes(8);
            byte[] encryptedBytes;

            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(password, saltBytes);
                aes.Key = rfc2898.GetBytes(32);
                aes.IV = rfc2898.GetBytes(16);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    }

                    encryptedBytes = memoryStream.ToArray();
                }
            }

            byte[] combinedBytes = new byte[saltBytes.Length + encryptedBytes.Length];
            Buffer.BlockCopy(saltBytes, 0, combinedBytes, 0, saltBytes.Length);
            Buffer.BlockCopy(encryptedBytes, 0, combinedBytes, saltBytes.Length, encryptedBytes.Length);

            return Convert.ToBase64String(combinedBytes);
        }

        public static string Decrypt(string encryptedText, string password)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] saltBytes = new byte[8];
            byte[] plainTextBytes;

            Buffer.BlockCopy(encryptedBytes, 0, saltBytes, 0, saltBytes.Length);

            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(password, saltBytes);
                aes.Key = rfc2898.GetBytes(32);
                aes.IV = rfc2898.GetBytes(16);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(encryptedBytes, saltBytes.Length, encryptedBytes.Length - saltBytes.Length);
                    }

                    plainTextBytes = memoryStream.ToArray();
                }
            }

            return System.Text.Encoding.UTF8.GetString(plainTextBytes);
        }


        static byte[] GetRandomBytes(int length)
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] bytes = new byte[length];
                rng.GetBytes(bytes);
                return bytes;
            }
        }
    }
}