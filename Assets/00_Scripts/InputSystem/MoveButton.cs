using UnityEngine;
using UnityEngine.EventSystems;

public class MoveButton : MonoBehaviour, IPointerEnterHandler, IPointerUpHandler
{
    //Left = -1, Right = 1
    [SerializeField] private int direction;

    private bool _isHolding;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHolding = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isHolding = false;
    }
}
