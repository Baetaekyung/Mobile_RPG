using UnityEngine;

public class PlayerAnimEventHandler : EntityAnimEventHandler
{
    [Tooltip("������ �ٽ� �����ϰ� ����� Ʈ����")]
    public event OnAnimationEventTrigger OnAttackDelayEnd;

    public void OnAttackDelayEndTrigger() => OnAttackDelayEnd?.Invoke();
}
