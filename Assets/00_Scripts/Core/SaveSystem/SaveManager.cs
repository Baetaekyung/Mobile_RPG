using System.IO;
using UnityEngine;

public static class SaveManager
{
    #region Json

    private static string GetFilePath(string fileName)
    {
        return Path.Combine(Application.persistentDataPath, fileName + ".sav");
    }

    public static void Save<T>(string fileName, T data, bool useCrypto = false)
    {
        if (data == null)
        {
            Debug.LogWarning($"[{fileName}] 저장할 데이터가 null입니다.");
            return;
        }

        string json = JsonUtility.ToJson(data, true);
        if (useCrypto)
            json = AESHelper.Encrypt(json);

        File.WriteAllText(GetFilePath(fileName), json);
        Debug.Log($"[SaveManager] Saved '{fileName}' (Encrypt: {useCrypto}) -> {json}");
    }

    public static T Load<T>(string fileName, bool useCrypto = false)
    {
        string path = GetFilePath(fileName);

        if (!Exists(fileName))
        {
            File.WriteAllText(path, ""); // 빈 파일 생성
            return default;
        }

        string readData = File.ReadAllText(path);
        if (string.IsNullOrEmpty(readData))
            return default;

        if(useCrypto)
            readData = AESHelper.Decrypt(readData);

        return JsonUtility.FromJson<T>(readData);
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

    public static void SetInt(string key, int data)
    {
        PlayerPrefs.SetInt(key, data);
        PlayerPrefs.Save();
    }

    public static void SetFloat(string key, float data) 
    {
        PlayerPrefs.SetFloat(key, data);
        PlayerPrefs.Save();
    }

    public static void SetString(string key, string data)
    {
        PlayerPrefs.SetString(key, data);
        PlayerPrefs.Save();
    }

    public static int GetInt(string key)        => PlayerPrefs.GetInt(key, 0);
    public static float GetFloat(string key)    => PlayerPrefs.GetFloat(key, 0f);
    public static string GetString(string key)  => PlayerPrefs.GetString(key, "");

    #endregion
}
