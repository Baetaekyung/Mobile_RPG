//Player Finate state machine

public partial class Player
{
    private PlayerStateMachine _stateMachine;

    private bool _canStateChange = true;

    public PlayerStateMachine GetStateMachine => _stateMachine;
    public bool GetCanStateChange => _canStateChange;


    private void InitializeStateMachine()
    {
        _stateMachine = new PlayerStateMachine(this);
        _canStateChange = true;

        _stateMachine.AddState(
            EPlayerStateEnum.IDLE,
            new PlayerIdleState(this, _stateMachine, EPlayerStateEnum.IDLE));

        _stateMachine.AddState(
            EPlayerStateEnum.MOVE,
            new PlayerMoveState(this, _stateMachine, EPlayerStateEnum.MOVE));

        _stateMachine.AddState(
            EPlayerStateEnum.JUMP,
            new PlayerJumpState(this, _stateMachine, EPlayerStateEnum.JUMP));

        _stateMachine.AddState(
            EPlayerStateEnum.ATTACK,
            new PlayerAttackState(this, _stateMachine, EPlayerStateEnum.ATTACK));

        _stateMachine.AddState(
            EPlayerStateEnum.FALL,
            new PlayerFallState(this, _stateMachine, EPlayerStateEnum.FALL));

        _stateMachine.SetState(EPlayerStateEnum.IDLE);
    }
}
