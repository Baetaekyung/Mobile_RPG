using System;
using UnityEngine;

public class EntityGroundChecker : MonoBehaviour, IEntityCompo
{
    public event Action OnGroundHit;

    [SerializeField] protected Vector2   groundCheckSize;
    [SerializeField] protected LayerMask LM_whatIsGround;

    protected bool _canCast = true;

    private void FixedUpdate()
    {
        GroundCheckCasting();
    }

    protected virtual void GroundCheckCasting()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, groundCheckSize, 360f,
                    Vector2.down, groundCheckSize.y, LM_whatIsGround);

        if (hit && _canCast)
        {
            OnGroundHit?.Invoke();
            Debug.Log("Ground casted");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(transform.position, groundCheckSize);
    }
}
