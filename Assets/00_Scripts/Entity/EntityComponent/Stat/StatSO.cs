using System;
using System.Collections.Generic;
using UnityEngine;

public delegate void StatChangeEventHandler();

[Serializable]
public class StatSaveData : ISavable
{
    public string StatName;
    public int    baseStat;
    public int    additionalStatValue;
    public int    statMultiplier;

    public string SaveKey => StatName;

    public StatSaveData(string name, int baseValue, int additionalValue, int multiplier)
    {
        StatName = name; 
        baseStat = baseValue;
        additionalStatValue = additionalValue;
        statMultiplier = multiplier;
    }

    public void SaveData() => SaveManager.Save(SaveKey, this);
}

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
    [SerializeField] private int       statMultiplierPercent;
    [SerializeField] private int       baseStat;
    [SerializeField] private int       additionalStat;
    [SerializeField] private bool      isPercent = false;

    private StatSaveData _saveData;

    #endregion

    #region ���� ������ ����� ������ �ִ� ������

    private int   _buffedStatMultiplier = 0;
    private int   _buffedBaseStat       = 0;
    private int   _buffedAdditionalStat = 0;

    #endregion

    #region ���� ������ ���

    private readonly Dictionary<StatBuffDataSO, List<int>> _additiveAmountCollection = new();
    private readonly Dictionary<StatBuffDataSO, int>       _additiveLayerCollection  = new();

    #endregion

    #region ������Ƽ��

    public EStatType StatType       { get => statType; set => statType = value; }
    public int       BaseStat       { get => baseStat; set => baseStat = value; }
    public int       AdditionalStat { get => additionalStat; set => additionalStat = value; }
    public int       StatMultiplier { get => statMultiplierPercent; set => statMultiplierPercent = value; }
    public bool      IsPercent      { get => isPercent; set => isPercent = value; }

    #endregion

    public StatSO GetRuntimeStat() 
    {
        StatSO stat = Instantiate(this);
        stat.Initialize();

        return stat;
    }

    private void Initialize()
    {
        _saveData = new StatSaveData(statType.ToString(), baseStat, additionalStat, StatMultiplier);

        AutoSaveManager.Inst.RegisterAutoSave(_saveData);
    }

    #region [BaseStat] Increase, Decrease

    public void IncreaseBaseStat(int increaseAmount)
    {
        baseStat += increaseAmount;
        _saveData.baseStat = baseStat;

        OnBaseStatValueChanged?.Invoke();
    }

    public void DecreaseBaseStat(int decreaseAmount)
    {
        baseStat -= decreaseAmount;
        _saveData.baseStat = baseStat;

        OnBaseStatValueChanged?.Invoke();
    }

    #endregion

    #region [AdditionalStat] Increase, Decrease

    public void IncreaseAdditionalStat(int increaseAmount)
    {
        additionalStat += increaseAmount;
        _saveData.additionalStatValue = additionalStat;

        OnAdditionalStatValueChanged?.Invoke();
    }

    public void DecreaseAdditionalStat(int decreaseAmount)
    {
        additionalStat -= decreaseAmount;
        _saveData.additionalStatValue = additionalStat;

        OnAdditionalStatValueChanged?.Invoke();
    }

    #endregion

    #region [StatMultiplier] Increase, Decrease

    public void IncreaseStatMultiplierPercent(int increaseAmount)
    {
        statMultiplierPercent += increaseAmount;
        _saveData.statMultiplier = statMultiplierPercent;

        OnStatMultiplierValueChanged?.Invoke();
    }

    public void DecreaseStatMultiplierPercent(int decreaseAmount)
    {
        statMultiplierPercent -= decreaseAmount;
        _saveData.statMultiplier = statMultiplierPercent;

        OnStatMultiplierValueChanged?.Invoke();
    }

    #endregion

    #region Stat Buff ����

    public void BuffStat(StatBuffDataSO additiveStatData)
    {
        Debug.Assert(additiveStatData.GetStatType == statType,
            $"���� �ʴ� ������ ������ �־���. ���� Ÿ��: {statType}");

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
        Debug.Assert(additiveStatData.GetStatType == statType, $"���� �ʴ� ������ ������ �־���. ���� Ÿ��: {statType}");

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

            if (_additiveAmountCollection[additiveStatData].Count == 0)
            {
                _additiveAmountCollection.Remove(additiveStatData);
                _additiveLayerCollection.Remove(additiveStatData);
            }

            EffectBuffToStat();
        }
        else
        {
            Debug.Log($"�������� �ʴ� ���� ���, ���� �̸�: {additiveStatData.GetBuffName}");
        }
    }

    private void InitStatBuff(StatBuffDataSO additiveStatData)
    {
        Debug.Assert(additiveStatData.GetStatType == statType, $"���� �ʴ� ������ ������ �־���. ���� Ÿ��: {statType}");

        _additiveLayerCollection.Add(additiveStatData, 1);

        _additiveAmountCollection.Add(additiveStatData, new List<int>() { additiveStatData.GetIncreaseAmount });
    }

    private void AddStatBuff(StatBuffDataSO additiveStatData)
    {
        Debug.Assert(additiveStatData.GetStatType == statType, $"���� �ʴ� ������ ������ �־���. ���� Ÿ��: {statType}");

        _additiveLayerCollection[additiveStatData]++;

        _additiveAmountCollection[additiveStatData].Add(additiveStatData.GetIncreaseAmount);
    }

    private void AddMaxLayerStatBuff(StatBuffDataSO additiveStatData)
    {
        Debug.Assert(additiveStatData.GetStatType == statType,
            $"���� �ʴ� ������ ������ �־���. ���� Ÿ��: {statType}");

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

        _buffedBaseStat = 0;
        _buffedAdditionalStat = 0;
        _buffedStatMultiplier = 0;

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
                _buffedBaseStat += statBuffAmount[index];
                break;

            case EStatBuffType.ADDITIONAL_STAT_BUFF:
                _buffedAdditionalStat += statBuffAmount[index];
                break;

            case EStatBuffType.STAT_MULTIPLIER_BUFF:
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
        float percentToMultiplier = (statMultiplierPercent + _buffedStatMultiplier) / 100f;

        int defaultValue = (BaseStat + AdditionalStat);
        int additionalMultipliedValue = Mathf.RoundToInt(defaultValue * percentToMultiplier);

        int statAmount = defaultValue + additionalMultipliedValue + _buffedBaseStat + _buffedAdditionalStat;

        if (IsPercent)
            return Mathf.RoundToInt(statAmount / 100f);

        return statAmount;
    }
}