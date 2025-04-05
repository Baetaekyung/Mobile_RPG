using System;
using UnityEngine;

public class EntityMoveController : MonoBehaviour, IEntityCompo
{
    [SerializeField] protected float groundMoveSpeed;
    [SerializeField] protected float airMoveSpeed;
    [SerializeField] protected float jumpForce;

    protected Rigidbody2D _rbCompo;

    public virtual void MoveEntityXDirection(int xDirection)
    {
        _rbCompo.linearVelocityX = xDirection * groundMoveSpeed;
    }

    public virtual void StopImmediately()
    {
        _rbCompo.linearVelocityX = 0f;
    }

    public virtual void Jump()
    {
        _rbCompo.AddForceY(jumpForce, ForceMode2D.Impulse);
    }
}
