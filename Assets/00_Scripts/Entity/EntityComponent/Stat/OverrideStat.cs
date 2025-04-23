using System;
using UnityEngine;

[Serializable]
public class OverrideStat
{
    [field: SerializeField] public bool IsOverride;

    [SerializeField] private StatSO baseStat;
    [SerializeField] private int overrideValue;

    public StatSO GetOverrideStat()
    {
        StatSO stat = baseStat.GetRuntimeStat;

        stat.BaseStat = overrideValue;

        return stat;
    }
}
