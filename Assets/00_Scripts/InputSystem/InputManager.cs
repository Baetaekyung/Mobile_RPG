using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class InputManager : MonoSingleton<InputManager>
{
    [HideInInspector] 
    public InputActionDataSO inputActionData;

    [SerializeField] private List<InputButton> inputButtons = new List<InputButton>();

    public int direction;

    protected override void Awake()
    {
        base.Awake();
    }

    public InputButton GetInputButton()
    {
        return inputButtons.FirstOrDefault(button => button.IsEmptyButton());
    }
}
