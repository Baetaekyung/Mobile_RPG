using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DesignationDataSO", menuName = "SO/PlayerData/DesignationDataSO")]
public class DesignationDataSO : ScriptableObject
{
    public string designationName;
    public Sprite designationSprite;

    public List<StatBuffDataSO> statBuffData;

    public void EffectDesignation(Player player)
    {
        var statCompo = player.GetEntityCompo<EntityStatComponent>();

        for (int i = 0; i < statBuffData.Count; i++)
        {
            var currentStatBuff = statBuffData[i];

            var currentStat = statCompo.GetStat(currentStatBuff.GetStatType);

            currentStat.BuffStat(currentStatBuff);
            Debug.Log(@$"Stat은 버프 되었다. {currentStatBuff.GetBuffName} 
                        | {currentStatBuff.GetStatBuffType} 
                        | {currentStatBuff.GetStatType} 
                        | {currentStatBuff.GetIncreaseAmount}"); ;
        }
    }

    public void UnEffectDesignation(Player player)
    {
        var statCompo = player.GetEntityCompo<EntityStatComponent>();

        for (int i = 0; i < statBuffData.Count; i++)
        {
            var currentStatBuff = statBuffData[i];

            var currentStat = statCompo.GetStat(currentStatBuff.GetStatType);

            currentStat.RemoveStatBuff(currentStatBuff);
            Debug.Log(@$"Stat은 버프해제 되었다. {currentStatBuff.GetBuffName} 
                        | {currentStatBuff.GetStatBuffType} 
                        | {currentStatBuff.GetStatType} 
                        | {currentStatBuff.GetIncreaseAmount}"); ;
        }
    }
}
