public enum EStatType
{
    //공통 스텟
    NONE = 0,
    ATTACK_POWER,       //공격력
    MAGIC_POWER,        //마력
    DAMAGE,             //데미지
    FINAL_ATTACK,       //최종 데미지
    BOSS_DAMAGE,        //보스 데미지
    PROFICIENCY,        //숙련도
    CRITICAL_PRECENT,   //치명타 확률
    CRITICAL_DAMAGE,    //치명타 데미지
    ATTACK_SPEED,       //공격 속도
    MOVE_SPEED,         //이동 속도
    DEFENCE,            //방어력
    DEFENCE_IGNORE,     //방어력 무시
    HEALTH,             //체력
    BUFF_DURATION,      //버프 지속 시간

    //직업 스텟
    STRENGTH,           //전사, 힘
    DEXTERITY,          //궁수, 민첩성
    INTELLIGENCE,       //마법사, 지능
    STEALTH             //도적, 은밀함
}
