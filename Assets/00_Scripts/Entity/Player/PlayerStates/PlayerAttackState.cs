using System.Threading;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private Animator _animator;

    private PlayerAnimEventHandler _playerAnimEventHandler;
    private PlayerCombatController _playerCombatController;
    private PlayerVisual           _playerVisual;

    private float _timer;

    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, EPlayerStateEnum state)
        : base(player, stateMachine, state)
    {
        _playerVisual = player.GetEntityCompo<PlayerVisual>();
        _playerAnimEventHandler = player.GetEntityCompo<PlayerAnimEventHandler>();
        _playerCombatController = player.GetEntityCompo<PlayerCombatController>();
    }

    public override void EnterState()
    {
        base.EnterState();

        _animator = _playerVisual.GetAnimator;
        _timer    = _animator.GetCurrentAnimatorStateInfo(0).length;

        _playerCombatController.SetCanAttack(false);
        _playerCombatController.InitBattleTimer();

        _playerAnimEventHandler.OnAttackDelayEnd += HandleCanAttack;
        _playerAnimEventHandler.OnAnimationEnd   += HandleStateChangeToIdle;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _timer -= Time.deltaTime;

        if (_timer < 0f)
            _stateMachine.ChangeState(EPlayerStateEnum.IDLE);
    }

    public override void ExitState()
    {
        _timer = 0f;

        HandleCanAttack();
        _playerAnimEventHandler.OnAttackDelayEnd -= HandleCanAttack;
        _playerAnimEventHandler.OnAnimationEnd   -= HandleStateChangeToIdle;

        base.ExitState();
    }

    private void HandleCanAttack() => _playerCombatController.SetCanAttack(true);

    private void HandleStateChangeToIdle() => _stateMachine.ChangeState(EPlayerStateEnum.IDLE);
}
