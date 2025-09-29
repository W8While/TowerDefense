using UnityEngine;

[CreateAssetMenu(fileName = "New Magnetic Field", menuName = "Skill/ActiveSkill/Arc Warden/MagneticField4")]
public class MagneticField4ScriptibleObj : MagneticFieldScriptibleObjects
{
    [SerializeField] private float _enemySlowProcently;
    public float EnemySlowProcently => _enemySlowProcently;

    public override string SkillFullDescription()
    {
        return $"Радиус поля - {FieldRadius} \nДействие поля - {FieldDuraction} секунд\nАттак спид бонус - {AttackSpeedBonus}\n Замедляет врагов на {EnemySlowProcently}%";
    }
}
