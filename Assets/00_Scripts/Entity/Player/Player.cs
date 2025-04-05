using System;
using UnityEngine;

public partial class Player : Entity
{
    private PlayerMoveController _entityMoveController;

    protected override void Awake()
    {
        base.Awake();

        InitializeStateMachine();

        _entityMoveController = GetEntityCompo<PlayerMoveController>();
    }

    private void Update()
    {
        _stateMachine.UpdateStateMachine();

        if (Input.GetKeyDown(KeyCode.Space))
            _entityMoveController.Jump();
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdateMachine();
    }
}
