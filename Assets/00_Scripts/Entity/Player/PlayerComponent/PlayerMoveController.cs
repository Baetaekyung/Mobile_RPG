using System;
using UnityEngine;

public class PlayerMoveController : EntityMoveController, IEntityCompoInit
{
    public event Action OnJump;

    [SerializeField] private JumpDataSO        jumpData;
    [SerializeField] private InputActionDataSO jumpAction;
    private Player _player;
    private int    _currentJumpCount = 0;

    public JumpDataSO   GetJumpData => jumpData;
    public Rigidbody2D  GetRbComponent => _rbCompo;

    public int CurrentJumpCount
    {
        get =>_currentJumpCount;
        set => _currentJumpCount = value; 
    }

    public void Initialize(Entity entity)
    {
        _player = entity as Player;

        _rbCompo = _player.GetComponent<Rigidbody2D>();

        jumpAction = jumpAction.GetRuntimeSO();
        jumpAction.OnPressEvent += Jump;
    }

    private void Start()
    {
        InputManager.Inst.ChangeMainButtonAction(jumpAction);
    }

    public override void Jump()
    {
        OnJump?.Invoke();
    }

    public override void StopImmediately()
    {
        _rbCompo.linearVelocity = Vector2.zero;
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
