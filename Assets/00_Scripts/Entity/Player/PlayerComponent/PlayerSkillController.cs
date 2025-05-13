using UnityEngine;

public class PlayerSkillController : MonoBehaviour, IEntityCompoInit
{
    private Player _player;
    private PlayerCombatController _combatController;

    public void Initialize(Entity entity)
    {
        _player = entity as Player;
    }
}
