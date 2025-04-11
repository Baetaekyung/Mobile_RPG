using UnityEngine;

/// <summary>
/// 올라가는 value랑 최대로 중첩할 수 있는 카운트 가지고 있는 SO
/// </summary>
[CreateAssetMenu(fileName = "new AdditiveData", menuName = "SO/Stat/AdditiveStatData")]
public class StatBuffDataSO : ScriptableObject
{
    [SerializeField] private EStatBuffType  statBuffType;
    [SerializeField] private EStatType      statType;
    [SerializeField] private string         statBuffName;
    [SerializeField] private int            maxLayerCount = 1;
    [SerializeField] private int            increaseAmount;

    public EStatBuffType GetStatBuffType   => statBuffType;
    public EStatType     GetStatType       => statType;
    public string        GetBuffName       => statBuffName;
    public int           GetMaxLayerCount  => maxLayerCount;
    public int           GetIncreaseAmount => increaseAmount;
}