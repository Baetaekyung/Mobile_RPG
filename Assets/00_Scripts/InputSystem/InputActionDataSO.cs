using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InputAction_", menuName = "SO/ActionData")]
public class InputActionDataSO : ScriptableObject
{
    public string actionName;
    public Sprite actionIcon;

    public Action OnPressEvent;
    public Action OnHoldEvent;

    [Space]
    public bool isHoldable = false;

    //Hold data는 holdable == true일 때만 보이게
    [HideInInspector] public float maxHoldTime;
    [HideInInspector] public float holdTick;

    public InputActionDataSO GetRuntimeSO()
    {
        var actionData = Instantiate(this);

        string name = actionName;
        Sprite icon = actionIcon;
        float holdTime = maxHoldTime;
        float tick = holdTick;

        Action OnPress = null;
        Action OnHold = null;

        actionData.maxHoldTime = holdTime;
        actionData.holdTick = holdTick;
        actionData.actionName = name;
        actionData.actionIcon = icon;
        actionData.OnPressEvent = OnPress;
        actionData.OnHoldEvent = OnHold;

        return actionData;
    }
}