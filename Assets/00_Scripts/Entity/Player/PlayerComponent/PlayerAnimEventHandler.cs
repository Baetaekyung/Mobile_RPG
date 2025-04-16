using UnityEngine;

public class PlayerAnimEventHandler : EntityAnimEventHandler
{
    [Tooltip("공격이 다시 가능하게 만드는 트리거")]
    public event OnAnimationEventTrigger OnAttackDelayEnd;

    public void OnAttackDelayEndTrigger() => OnAttackDelayEnd?.Invoke();
}
