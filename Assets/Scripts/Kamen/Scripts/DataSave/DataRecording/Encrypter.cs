using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Kamen.DataSave
{
    #region Enums

    public enum EncryptionType
    {
        None,
        Aes
    }

    #endregion

    public static class Encrypter
    {
        #region Aes

        public static void AesEncrypt(string jsonData, FileStream stream, string key, string iv)
        {
            using Aes aesProvider = Aes.Create();
            aesProvider.Key = Convert.FromBase64String(key);
            aesProvider.IV = Convert.FromBase64String(iv);

            using ICryptoTransform cryptoTransform = aesProvider.CreateEncryptor();
            using CryptoStream cryptoStream = new CryptoStream(stream, cryptoTransform, CryptoStreamMode.Write);

            cryptoStream.Write(Encoding.ASCII.GetBytes(jsonData));
        }
        public static string AesDecrypt(string path, string key, string iv)
        {
            byte[] fileBytes = File.ReadAllBytes(path);
            using Aes aesProvider = Aes.Create();
            aesProvider.Key = Convert.FromBase64String(key);
            aesProvider.IV = Convert.FromBase64String(iv);

            using ICryptoTransform cryptoTransform = aesProvider.CreateDecryptor(aesProvider.Key, aesProvider.IV);
            using MemoryStream decryptorStream = new MemoryStream(fileBytes);
            using CryptoStream cryptoStream = new CryptoStream(decryptorStream, cryptoTransform, CryptoStreamMode.Read);
            using StreamReader reader = new StreamReader(cryptoStream);
            
            return reader.ReadToEnd();
        }
        public static void GenerateAesKeyAndIV(ref string key, ref string iv)
        {
            using Aes aesProvider = Aes.Create();
            key = Convert.ToBase64String(aesProvider.Key);
            iv = Convert.ToBase64String(aesProvider.IV);
        }

        #endregion

        #region Async Aes

        public static async Task AesEncryptAsync(string jsonData, FileStream stream, string key, string iv)
        {
            using Aes aesProvider = Aes.Create();
            aesProvider.Key = Convert.FromBase64String(key);
            aesProvider.IV = Convert.FromBase64String(iv);

            using ICryptoTransform cryptoTransform = aesProvider.CreateEncryptor();
            using CryptoStream cryptoStream = new CryptoStream(stream, cryptoTransform, CryptoStreamMode.Write);

            byte[] dataToEncrypt = Encoding.ASCII.GetBytes(jsonData);
            await cryptoStream.WriteAsync(dataToEncrypt, 0, dataToEncrypt.Length);
        }

        public static async Task<string> AesDecryptAsync(string path, string key, string iv)
        {
            using Aes aesProvider = Aes.Create();
            aesProvider.Key = Convert.FromBase64String(key);
            aesProvider.IV = Convert.FromBase64String(iv);

            using ICryptoTransform cryptoTransform = aesProvider.CreateDecryptor(aesProvider.Key, aesProvider.IV);
            using FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            using CryptoStream cryptoStream = new CryptoStream(stream, cryptoTransform, CryptoStreamMode.Read);
            using StreamReader reader = new StreamReader(cryptoStream);

            return await reader.ReadToEndAsync();
        }

        #endregion

    }
}