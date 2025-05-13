using UnityEngine;

public class PlayerCombatController : MonoBehaviour, IEntityCompoInit
{
    [Header("�Է� �ൿ SO��")]
    [SerializeField] private InputActionDataSO attackAction;

    [Header("���� �ð� ����")]
    [SerializeField] private float battleTime;
    private float _battleTimer; // ���� ��Ʋ �ð� ���� �� �ȵǰ� �� �� ���

    //private Queue<> _skillDatas;

    private Player _player;
    private bool   _canAttack = true; // �÷��̾��� ������ ���� �Ǿ��ų� �߰��� ���� �����ϵ��� �������Ҷ�

    public void Initialize(Entity entity)
    {
        _player = entity as Player;
        _battleTimer = 0f;

        attackAction.OnPressEvent += HandleStateChangeToAttack;
        InputManager.Inst.ChangeMainButtonAction(attackAction);
    }
    private void OnDisable()
    {
        attackAction.OnPressEvent -= HandleStateChangeToAttack;
    }

    public void InitBattleTimer()
    {
        _battleTimer = battleTime;
    }

    private void HandleStateChangeToAttack()
    {
        if (_canAttack == false)
            return;

        _player.GetStateMachine.ChangeState(EPlayerStateEnum.ATTACK);
    }

    public void SetCanAttack(bool value) => _canAttack = value;
}
