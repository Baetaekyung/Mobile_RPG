using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum EInputDirection
    {
        NONE = 0,
        RIGHT,
        LEFT,
        UP,
        DOWN,
        ALL = 99
    }

    [SerializeField] private EInputDirection direction;
    private Vector2 _direction;

    public void OnPointerDown(PointerEventData eventData)
    {
        _direction = direction switch
        {
            EInputDirection.NONE => Vector2.zero,
            EInputDirection.LEFT => Vector2.left,
            EInputDirection.RIGHT => Vector2.right,
            EInputDirection.UP => Vector2.up,
            EInputDirection.DOWN => Vector2.down,
            _ => throw new ArgumentException("MoveButton의 Direction이 잘 못 설정 되어있음!")
        };

        Debug.Log($"direction X: {_direction.x}, direction Y: {_direction.y}");
        InputManager.Inst.Direction += new Vector2(_direction.x, _direction.y);

        if(Mathf.Approximately(_direction.x, 0f) == false)
            InputManager.Inst.LastInputDirectionOnlyX = Mathf.RoundToInt(_direction.x);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputManager.Inst.Direction -= new Vector2(_direction.x, _direction.y);
    }
}
