using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, EPlayerStateEnum state)
        : base(player, stateMachine, state)
    {

    }

    public override void EnterState()
    {
        base.EnterState();

        Debug.Log("Attack");
    }

    public override void UpdateState()
    {
        base.UpdateState();

    }

    public override void ExitState()
    {
        base.ExitState();

    }
}
