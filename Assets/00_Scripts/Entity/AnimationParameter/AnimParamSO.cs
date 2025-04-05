using UnityEngine;

public abstract class AnimParamSO : ScriptableObject
{
    [SerializeField] private bool isBooleanType;
    [SerializeField] private string AnimationName;
    [SerializeField] private int animHash;

    private void OnValidate()
    {
        animHash = Animator.StringToHash(AnimationName);
    }

    public bool GetIsBoolAnimation => isBooleanType;
    public int GetAnimHash => animHash;
}
