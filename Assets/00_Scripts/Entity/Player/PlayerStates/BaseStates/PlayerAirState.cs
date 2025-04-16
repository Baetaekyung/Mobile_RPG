using System;
using UnityEngine;

public class PlayerAirState : PlayerState, IBasePlayerState
{
    private PlayerGroundChecker  _groundChecker;
    private PlayerMoveController _moveController;

    public PlayerAirState(Player player, PlayerStateMachine stateMachine, EPlayerStateEnum state)
        : base(player, stateMachine, state)
    {
        _groundChecker  = player.GetEntityCompo<PlayerGroundChecker>();
        _moveController = player.GetEntityCompo<PlayerMoveController>();
    }
    public void EnterBaseState()
    {
        OnBaseStateUpdate += HandleMovePlayerOnAir;

        _groundChecker.OnGroundHit += HandleStateChangeToIdle;
    }

    public void ExitBaseState()
    {
        OnBaseStateUpdate -= HandleMovePlayerOnAir;

        _groundChecker.OnGroundHit -= HandleStateChangeToIdle;
    }

    private void HandleMovePlayerOnAir()
    {
        _moveController.MoveEntityXDirection(Mathf.RoundToInt(InputManager.Inst.Direction.x));
    }

    private void HandleStateChangeToIdle() => _stateMachine.ChangeState(EPlayerStateEnum.IDLE);
}
