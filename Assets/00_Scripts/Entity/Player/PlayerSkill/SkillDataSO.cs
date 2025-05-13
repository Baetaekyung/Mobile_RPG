using System;
using UnityEngine;

public enum SkillType
{
    NONE = 0,
    ACTIVE,
    PASSIVE,
    TRIGGER,
    BUFF,
    ALL = 99
}

[Flags]
public enum SkillElement
{
    NONE = 1 << 0,
    FIRE = 1 << 1,
    ICE  = 1 << 2,
    HOLY = 1 << 3,
    DARK = 1 << 4,

    ALL = FIRE | ICE | HOLY | DARK
}

[CreateAssetMenu(fileName = "SkillDataSO", menuName = "SO/Skill/SkillDataSO")]
public class SkillDataSO : ScriptableObject
{
    [Header("스킬 설명 변수들")]
    [SerializeField] private Sprite skillIcon;
    [SerializeField] private int    skillID;
    [SerializeField] private string skillName;
    [SerializeField] private string displayName;
    [SerializeField, TextArea] private string skillDescription;

    [Header("스킬 사용 데이터")]
    [SerializeField] private SkillType skillType;
    [SerializeField] private int skillLevel;
    [SerializeField] private int canUseLevel;
    [SerializeField] private int levelScaling;
    [SerializeField] private int damagePercent;
    [SerializeField] private float cooldown;
    [SerializeField] private float castTime;
    [SerializeField] private int manaCost;
    [SerializeField] private float range;
    [SerializeField] private int hitCount;

    [SerializeField] private GameObjectPoolDataSO skillEffect;
}
