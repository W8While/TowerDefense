using UnityEngine;

[CreateAssetMenu(fileName = "New Necromastery", menuName = "Skill/PassiveSkill/ShadowFiend/Necromastery")]
public class NecromasterySctiptibleObjects : PassiveSkill
{
    [SerializeField] float _damagePerNecromastery;
    public float DamagePerNecromastery => _damagePerNecromastery;

    public override string SkillFullDescription()
    {
        return $"Демедж за душу - {DamagePerNecromastery}";
    }
}
