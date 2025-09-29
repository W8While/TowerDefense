using UnityEngine;

[CreateAssetMenu(fileName = "New Magnetic Field", menuName = "Skill/ActiveSkill/Arc Warden/MagneticField4")]
public class MagneticField4ScriptibleObj : MagneticFieldScriptibleObjects
{
    [SerializeField] private float _enemySlowProcently;
    public float EnemySlowProcently => _enemySlowProcently;

    public override string SkillFullDescription()
    {
        return $"������ ���� - {FieldRadius} \n�������� ���� - {FieldDuraction} ������\n����� ���� ����� - {AttackSpeedBonus}\n ��������� ������ �� {EnemySlowProcently}%";
    }
}
