using System;

public class PlayerAirState : PlayerState, IBasePlayerState
{
    private PlayerGroundChecker _groundChecker;

    public PlayerAirState(Player player, PlayerStateMachine stateMachine, EPlayerStateEnum state)
        : base(player, stateMachine, state)
    {
        _groundChecker = player.GetEntityCompo<PlayerGroundChecker>();
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
