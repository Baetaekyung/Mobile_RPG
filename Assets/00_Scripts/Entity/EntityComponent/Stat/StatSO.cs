using System.Collections.Generic;
using UnityEngine;

public delegate void StatChangeEventHandler();

[CreateAssetMenu(menuName = "SO/Stat/Stat")]
public class StatSO : ScriptableObject
{
    #region ���� ��ȭ �̺�Ʈ��

    public event StatChangeEventHandler OnBaseStatValueChanged;
    public event StatChangeEventHandler OnAdditionalStatValueChanged;
    public event StatChangeEventHandler OnStatMultiplierValueChanged;

    #endregion

    #region ���� ���� ������ (���� Ÿ��, ���� ���, �߰� ����, �⺻ ����, �ۼ�Ʈ ����)

    [SerializeField] private EStatType statType;
    [SerializeField] private float     statMultiplier = 1;
    [SerializeField] private int       baseStat;
    [SerializeField] private int       additionalStat;
    [SerializeField] private bool      isPercent = false;

    #endregion

    #region ���� ������ ����� ������ �ִ� ������

    private float _buffedStatMultiplier;
    private int   _buffedBaseStat;
    private int   _buffedAdditionalStat;

    #endregion

    #region ���� ������ ���

    private readonly Dictionary<StatBuffDataSO, List<int>> _additiveAmountCollection = new();
    private readonly Dictionary<StatBuffDataSO, int>       _additiveLayerCollection  = new();

    #endregion

    #region ������Ƽ��

    public EStatType StatType       { get => statType; set => statType = value; }
    public int       BaseStat       { get => baseStat; set => baseStat = value; }
    public int       AdditionalStat { get => additionalStat; set => additionalStat = value; }
    public float     StatMultiplier { get => statMultiplier; set => statMultiplier = value; }
    public bool      IsPercent      { get => isPercent; set => isPercent = value; }

    #endregion

    public StatSO GetRuntimeStat => Instantiate(this);

    #region [BaseStat] Increase, Decrease

    public void IncreaseBaseStat(int increaseAmount)
    {
        baseStat += increaseAmount;

        OnBaseStatValueChanged?.Invoke();
    }

    public void DecreaseBaseStat(int decreaseAmount)
    {
        baseStat -= decreaseAmount;

        OnBaseStatValueChanged?.Invoke();
    }

    #endregion

    #region [AdditionalStat] Increase, Decrease

    public void IncreaseAdditionalStat(int increaseAmount)
    {
        additionalStat += increaseAmount;

        OnAdditionalStatValueChanged?.Invoke();
    }

    public void DecreaseAdditionalStat(int decreaseAmount)
    {
        additionalStat -= decreaseAmount;

        OnAdditionalStatValueChanged?.Invoke();
    }

    #endregion

    #region [StatMultiplier] Increase, Decrease

    public void IncreaseStatMultiplier(int increaseAmount)
    {
        statMultiplier += increaseAmount;

        OnStatMultiplierValueChanged?.Invoke();
    }

    public void DecreaseStatMultiplier(float decreaseAmount)
    {
        statMultiplier -= decreaseAmount;

        OnStatMultiplierValueChanged?.Invoke();
    }

    #endregion

    #region Stat Buff ����

    public void BuffStat(StatBuffDataSO additiveStatData)
    {
        Debug.Assert(additiveStatData.GetStatType == statType,
            $"���� �ʴ� ������ ������ �־���. ���� Ÿ��: {statType.ToString()}");

        // ���� ������ ����
        if (_additiveLayerCollection.TryGetValue(additiveStatData, out var layerAmount))
        {
            // ���� ������ �ִ� ��ø �����̴�.
            if (additiveStatData.GetMaxLayerCount == layerAmount)
                AddMaxLayerStatBuff(additiveStatData);
            // �ִ� ��ø �ƴϸ� ��ø ���� �ø��� ���� �߰��ϱ�
            else
                AddStatBuff(additiveStatData);
        }
        // ó�� ����Ǵ� ������� �׳� ���� ���̾� 1 �߰��ϰ� ���� �� �߰��ϱ�
        else
            InitStatBuff(additiveStatData);

        EffectBuffToStat();
    }

    public void RemoveStatBuff(StatBuffDataSO additiveStatData)
    {
        Debug.Assert(additiveStatData.GetStatType == statType, $"���� �ʴ� ������ ������ �־���. ���� Ÿ��: {statType.ToString()}");

        if (_additiveAmountCollection.TryGetValue(additiveStatData, out var amountList))
        {
            int matchAmountIndex = 0;
            bool hasMatchBuff = false;

            for(int i = 0; i < amountList.Count; i++)
            {
                if (amountList[i] == additiveStatData.GetIncreaseAmount)
                {
                    matchAmountIndex = i;
                    hasMatchBuff = true;

                    break;
                }
            }

            if(hasMatchBuff == false)
            {
                Debug.Log($"�������� �ʴ� ���� ���, ���� �̸�: {additiveStatData.GetBuffName}");
                return;
            }

            _additiveAmountCollection[additiveStatData].RemoveAt(matchAmountIndex);
            _additiveLayerCollection[additiveStatData]--;
        }
        else
        {
            Debug.Log($"�������� �ʴ� ���� ���, ���� �̸�: {additiveStatData.GetBuffName}");
        }
    }

    private void InitStatBuff(StatBuffDataSO additiveStatData)
    {
        Debug.Assert(additiveStatData.GetStatType == statType, $"���� �ʴ� ������ ������ �־���. ���� Ÿ��: {statType.ToString()}");

        _additiveLayerCollection.Add(additiveStatData, 1);

        _additiveAmountCollection.Add(additiveStatData, new List<int>() { additiveStatData.GetIncreaseAmount });
    }

    private void AddStatBuff(StatBuffDataSO additiveStatData)
    {
        Debug.Assert(additiveStatData.GetStatType == statType, $"���� �ʴ� ������ ������ �־���. ���� Ÿ��: {statType.ToString()}");

        _additiveLayerCollection[additiveStatData]++;

        _additiveAmountCollection[additiveStatData].Add(additiveStatData.GetIncreaseAmount);
    }

    private void AddMaxLayerStatBuff(StatBuffDataSO additiveStatData)
    {
        Debug.Assert(additiveStatData.GetStatType == statType,
            $"���� �ʴ� ������ ������ �־���. ���� Ÿ��: {statType.ToString()}");

        int minIncreaseAmount = int.MaxValue;
        int minAmountLayer = 0;
        int currentLayer = 0;

        for (; currentLayer < additiveStatData.GetMaxLayerCount; currentLayer++)
        {
            if (_additiveAmountCollection[additiveStatData][currentLayer] < minIncreaseAmount)
            {
                minIncreaseAmount = _additiveAmountCollection[additiveStatData][currentLayer];
                minAmountLayer = currentLayer;
            }
        }

        //���� �ø����� ������ �ѷ��� ���� ���� ������ �� ���� ���� �纸�� ������ ����
        if (minIncreaseAmount >= additiveStatData.GetIncreaseAmount)
            return;
        //���� �ø����� ������ �ѷ��� ���� ���� ���� �� ���� ���� �纸�� ũ��, ���� ���� �� ���� ���� �־��ֱ�
        else
        {
            _additiveAmountCollection[additiveStatData].RemoveAt(minAmountLayer);

            _additiveAmountCollection[additiveStatData].Add(additiveStatData.GetIncreaseAmount);
        }
    }

    private void EffectBuffToStat()
    {
        int index;

        DecreaseBaseStat(_buffedBaseStat);
        DecreaseAdditionalStat(_buffedAdditionalStat);
        DecreaseStatMultiplier(_buffedStatMultiplier);

        _buffedBaseStat = 0;
        _buffedAdditionalStat = 0;
        _buffedStatMultiplier = 0f;

        foreach (var additiveBuffData in _additiveAmountCollection)
        {
            StatBuffDataSO statBuffData   = additiveBuffData.Key;
            EStatBuffType  statBuffType   = statBuffData.GetStatBuffType;
            List<int>      statBuffAmount = additiveBuffData.Value;

            for(index = 0; index < statBuffAmount.Count; index++)
                BuffAtMatchStatType(index, statBuffData, statBuffAmount, statBuffType);
        }

        Debug.Log("���� �����");
    }

    private void BuffAtMatchStatType(
        int             index, 
        StatBuffDataSO  statBuffData, 
        List<int>       statBuffAmount, 
        EStatBuffType   statBuffType)
    {
        switch (statBuffType)
        {
            case EStatBuffType.BASE_STAT_BUFF:
                IncreaseBaseStat(statBuffAmount[index]);
                _buffedBaseStat += statBuffAmount[index];
                break;

            case EStatBuffType.ADDITIONAL_STAT_BUFF:
                IncreaseAdditionalStat(statBuffAmount[index]);
                _buffedAdditionalStat += statBuffAmount[index];
                break;

            case EStatBuffType.STAT_MULTIPLIER_BUFF:
                IncreaseStatMultiplier(statBuffAmount[index]);
                _buffedStatMultiplier += statBuffAmount[index];
                break;

            default:
                Debug.Log($"StatBuffDataSO�� StatBuffType�� ���� ���� �ʾҽ��ϴ�. {statBuffData.GetBuffName}");
                break;
        }
    }

    #endregion

    public int GetValue()
    {
        int statAmount = Mathf.RoundToInt((BaseStat + AdditionalStat) * statMultiplier);

        if (IsPercent)
            return Mathf.RoundToInt(statAmount / 100f);

        return statAmount;
    }
}