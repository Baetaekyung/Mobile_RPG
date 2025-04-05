using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "JumpData", menuName = "SO/JumpData")]
public class JumpDataSO : ScriptableObject
{
    public int        JumpCount;
    public GameObject JumpEffectPrefab;
    public float      UpJumpForce;

    //JumpCount만큼 보이도록
    [HideInInspector] public List<float>   JumpForce     = new List<float>();
    [HideInInspector] public List<Vector2> JumpDirection = new List<Vector2>();

    public Vector2 GetJumpDirection(int index) => Vector3.Normalize(JumpDirection[index]);
    public float   GetJumpForce(int index)     => JumpForce[index];
}
