using UnityEngine;

public partial class Player : Entity
{
    [SerializeField] private DesignationDataSO defaultDesignation;
    [SerializeField] private string playerName;

    private PlayerMoveController _entityMoveController;
    private PlayerDesignationController _designationController;
    private PlayerNameController _nameController;

    protected override void Awake()
    {   
        base.Awake();

        InitializeStateMachine();

        _entityMoveController  = GetEntityCompo<PlayerMoveController>();
        _designationController = GetEntityCompo<PlayerDesignationController>();
        _nameController = GetEntityCompo<PlayerNameController>();
    }

    private void Start()
    {
        _designationController.SetDesignation(defaultDesignation);
        _nameController.SetName(playerName);
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
