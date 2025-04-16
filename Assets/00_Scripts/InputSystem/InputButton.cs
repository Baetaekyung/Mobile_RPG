using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InputButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private Image           infoIcon;

    private Button _button;
    private InputActionDataSO _inputActionData;

    private InputManager inputManaget;

    private void Awake()
    {
        _button = GetComponent<Button>();

        inputManaget = InputManager.Inst;
    }

    public void SetButtonAction(InputActionDataSO inputActionData)
    {
        if(inputActionData == null)
        {
            Debug.LogWarning($"Button can't set cause argument is null");
            return;
        }

        if(_inputActionData != null)
            _button.RemoveAllListeners();

        _inputActionData = inputActionData;

        _button.AddListener(inputActionData.OnPressEvent);

        if(inputActionData.isHoldable == true)
        {
            _button.AddHoldListener(
                inputActionData.OnHoldEvent, 
                inputActionData.maxHoldTime, 
                inputActionData.holdTick);
        }

        SetInfoUI(inputActionData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(inputManaget.InputActionData)
        {
            SetButtonAction(inputManaget.InputActionData);
            inputManaget.InputActionData = null;
        }
    }

    #region SetUI

    private void SetInfoUI(InputActionDataSO inputActionData)
    {
        Sprite sprite = inputActionData.actionIcon;

        if (sprite)
            SetButtonInfo(sprite);
        else
            SetButtonInfo(inputActionData.actionName);
    }

    private void SetButtonInfo(string buttonInfoText)
    {
        infoText.text = buttonInfoText;
        infoIcon.color = Color.clear;
    }

    private void SetButtonInfo(Sprite buttonInfoIcon)
    {
        infoText.text   = string.Empty;
        infoIcon.color  = Color.white;
        infoIcon.sprite = buttonInfoIcon;
    }

    #endregion

    public bool IsEmptyButton() => _inputActionData == null;
}
