using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InputActionDataSO))]
public class CustomInputActionData : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        InputActionDataSO data = (InputActionDataSO)target;

        if(data.isHoldable == true)
        {
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Hold Datas");
            }

            data.maxHoldTime = EditorGUILayout.FloatField("Max hold Time", data.maxHoldTime);
            data.holdTick = EditorGUILayout.FloatField("Hold tick", data.holdTick);
        }
        else
        {
            data.maxHoldTime = 0f;
            data.holdTick = 0f;
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(data);
        }
    }
}
