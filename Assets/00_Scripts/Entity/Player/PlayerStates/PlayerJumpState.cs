using System;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    private PlayerMoveController _moveController;

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, EPlayerStateEnum state)
        : base(player, stateMachine, state)
    {
        _moveController = player.GetEntityCompo<PlayerMoveController>();
    }

    public override void EnterState()
    {
        base.EnterState();

        _player.GetEntityCompo<PlayerGroundChecker>().OnGroundHit += HandleStateChangeToIdle;
        _moveController.OnJump += HandleStateChangeToJump;

        JumpAction();
    }

    private void JumpAction()
    {
        if (_moveController.CurrentJumpCount < _moveController.GetJumpData.JumpCount)
        {
            //윗 점프
            if (_moveController.CurrentJumpCount == 0 
                && InputManager.Inst.Direction == Vector2.up)
            {
                _moveController.CurrentJumpCount = _moveController.GetJumpData.JumpCount; //Jump 못하게

                _moveController.GetRbComponent.AddForce(
                    Vector2.up * _moveController.GetJumpData.UpJumpForce,
                    ForceMode2D.Impulse);

                return;
            }

            //Jump왜 1단에서 2~3단 바로 진행되는 지 알고 고치기
            Vector2 jumpActionData = _moveController.GetJumpData.GetJumpDirection(_moveController.CurrentJumpCount);

            Vector2 jumpWithDirection = new Vector2(
                jumpActionData.x * InputManager.Inst.LastInputDirectionOnlyX,
                jumpActionData.y).normalized;

            if(_moveController.CurrentJumpCount > 0) //부드럽게 점프 하기 위함
                _moveController.StopImmediately();

            _moveController.GetRbComponent.AddForce(
                jumpWithDirection * _moveController.GetJumpData.GetJumpForce(_moveController.CurrentJumpCount),
                ForceMode2D.Impulse);

            _moveController.CurrentJumpCount++;
        }

        Debug.Log(_moveController.CurrentJumpCount);
    }

    public override void StateExit()
    {
        _player.GetEntityCompo<PlayerGroundChecker>().OnGroundHit -= HandleStateChangeToIdle;
        _moveController.OnJump -= HandleStateChangeToJump;

        base.StateExit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    private void HandleStateChangeToIdle() => _stateMachine.ChangeState(EPlayerStateEnum.IDLE);

    private void HandleStateChangeToJump() => _stateMachine.ChangeState(EPlayerStateEnum.JUMP);
}
