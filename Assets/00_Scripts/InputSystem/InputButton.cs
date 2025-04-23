using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputButton : MonoBehaviour, 
    IPointerDownHandler, IPointerUpHandler
{
    #region ��ư �̺�Ʈ��

    private event Action OnButtonInputEvent;
    private event Action OnbuttonHoldEvent;

    #endregion

    [Header("Button UI��")]
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private Image           infoIcon;

    [Header("��ư Ȧ�� ���� ������")]
    private Coroutine _holdRoutine = null;

    private InputActionDataSO _inputActionData;

    public bool IsRegisteredButton => _inputActionData != null;

    public void SetButtonAction(InputActionDataSO inputActionData)
    {
        Debug.Assert(inputActionData != null,
            $"Can't register button action cause InputActionDataSO argument is null");

        _inputActionData   = inputActionData;
        OnButtonInputEvent = inputActionData.OnPressEvent;

        if(inputActionData.isHoldable == true)
            OnbuttonHoldEvent = inputActionData.OnHoldEvent;

        SetInfoUI(inputActionData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Assert(_inputActionData != null, 
            $"InputActionData is null, so can't invoke button event");
        
        OnButtonInputEvent?.Invoke();

        if (_inputActionData.isHoldable)
            _holdRoutine = StartCoroutine(nameof(HoldButtonRoutine));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(_holdRoutine != null)
            StopCoroutine(_holdRoutine);
    }

    private IEnumerator HoldButtonRoutine()
    {
        WaitForSeconds holdTick = new WaitForSeconds(_inputActionData.holdTick);

        //while(true)�� ���ǹ� �˻縦 ���ϱ� ���� for ���
        for( ; ; )
        {


            OnbuttonHoldEvent?.Invoke();
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
