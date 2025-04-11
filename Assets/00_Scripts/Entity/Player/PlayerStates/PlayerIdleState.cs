using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine,EPlayerStateEnum state) 
        : base(player, stateMachine, state) { }

    public override void EnterState()
    {
        base.EnterState();

        _player.GetEntityCompo<PlayerMoveController>().StopImmediately();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if(Mathf.Approximately(InputManager.Inst.Direction.x, 0f) == false)
        {
            _stateMachine.ChangeState(EPlayerStateEnum.MOVE);
        }
    }
}
