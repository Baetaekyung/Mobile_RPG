using UnityEngine;

public class PlayerGroundChecker : EntityGroundChecker
    , IEntityCompoInit
    , IEntityCompoAfterInit
{
    private Player _player;
    private PlayerMoveController _moveController;
    private Rigidbody2D _rbCompo;

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

    private void Update()
    {
        _canCast = _rbCompo.linearVelocityY <= 0;
    }

    private void Start()
    {
        OnGroundHit += _moveController.InitJumpCount;
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
