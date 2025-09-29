using UnityEngine;

[CreateAssetMenu(fileName = "BashOfTheDeep4", menuName = "Skill/PassiveSkill/Slardar/BashOfTheDeep4")]
public class BashOfTheDeep4ScriptibleObj : BashOfTheDeepScriptibleObject
{
    [SerializeField] private float _anotherBushDuraction;
    public float AnotherBushDuraction => _anotherBushDuraction;

    public override string SkillFullDescription()
    {
        return $"���� ��� ���� - {CountAttackToBash}\n��� ��������� �� ������� - {BashDuraction}+ {AdditionalDamage}��� ����� � ����, �� ���������(�������� ����) {AnotherBushDuraction}";
    }
}
