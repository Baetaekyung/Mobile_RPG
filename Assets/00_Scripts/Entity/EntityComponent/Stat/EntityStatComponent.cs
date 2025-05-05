using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct DebugStats
{
    public string StatName;
    public int CurrentValue;

    public DebugStats(string name, int value)
    {
        StatName = name;
        CurrentValue = value;
    }
}

public class EntityStatComponent : MonoBehaviour, IEntityCompoInit
{
    [SerializeField] protected OverrideStat[]   defaultStats;
    [SerializeField] protected List<DebugStats> debugStats;

    protected StatSO[] _overridedStats;

    public void Initialize(Entity entity)
    {
        int length = defaultStats.Length;

        _overridedStats = new StatSO[length];
        for (int i = 0; i < length; i++)
        {
            _overridedStats[i] = defaultStats[i].GetOverrideStat();

#if UNITY_EDITOR

            _overridedStats[i].OnAdditionalStatValueChanged += HandleUpdateDebugStats;
            _overridedStats[i].OnBaseStatValueChanged       += HandleUpdateDebugStats;
            _overridedStats[i].OnStatMultiplierValueChanged += HandleUpdateDebugStats;

#endif
        }
    }

    private void HandleUpdateDebugStats()
    {
        int length = _overridedStats.Length;

        debugStats.Clear();
        for (int i = 0; i < length; i++)
        {
            debugStats.Add(
                new DebugStats(
                _overridedStats[i].StatType.ToString(),
                _overridedStats[i].GetValue())
                );
        }
    }

    public StatSO GetStat(EStatType statType)
    {
        StatSO findStat = _overridedStats.FirstOrDefault(stat => stat.StatType == statType);

        Debug.Assert(findStat != null, $"Stat can't find stat name: {statType}");
        return findStat;
    }
}
