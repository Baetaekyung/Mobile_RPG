using System;
using UnityEngine;

public class PlayerAirState : PlayerState, IBasePlayerState
{
    protected PlayerGroundChecker  _groundChecker;
    protected PlayerMoveController _moveController;

    public PlayerAirState(Player player, PlayerStateMachine stateMachine, EPlayerStateEnum state)
        : base(player, stateMachine, state)
    {
        _groundChecker  = player.GetEntityCompo<PlayerGroundChecker>();
        _moveController = player.GetEntityCompo<PlayerMoveController>();
    }
    public void EnterBaseState()
    {
        _groundChecker.OnGroundHit += HandleStateChangeToIdle;
    }

    public void ExitBaseState()
    {
        _groundChecker.OnGroundHit -= HandleStateChangeToIdle;
    }

    private void HandleStateChangeToIdle() => _stateMachine.ChangeState(EPlayerStateEnum.IDLE);
}
