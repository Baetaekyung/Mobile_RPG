using System;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour, IEntityCompoInit
{
    public event Action OnAttack;

    [SerializeField] private InputActionDataSO attackAction;

    private Player _player;

    public void Initialize(Entity entity)
    {
        _player = entity as Player;

        InputManager.Inst.GetInputButton().SetButtonAction(attackAction);
    }

    
}
