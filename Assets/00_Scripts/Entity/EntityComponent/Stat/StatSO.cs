using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public delegate void StatChangeEventHandler();

public class StatSO : MonoBehaviour
{
    public event StatChangeEventHandler OnBaseStatValueChanged;
    public event StatChangeEventHandler OnAdditionalStatValueChanged;
    public event StatChangeEventHandler OnStatMultiplierChanged;

    [SerializeField] private EStatType statType;
    [SerializeField] private int       baseStat;
    [SerializeField] private int       additionalStat;
    [SerializeField] private int       statMultiplier;

    private readonly Dictionary<StatBuffDataSO, List<int>> _additiveAmountCollection = new();
    private readonly Dictionary<StatBuffDataSO, int>       _additiveLayerCollection  = new();

    public EStatType GetStatType       => statType;
    public StatSO    GetRuntimeStat    => Instantiate(this);
    public int       GetBaseStat       => baseStat;
    public int       GetAdditionalStat => additionalStat;

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

        OnAdditionalStatValueChanged?.Invoke();
    }

    public void DecreaseStatMultiplier(int decreaseAmount)
    {
        statMultiplier -= decreaseAmount;

        OnAdditionalStatValueChanged?.Invoke();
    }

    #endregion

    #region Stat Buff 관련

    public void StatBuff(StatBuffDataSO additiveStatData)
    {
        Debug.Assert(additiveStatData.GetStatType == statType,
            $"맞지 않는 스텟의 버프를 넣었다. 스텟 타입: {statType.ToString()}");

        // 현재 버프가 존재
        if (_additiveLayerCollection.TryGetValue(additiveStatData, out var layerAmount))
        {
            // 현재 버프가 최대 중첩 상태이다.
            if (additiveStatData.GetMaxLayerCount == layerAmount)
                AddMaxLayerStatBuff(additiveStatData);
            // 최대 중첩 아니면 중첩 상태 올리고 버프 추가하기
            else
                AddStatBuff(additiveStatData);
        }
        // 처음 적용되는 버프라면 그냥 버프 레이어 1 추가하고 버프 량 추가하기
        else
            InitStatBuff(additiveStatData);
    }

    public void RemoveStatBuff(StatBuffDataSO additiveStatData)
    {
        Debug.Assert(additiveStatData.GetStatType == statType, $"맞지 않는 스텟의 버프를 넣었다. 스텟 타입: {statType.ToString()}");

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
                Debug.Log($"존재하지 않는 버프 목록, 버프 이름: {additiveStatData.GetBuffName}");
                return;
            }

            _additiveAmountCollection[additiveStatData].RemoveAt(matchAmountIndex);
            _additiveLayerCollection[additiveStatData]--;
        }
        else
        {
            Debug.Log($"존재하지 않는 버프 목록, 버프 이름: {additiveStatData.GetBuffName}");
        }
    }

    private void InitStatBuff(StatBuffDataSO additiveStatData)
    {
        Debug.Assert(additiveStatData.GetStatType == statType, $"맞지 않는 스텟의 버프를 넣었다. 스텟 타입: {statType.ToString()}");

        _additiveLayerCollection.Add(additiveStatData, 1);

        _additiveAmountCollection.Add(additiveStatData, new List<int>() { additiveStatData.GetIncreaseAmount });
    }

    private void AddStatBuff(StatBuffDataSO additiveStatData)
    {
        Debug.Assert(additiveStatData.GetStatType == statType, $"맞지 않는 스텟의 버프를 넣었다. 스텟 타입: {statType.ToString()}");

        _additiveLayerCollection[additiveStatData]++;

        _additiveAmountCollection[additiveStatData].Add(additiveStatData.GetIncreaseAmount);
    }

    private void AddMaxLayerStatBuff(StatBuffDataSO additiveStatData)
    {
        Debug.Assert(additiveStatData.GetStatType == statType,
            $"맞지 않는 스텟의 버프를 넣었다. 스텟 타입: {statType.ToString()}");

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

        //만약 올리려는 버프의 총량이 현재 오른 버프들 중 가장 작은 양보다 작으면 리턴
        if (minIncreaseAmount >= additiveStatData.GetIncreaseAmount)
            return;
        //만약 올리려는 버프의 총량이 현재 오른 버프 중 가장 작은 양보다 크면, 가장 작은 양 빼고 새로 넣어주기
        else
        {
            _additiveAmountCollection[additiveStatData].RemoveAt(minAmountLayer);

            _additiveAmountCollection[additiveStatData].Add(additiveStatData.GetIncreaseAmount);
        }
    }

    private void EffectBuffToStat()
    {
        int index;

        foreach (var additiveBuffData in _additiveAmountCollection)
        {
            StatBuffDataSO statBuffData   = additiveBuffData.Key;
            List<int>      statBuffAmount = additiveBuffData.Value;

            EStatBuffType  statBuffType   = statBuffData.GetStatBuffType;

            for(index = 0; index < statBuffAmount.Count; index++)
                BuffAtMatchStatType(index, statBuffData, statBuffAmount, statBuffType);
        }
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
                baseStat += statBuffAmount[index];
                break;
            case EStatBuffType.ADDITIONAL_STAT_BUFF:
                additionalStat += statBuffAmount[index];
                break;
            case EStatBuffType.STAT_MULTIPLIER_BUFF:
                statMultiplier += statBuffAmount[index];
                break;
            default:
                Debug.Log($"StatBuffDataSO에 StatBuffType이 설정 되지 않았습니다. {statBuffData.GetBuffName}");
                break;
        }
    }



    #endregion
}