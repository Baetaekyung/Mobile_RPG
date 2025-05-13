using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    private PlayerMoveController _moveController;

    public PlayerIdleState(Player player, PlayerStateMachine stateMachine,EPlayerStateEnum state) 
        : base(player, stateMachine, state)
    {
        _moveController = _player.GetEntityCompo<PlayerMoveController>();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if(!Mathf.Approximately(InputManager.Inst.Direction.x, 0f))
        {
            _stateMachine.ChangeState(EPlayerStateEnum.MOVE);
        }
    }
}
