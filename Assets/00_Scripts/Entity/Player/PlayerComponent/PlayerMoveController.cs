using System;
using UnityEngine;

public class PlayerMoveController : EntityMoveController, IEntityCompoInit
{
    private const float AIR_MOVE_INTERPOLATION = 3.5f;

    public event Action OnJumpEvent;

    [SerializeField] private InputActionDataSO jumpAction;
    [SerializeField] private JumpDataSO        jumpData;

    private Player _player;
    private int    _currentJumpCount = 0;

    public Rigidbody2D  GetRbComponent => _rbCompo;
    public JumpDataSO   GetJumpData    => jumpData;

    public int CurrentJumpCount
    {
        get =>_currentJumpCount;
        set => _currentJumpCount = value; 
    }

    public override void Initialize(Entity entity)
    {
        base.Initialize(entity);

        _player = entity as Player;

        _rbCompo = _player.GetComponent<Rigidbody2D>();

        jumpAction = jumpAction.GetRuntimeSO();
        jumpAction.OnPressEvent += Jump;
    }

    private void Start()
    {
        InputManager.Inst.GetInputButton().SetButtonAction(jumpAction);
    }

    public override void Jump()
    {
        OnJumpEvent?.Invoke();
    }

    public override void StopImmediately()
    {
        _rbCompo.linearVelocity = Vector2.zero;
    }

    public override void MoveEntityXDirection(int xDirection, bool isAir = false)
    {
        if(!isAir)
            base.MoveEntityXDirection(xDirection);
        else if (isAir)
            _rbCompo.AddForceX(xDirection * airMoveSpeed * AIR_MOVE_INTERPOLATION, ForceMode2D.Force);
    }

    private void OnDestroy()
    {
        jumpAction.OnPressEvent -= Jump;
    }

    public void InitJumpCount()
    {
        _currentJumpCount = 0;
    }
}
