using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private event Action _onHoldEvent;

    private float _holdDuration;
    private float _eventTick;
    private bool  _isHolding;

    private Coroutine      holdRoutine;
    private WaitForSeconds holdTickWFS;

    public void Initialize(Button button, Action holdAction, float holdDuration, float eventTick)
    {
        _onHoldEvent  = holdAction;
        _holdDuration = holdDuration;
        _eventTick    = eventTick;

        _isHolding    = false;

        holdRoutine = null;
        holdTickWFS = new WaitForSeconds(eventTick);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isHolding = true;

        holdRoutine = StartCoroutine(nameof(HoldRoutine));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isHolding = false;

        if(holdRoutine != null)
        {
            StopCoroutine(nameof(HoldRoutine));
            holdRoutine = null;
        }
    }

    private IEnumerator HoldRoutine()
    {
        float currentHoldTime = 0f;

        while(currentHoldTime < _holdDuration)
        {
            if (_isHolding == false)
                yield break;

            yield return holdTickWFS;

            _onHoldEvent?.Invoke();
            currentHoldTime += _eventTick; //틱 마다 이벤트 발생
        }
    }
}
