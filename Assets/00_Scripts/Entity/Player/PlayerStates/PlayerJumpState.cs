using System;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, EPlayerStateEnum state)
        : base(player, stateMachine, state)
    { }

    public override void EnterState()
    {
        base.EnterState();

        _moveController.OnJumpEvent += HandleStateChangeToJump;

        JumpAction();
    }

    public override void ExitState()
    {
        _moveController.OnJumpEvent -= HandleStateChangeToJump;

        base.ExitState();
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        if (_moveController.GetRbComponent.linearVelocityY < 0)
            _stateMachine.ChangeState(EPlayerStateEnum.FALL);
    }

    private void JumpAction()
    {
        if (_moveController.CurrentJumpCount == _moveController.GetJumpData.JumpCount + 1)
            return;

        //기본 점프
        if(_moveController.CurrentJumpCount == 0)
        {
            Jump(Vector2.up, _moveController.GetJumpData.UpJumpForce);

            return;
        }

        if (_moveController.CurrentJumpCount > 0)
        {
            //윗 점프
            if (_moveController.CurrentJumpCount == 1 && InputManager.Inst.Direction == Vector2.up)
            {
                int maxJumpCnt = _moveController.GetJumpData.JumpCount + 1;
                _moveController.StopImmediately();

                Jump(Vector2.up, _moveController.GetJumpData.UpJumpForce * 2f, true);
                _moveController.CurrentJumpCount = maxJumpCnt; //Jump 못하게

                return;
            }

            {
                //기본 점프 횟수는 제외하고
                int additionalJumpCnt = _moveController.CurrentJumpCount - 1;

                Vector2 jumpActionData = _moveController.GetJumpData.GetJumpDirection(additionalJumpCnt);
                float jumpForce = _moveController.GetJumpData.GetJumpForce(additionalJumpCnt);

                Vector2 jumpWithDirection = new Vector2(
                    jumpActionData.x * InputManager.Inst.LastInputDirectionOnlyX,
                    jumpActionData.y).normalized;

                Jump(jumpWithDirection, jumpForce, true);
            }
        }
    }

    private void Jump(Vector2 direction, float force, bool withStop = false)
    {
        if (withStop)
            _moveController.StopImmediately();

        _moveController.GetRbComponent.AddForce(
                    direction * force,
                    ForceMode2D.Impulse);

        _moveController.CurrentJumpCount++;
    }

    private void HandleStateChangeToJump() => _stateMachine.ChangeState(EPlayerStateEnum.JUMP);
}
