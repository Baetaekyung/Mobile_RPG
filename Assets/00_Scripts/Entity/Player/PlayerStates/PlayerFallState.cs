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

        _moveController.OnJump += HandleChangeStateToJump;
    }

    public override void ExitState()
    {
        base.ExitState();

        _moveController.OnJump -= HandleChangeStateToJump;
    }

    private void HandleChangeStateToJump() => _stateMachine.ChangeState(EPlayerStateEnum.JUMP);
}
