using UnityEngine;

public class PlayerGroundChecker : EntityGroundChecker
    , IEntityCompoInit
    , IEntityCompoAfterInit
{
    private PlayerMoveController _moveController;
    private Rigidbody2D          _rbCompo;
    private Player               _player;

    public bool IsPlayerFalling => _canCast;

    public void Initialize(Entity entity)
    {
        _player = entity as Player;

        _moveController = _player.GetEntityCompo<PlayerMoveController>();
    }

    public void AfterInitialize(Entity entity)
    {
        _rbCompo = _moveController.GetRbComponent;
    }

    private void Start()
    {
        OnGroundHit += _moveController.InitJumpCount;
    }

    private void Update()
    {
        _canCast = _rbCompo.linearVelocityY <= 0f;
    }

    private void OnDestroy()
    {
        OnGroundHit -= _moveController.InitJumpCount;
    }

    protected override void GroundCheckCasting()
    {
        base.GroundCheckCasting();
    }
}
