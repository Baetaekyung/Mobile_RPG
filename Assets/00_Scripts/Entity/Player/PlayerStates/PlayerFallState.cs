using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    private const bool ISAIR = true;

    public PlayerFallState(Player player, PlayerStateMachine stateMachine, EPlayerStateEnum state)
        : base(player, stateMachine, state)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _moveController.OnJumpEvent += HandleChangeStateToJump;
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        MovePlayerOnFall();
    }

    public override void ExitState()
    {
        base.ExitState();

        _moveController.OnJumpEvent -= HandleChangeStateToJump;
    }

    private void MovePlayerOnFall()
    {
        _moveController.MoveEntityXDirection(Mathf.RoundToInt(InputManager.Inst.Direction.x), ISAIR);
    }

    private void HandleChangeStateToJump()
    {
        _stateMachine.ChangeState(EPlayerStateEnum.JUMP);
    }
}
