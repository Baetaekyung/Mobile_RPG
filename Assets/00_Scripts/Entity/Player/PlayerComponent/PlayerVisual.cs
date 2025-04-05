using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : EntityVisual, IEntityCompoInit
{
    [SerializeField] private List<PlayerAnimParamSO> animParams;

    private Dictionary<EPlayerStateEnum, PlayerAnimParamSO> _animParams = new();

    public void Initialize(Entity entity)
    {
        CollectAnimParams();
    }

    private void CollectAnimParams()
    {
        //foreach (var parameter in animParams)
        //    _animParams.Add(parameter.GetStateEnum, parameter);
    }

    public void SetAnimation(EPlayerStateEnum state)
    {
        //var playerAnimParam = GetPlayerAnimParam(state);

        //if(playerAnimParam != null)
        //{
        //    ClearCurrentAnimState();

        //    if (playerAnimParam.GetIsBoolAnimation)
        //        GetAnimator.SetBool(playerAnimParam.GetAnimHash, true);
        //    else
        //        GetAnimator.SetTrigger(playerAnimParam.GetAnimHash);
        //}
    }

    private void ClearCurrentAnimState()
    {
        //foreach(var playerAnimParam in animParams)
        //{
        //    if (playerAnimParam.GetIsBoolAnimation)
        //        GetAnimator.SetBool(playerAnimParam.GetAnimHash, false);
        //}
    }

    public PlayerAnimParamSO GetPlayerAnimParam(EPlayerStateEnum stateEnum)
    {
        //if(_animParams.TryGetValue(stateEnum, out var playerAnimParam))
        //{
        //    return playerAnimParam;
        //}

        //Debug.LogWarning
        //    ($"Player state의 Animation parameter so데이터가 존재하지 않음. state name: {stateEnum.ToString()}");
        return null;
    }
}
