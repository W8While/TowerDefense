using UnityEngine;

[CreateAssetMenu(fileName = "New Fevor", menuName = "Skill/PassiveSkill/Troll Warlord/Fevor")]
public class FevorScroptibleObject : PassiveSkill
{
    [SerializeField] private float _attackSpeedBuff;
    public float AttackSpeedBuff => _attackSpeedBuff;

    public override string SkillFullDescription()
    {
        return $"���� ���� ���� �� ������ ���� - {AttackSpeedBuff}";
    }
}
