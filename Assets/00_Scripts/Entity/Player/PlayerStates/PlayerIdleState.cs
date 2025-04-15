using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    private PlayerMoveController _moveController;

    public PlayerIdleState(Player player, PlayerStateMachine stateMachine,EPlayerStateEnum state) 
        : base(player, stateMachine, state) { }

    public override void EnterState()
    {
        base.EnterState();
        _moveController = _player.GetEntityCompo<PlayerMoveController>();
    }

    public override void ExitState()
    {
        base.ExitState();

    }

    public override void UpdateState()
    {
        base.UpdateState();

        _moveController.StopImmediately();

        if(Mathf.Approximately(InputManager.Inst.Direction.x, 0f) == false)
        {
            _stateMachine.ChangeState(EPlayerStateEnum.MOVE);
        }
    }
}
