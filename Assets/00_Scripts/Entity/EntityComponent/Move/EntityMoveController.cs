using System;
using UnityEngine;

public class EntityMoveController : MonoBehaviour, IEntityCompoInit, IEntityCompoAfterInit
{
    protected float groundMoveSpeed;
    protected float airMoveSpeed;

    protected EntityStatComponent _statComponent;
    protected Rigidbody2D _rbCompo;

    public virtual void Initialize(Entity entity)
    {
        _statComponent = entity.GetEntityCompo<EntityStatComponent>();
    }

    public void AfterInitialize(Entity entity)
    {
        groundMoveSpeed = _statComponent.GetStat(EStatType.MOVE_SPEED).GetValue();
        airMoveSpeed = groundMoveSpeed * 0.6f;
    }

    public virtual void MoveEntityXDirection(int xDirection, bool isAir = false)
    {
        float moveSpeed = isAir ? airMoveSpeed : groundMoveSpeed;

        _rbCompo.linearVelocityX = xDirection * moveSpeed;
    }

    public virtual void StopImmediately()
    {
        _rbCompo.linearVelocityX = 0f;
    }

    public virtual void Jump() { } //Entity can jump...?? hmm..
}
