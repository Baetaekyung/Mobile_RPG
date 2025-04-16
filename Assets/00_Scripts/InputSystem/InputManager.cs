using UnityEngine;
using System.Collections.Generic;

public class InputManager : MonoSingleton<InputManager>
{
    public enum EMainButtonType
    {
        NONE,
        JUMP,
        INTERACT
    }

    [SerializeField] private List<InputButton> inputButtons = new();
    [SerializeField] private InputButton mainButton;

    public Vector2 Direction { get; set; }

    /// <summary>
    /// 마지막으로 바라본 방향, X축만 필요
    /// </summary>
    public int     LastInputDirectionOnlyX;

    public InputActionDataSO InputActionData { get; set; }

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
#if UNITY_EDITOR

        EditorTestCode();
#endif
    }

    public void ChangeMainButtonAction(InputActionDataSO inputAction)
    {
        mainButton.SetButtonAction(inputAction);
    }

    public InputButton GetInputButton(int index = 0)
    {
        if (inputButtons.Count <= index)
            return null;

        return inputButtons[index];
    }

    #region Editor Testing Code

    private void EditorTestCode()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Direction -= new Vector2(1, 0);
            LastInputDirectionOnlyX = -1;
        }
        else if (Input.GetKeyUp(KeyCode.A))
            Direction += new Vector2(1, 0);

        if (Input.GetKeyDown(KeyCode.D))
        {
            Direction += new Vector2(1, 0);
            LastInputDirectionOnlyX = 1;
        }
        else if (Input.GetKeyUp(KeyCode.D))
            Direction -= new Vector2(1, 0);

        if (Input.GetKeyDown(KeyCode.W))
            Direction += new Vector2(0, 1);
        else if (Input.GetKeyUp(KeyCode.W))
            Direction -= new Vector2(0, 1);

        if (Input.GetKeyDown(KeyCode.S))
            Direction -= new Vector2(0, 1);
        else if (Input.GetKeyUp(KeyCode.S))
            Direction += new Vector2(0, 1);
    }

    #endregion
}
