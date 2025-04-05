using UnityEngine;

public class EntityVisual : MonoBehaviour, IEntityCompo
{
    private Animator _animator;

    public Animator GetAnimator => _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}
