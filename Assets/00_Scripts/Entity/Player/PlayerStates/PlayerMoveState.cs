using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{
    private PlayerMoveController _moveController;

    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, EPlayerStateEnum state) 
        : base(player, stateMachine, state)
    {
        _moveController = player.GetEntityCompo<PlayerMoveController>();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if(Mathf.Approximately(InputManager.Inst.Direction.x, 0f) == true)
        {
            _stateMachine.ChangeState(EPlayerStateEnum.IDLE);
        }
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        _moveController.MoveEntityXDirection(Mathf.RoundToInt(InputManager.Inst.Direction.x));
    }

    public override void StateExit()
    {
        base.StateExit();
    }
}
