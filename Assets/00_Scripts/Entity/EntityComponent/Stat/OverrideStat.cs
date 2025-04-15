using UnityEngine;

public class OverrideStat : MonoBehaviour
{
    [field: SerializeField] public bool IsOverride;

    [SerializeField] private StatSO baseStat;
    [SerializeField] private int overrideValue;

    public StatSO GetOverrideStat()
    {
        StatSO stat = Instantiate(baseStat);

        stat.BaseStat = overrideValue;

        return stat;
    }
}
