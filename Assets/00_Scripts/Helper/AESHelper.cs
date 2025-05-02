using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;

public static class AESHelper
{
    private static readonly string key = "12345678901234567890123456789012";
    private static readonly string iv = "0987654321098765";

    public static string Encrypt(string plainText)
    {
        using Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.IV = Encoding.UTF8.GetBytes(iv);

        using MemoryStream ms = new MemoryStream();
        using CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
        using (StreamWriter sw = new StreamWriter(cs)) sw.Write(plainText);

        return Convert.ToBase64String(ms.ToArray());
    }

    public static string Decrypt(string cipherText)
    {
        using Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.IV = Encoding.UTF8.GetBytes(iv);

        using MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText));
        using CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
        using StreamReader sr = new StreamReader(cs);

        return sr.ReadToEnd();
    }
}
