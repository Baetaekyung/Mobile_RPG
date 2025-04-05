using Codice.Client.Common.GameUI;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(JumpDataSO))]
public class JumpDataCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        JumpDataSO jumpData = (JumpDataSO)target;

        EditorGUILayout.LabelField("데이터를 입력하기 전에 한번 누르시오.");
        if (jumpData != null)
        {
            if(GUILayout.Button("데이터 캐시 초기화."))
            {
                jumpData.JumpCount = 0;
                jumpData.JumpEffectPrefab = null;
            }
        }

        if(jumpData.JumpCount == 0)
        {
            ClearCache();
        }

        for (int i = 0; i < jumpData.JumpCount; i++)
        {
            jumpData.JumpForce.Add(0);
            jumpData.JumpDirection.Add(Vector2.zero);
        }

        for (int i = 0; i < jumpData.JumpCount; i++)
        {
            EditorGUILayout.Space(20);

            EditorGUILayout.LabelField("점프 데이터");
            jumpData.JumpForce[i] = EditorGUILayout.FloatField("Jump Force", jumpData.JumpForce[i]);
            jumpData.JumpDirection[i] = EditorGUILayout.Vector2Field("Jump Direction", jumpData.JumpDirection[i]);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(jumpData);
        }

        void ClearCache()
        {
            jumpData.JumpForce.Clear();
            jumpData.JumpDirection.Clear();
        }
    }
}
