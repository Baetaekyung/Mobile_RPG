using UnityEngine;

public class EntityStatComponent : MonoBehaviour, IEntityCompoInit
{
    [SerializeField] protected OverrideStat[] stats;

    protected StatSO[] _overridedStats;

    public void Initialize(Entity entity)
    {
        int length = stats.Length;

        _overridedStats = new StatSO[length];
        for(int i = 0; i < length; i++)
        {
            _overridedStats[i] = stats[i].GetOverrideStat();
        }
    }
}
