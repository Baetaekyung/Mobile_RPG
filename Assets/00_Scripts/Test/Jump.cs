using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private InputActionDataSO actionData;

    private void Start()
    {
        actionData.OnPressEvent += () => Debug.Log("Jump");
        actionData.OnHoldEvent += () => Debug.Log("Hold Jump");

        var button = InputManager.Inst.GetInputButton();
        button.SetButtonAction(actionData);
    }
}
