using UnityEngine;

[CreateAssetMenu(fileName = "New ArctickBurn", menuName = "Skill/PassiveSkill/WinterWiverna/ArctickBurn")]
public class ArctickBurnScriptibleObject : PassiveSkill
{
    [SerializeField] private float _procenteDamageBuff;
    public float ProcenteDamageBuff => _procenteDamageBuff;

    public override string SkillFullDescription()
    {
        return $"”рон - {ProcenteDamageBuff}% от макс хп врага";
    }
}
