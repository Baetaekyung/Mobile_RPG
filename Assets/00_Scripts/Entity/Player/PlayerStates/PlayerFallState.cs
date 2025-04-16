using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    private PlayerMoveController _moveController;
    public PlayerFallState(Player player, PlayerStateMachine stateMachine, EPlayerStateEnum state)
        : base(player, stateMachine, state)
    {
        _moveController = player.GetEntityCompo<PlayerMoveController>();
    }

    public override void EnterState()
    {
        base.EnterState();

        _moveController.OnJumpEvent += HandleChangeStateToJump;
    }

    public override void ExitState()
    {
        base.ExitState();

        _moveController.OnJumpEvent -= HandleChangeStateToJump;
    }

    private void HandleChangeStateToJump() => _stateMachine.ChangeState(EPlayerStateEnum.JUMP);
}
