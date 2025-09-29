using UnityEngine;

[CreateAssetMenu(fileName = "New BashOfTheDeep", menuName = "Skill/PassiveSkill/Slardar/BashOfTheDeep")]
public class BashOfTheDeepScriptibleObject : PassiveSkill
{
    [SerializeField] private int _countAttackToBash;
    public int CountAttackToBash => _countAttackToBash;
    [SerializeField] private float _bashDuraction;
    public float BashDuraction => _bashDuraction;
    [SerializeField] private float _additionalDamage;
    public float AdditionalDamage => _additionalDamage;

    public override string SkillFullDescription()
    {
        return $"Атак для баша - {CountAttackToBash}\nБаш дюрактион - {BashDuraction} + {AdditionalDamage}Доп урона с баша";
    }
}
