using UnityEngine;

public static class StringExtention
{
    public static string CapitalizeFirst(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return string.Empty;

        str = str.ToLower();
        str = char.ToUpper(str[0]) + str.Substring(1);

        return str;
    }
}
