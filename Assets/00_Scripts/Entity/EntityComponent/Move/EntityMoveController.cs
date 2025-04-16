using System;
using UnityEngine;

public class EntityMoveController : MonoBehaviour, IEntityCompo
{
    [SerializeField] protected float groundMoveSpeed;
    [SerializeField] protected float airMoveSpeed;
    [SerializeField] protected float jumpForce;

    protected Rigidbody2D _rbCompo;

    public virtual void MoveEntityXDirection(int xDirection, bool isAir = false)
    {
        float moveSpeed = isAir ? airMoveSpeed : groundMoveSpeed;
        _rbCompo.linearVelocityX = xDirection * moveSpeed;
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
