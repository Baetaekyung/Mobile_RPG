using UnityEditor;
using UnityEngine;

public class StatGeneratorEditor : EditorWindow
{
    private const string SAVE_PATH = "Assets/03_Scriptables/BaseStat/";

    private static EStatType _statType;
    private static int       _baseStat;
    private static bool      _isPercent;

    [MenuItem("Tools/Stat generate window")]
    public static void ShowWindow()
    {
        _statType = EStatType.NONE;
        _baseStat = 0;

        GetWindow<StatGeneratorEditor>("Stat generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("½ºÅÝ »ý¼º±â");

        _statType  = (EStatType)EditorGUILayout.EnumPopup(_statType);
        _baseStat  = EditorGUILayout.IntField(_baseStat);

        {
            EditorGUILayout.BeginHorizontal();
    
            GUILayout.Label("¹éºÐÀ² ½ºÅÝÀÎ°¡?");
            _isPercent = EditorGUILayout.Toggle(_isPercent);
    
            EditorGUILayout.EndHorizontal();
        }

        if(GUILayout.Button("½ºÅÝ »ý¼º"))
        {
            StatSO stat = CreateInstance<StatSO>();

            string statTypeUnder = _statType.ToString().CapitalizeFirst();
            string statName = $"Stat_{statTypeUnder}";

            stat.StatType  = _statType;
            stat.BaseStat  = _baseStat;
            stat.IsPercent = _isPercent;

            AssetDatabase.CreateAsset(stat, SAVE_PATH + $"{statName}.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"Base stat »ý¼ºµÊ, Type: {_statType}");
        }
    }
}
