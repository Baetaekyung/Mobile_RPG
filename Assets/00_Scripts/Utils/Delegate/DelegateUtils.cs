using System;
using UnityEngine;
using UnityEngine.Events;

public static class DelegateUtils
{
    public static UnityAction ActionToUnityAction(Action action)
    {
        return new UnityAction(action);
    }
}
