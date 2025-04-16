public class PlayerAttackState : PlayerState
{
    private PlayerAnimEventHandler _playerAnimEventHandler;
    private PlayerCombatController _playerCombatController;

    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, EPlayerStateEnum state)
        : base(player, stateMachine, state)
    {
        _playerAnimEventHandler = player.GetEntityCompo<PlayerAnimEventHandler>();
        _playerCombatController = player.GetEntityCompo<PlayerCombatController>();
    }

    public override void EnterState()
    {
        base.EnterState();

        _playerCombatController.SetCanAttack(false);
        _playerCombatController.InitBattleTimer();

        _playerAnimEventHandler.OnAttackDelayEnd += HandleCanAttack;
        _playerAnimEventHandler.OnAnimationEnd   += HandleStateChangeToIdle;
    }

    public override void ExitState()
    {
        _playerAnimEventHandler.OnAttackDelayEnd -= HandleCanAttack;
        _playerAnimEventHandler.OnAnimationEnd   -= HandleStateChangeToIdle;

        base.ExitState();
    }

    private void HandleCanAttack() => _playerCombatController.SetCanAttack(true);

    private void HandleStateChangeToIdle() => _stateMachine.ChangeState(EPlayerStateEnum.IDLE);
}
