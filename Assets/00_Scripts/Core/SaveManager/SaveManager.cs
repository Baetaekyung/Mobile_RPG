using System.IO;
using UnityEditor;
using UnityEngine;

public static class SaveManager
{
    #region Json

    private static string GetFilePath(string fileName)
    {
        return Path.Combine(Application.dataPath, fileName + ".sav");
    }

    public static void Save<T>(string fileName, T data, bool useCrypto = false)
    {
        string json = JsonUtility.ToJson(data);
        string encrypted = string.Empty;

        if (useCrypto)
        {
            encrypted = AESHelper.Encrypt(json);
        }

        File.WriteAllText(GetFilePath(fileName), useCrypto ? encrypted : json);
        Debug.Log($"You save the data to {fileName}, data: {data}");
    }

    public static T Load<T>(string fileName, bool useCrypto = false)
    {
        string path = GetFilePath(fileName);
        string cryptoJson = string.Empty;

        if (Exists(fileName) == false)
        {
            Save(fileName, default(T));
            return default;
        }

        string readData = File.ReadAllText(path);

        if(useCrypto)
        {
            cryptoJson = AESHelper.Decrypt(readData);
        }

        return JsonUtility.FromJson<T>(useCrypto ? cryptoJson : readData);
    }

    public static bool Exists(string fileName)
    {
        return File.Exists(GetFilePath(fileName));
    }

    public static void Delete(string fileName)
    {
        string path = GetFilePath(fileName);

        if (File.Exists(path))
            File.Delete(path);
    }

    #endregion

    #region PlayerPrefs

    public static void SetInt(string key, int data) => PlayerPrefs.SetInt(key, data);

    public static int GetInt(string key) => PlayerPrefs.GetInt(key, 0);

    public static void SetFloat(string key, float data) => PlayerPrefs.SetFloat(key, data);

    public static float GetFloat(string key) => PlayerPrefs.GetFloat(key, 0f);

    public static void SetString(string key, string data) => PlayerPrefs.SetString(key, data);

    public static string GetString(string key) => PlayerPrefs.GetString(key, "");

    #endregion
}
