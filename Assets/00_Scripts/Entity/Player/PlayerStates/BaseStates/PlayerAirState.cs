public class PlayerAirState : PlayerState, IBasePlayerState
{
    public PlayerAirState(Player player, PlayerStateMachine stateMachine, EPlayerStateEnum state)
        : base(player, stateMachine, state)
    {
    }

    public void EnterBaseState()
    {
        //hmm...
    }

    public void ExitBaseState()
    {
        //hmm...
    }
}
