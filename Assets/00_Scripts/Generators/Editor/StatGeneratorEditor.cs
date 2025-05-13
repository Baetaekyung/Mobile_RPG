using UnityEditor;
using UnityEngine;

public class StatGeneratorEditor : EditorWindow
{
    private const string SAVE_PATH = "Assets/03_Scriptables/BaseStat/";

    private static StatDatabase _statDB;

    private static EStatType _statType;
    private static int       _baseStat;
    private static bool      _isPercent;

    [MenuItem("Tools/Stat generate window")]
    public static void ShowWindow()
    {
        _statType = EStatType.NONE;
        _baseStat = 0;

        _statDB = Resources.Load<StatDatabase>("Database/StatDatabase/StatDatabase");
        Debug.Log(_statDB);

        var window = GetWindow<StatGeneratorEditor>("Stat generator");
        window.minSize = new Vector2(700, 600);
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("���� ������");
        GUILayout.Space(10);

        _statType  = (EStatType)EditorGUILayout.EnumPopup(_statType);
        _baseStat  = EditorGUILayout.IntField(_baseStat);

        {
            EditorGUILayout.BeginHorizontal();
    
            GUILayout.Label("�ۼ�Ʈ �����ΰ�?");
            _isPercent = EditorGUILayout.Toggle(_isPercent);
    
            EditorGUILayout.EndHorizontal();
        }

        if(GUILayout.Button("���� ����"))
        {
            StatSO stat = CreateInstance<StatSO>();

            string statTypeUnder = _statType.ToString().CapitalizeFirst();
            string statName = $"Stat_{statTypeUnder}";

            stat.StatType  = _statType;
            stat.BaseStat  = _baseStat;
            stat.IsPercent = _isPercent;

            _statDB.AddData(stat);

            AssetDatabase.CreateAsset(stat, SAVE_PATH + $"{statName}.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"Base stat ������, Type: {_statType}");
        }

        EditorGUILayout.Space(10);

        if(GUILayout.Button("������ ����"))
        {
            _statDB.SortByName();
        }
    }
}
