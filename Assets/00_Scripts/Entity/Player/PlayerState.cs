using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerState
{
    private event Action OnBaseStateUpdate;

    protected Player             _player;
    protected PlayerStateMachine _stateMachine;
    protected EPlayerStateEnum   _state;

    public PlayerState(Player player, PlayerStateMachine stateMachine, EPlayerStateEnum state)
    {
        _player       = player;
        _stateMachine = stateMachine;
        _state        = state;
    }

    public virtual void EnterState() 
    {
        _player.GetEntityCompo<PlayerVisual>().SetAnimation(_state);

        #region 이 클래스가 BaseState를 상속받은 클래스 인지 확인
        bool hasBaseState = typeof(IBasePlayerState).IsAssignableFrom(GetType());
        if(hasBaseState)
        {
            (this as IBasePlayerState).EnterBaseState();
            OnBaseStateUpdate += (this as IBasePlayerState).UpdateBaseState;
        }
        #endregion
    }

    public virtual void UpdateState() 
    {
        OnBaseStateUpdate?.Invoke();
    }

    public virtual void StateExit()
    {
        #region Base 상속 확인
        bool hasBaseState = typeof(IBasePlayerState).IsAssignableFrom(GetType());
        if (hasBaseState)
        {
            (this as IBasePlayerState).ExitBaseState();
            OnBaseStateUpdate -= (this as IBasePlayerState).UpdateBaseState;
        }
        #endregion
    }

    public virtual void FixedUpdateState() { }
}
