using System;
using UnityEngine.Events;
using UnityEngine.UI;

public static class ButtonExtensions
{
    public static void AddListener(this Button button, UnityAction action)
    {
        button.onClick.AddListener(action);
    }
    public static void AddListener(this Button button, Action action)
    {
        UnityAction uAction = DelegateUtils.ActionToUnityAction(action);

        button.onClick.AddListener(uAction);
    }

    public static void AddHoldListener(this Button button, Action action, float holdTime, float tick)
    {        
        button.gameObject.AddComponent<HoldButton>().Initialize(button, action, holdTime, tick);
    }

    public static void RemoveAllListeners(this Button button)
    {
        if(button.TryGetComponent<HoldButton>(out var holdBtn))
            holdBtn.enabled = false;

        button.onClick.RemoveAllListeners();
    }
}