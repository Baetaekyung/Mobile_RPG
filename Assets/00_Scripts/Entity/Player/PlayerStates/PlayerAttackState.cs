using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private PlayerAnimEventHandler _playerAnimEventHandler;
    private PlayerCombatController _playerCombatController;

    private float _timer;

    private bool _isHandlerInvoked = false;

    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, EPlayerStateEnum state)
        : base(player, stateMachine, state)
    {
        _playerAnimEventHandler = player.GetEntityCompo<PlayerAnimEventHandler>();
        _playerCombatController = player.GetEntityCompo<PlayerCombatController>();
    }

    public override void EnterState()
    {
        base.EnterState();

        _isHandlerInvoked = false;

        _timer    = 2f; // todo: 나중에 skillData로 skillDuration 받아서 사용

        //_playerCombatController.InitBattleTimer();
        _playerCombatController.SetCanAttack(false);

        _playerAnimEventHandler.OnAnimationEnd += HandleCanAttack;
        _playerAnimEventHandler.OnAnimationEnd += HandleStateChangeToIdle;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _timer -= Time.deltaTime;

        if (_timer <= 0f)
            _stateMachine.ChangeState(EPlayerStateEnum.IDLE);
    }

    public override void ExitState()
    {
        if (!_isHandlerInvoked)
            HandleCanAttack();

        _playerAnimEventHandler.OnAnimationEnd -= HandleCanAttack;
        _playerAnimEventHandler.OnAnimationEnd -= HandleStateChangeToIdle;

        base.ExitState();
    }

    private void HandleCanAttack() 
    { 
        _playerCombatController.SetCanAttack(true);

        _isHandlerInvoked = true;
    }
    private void HandleStateChangeToIdle() => _stateMachine.ChangeState(EPlayerStateEnum.IDLE);
}
