using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WoundClinic_WPF.Data
{
    /// <summary>
    /// این کلاس برای رمزنگاری و رمزگشایی متن با استفاده از AES-128 طراحی شده است.
    /// </summary>

    public static class AesEncryption
    {
        private static readonly string Key = "my16charsecretkey"; // باید 16 کاراکتر باشد برای AES-128

        public static string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(Key);
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();
            ms.Write(aes.IV, 0, aes.IV.Length); // ذخیره IV در ابتدای فایل

            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }

            return Convert.ToBase64String(ms.ToArray());
        }

        public static string Decrypt(string cipherText)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(Key);

            byte[] iv = new byte[16];
            Array.Copy(fullCipher, iv, iv.Length);

            using var decryptor = aes.CreateDecryptor(aes.Key, iv);
            using var ms = new MemoryStream(fullCipher, iv.Length, fullCipher.Length - iv.Length);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
    }

}

