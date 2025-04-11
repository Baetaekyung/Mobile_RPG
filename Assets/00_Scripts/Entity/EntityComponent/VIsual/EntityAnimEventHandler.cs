using UnityEngine;

public delegate void OnAnimationEventTrigger();

public class EntityAnimEventHandler : MonoBehaviour, IEntityCompo
{
    public event OnAnimationEventTrigger OnAnimationStart;
    public event OnAnimationEventTrigger OnAnimationEvent;
    public event OnAnimationEventTrigger OnAnimationEnd;

    public void OnAnimationStartTrigger() => OnAnimationStart?.Invoke();
    public void OnAnimationEventTrigger() => OnAnimationEvent?.Invoke();
    public void OnAnimationEndTrigger()   => OnAnimationEnd?.Invoke();
}
