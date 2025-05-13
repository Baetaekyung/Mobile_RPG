using UnityEngine;

public class PlayerCombatController : MonoBehaviour, IEntityCompoInit
{
    [Header("입력 행동 SO들")]
    [SerializeField] private InputActionDataSO attackAction;

    [Header("전투 시간 관련")]
    [SerializeField] private float battleTime;
    private float _battleTimer; // 추후 배틀 시간 동안 뭐 안되게 할 때 사용

    //private Queue<> _skillDatas;

    private Player _player;
    private bool   _canAttack = true; // 플레이어의 공격이 종료 되었거나 중간에 공격 가능하도록 만들어야할때

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
