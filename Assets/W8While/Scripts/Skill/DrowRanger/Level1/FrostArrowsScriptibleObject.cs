using UnityEngine;

[CreateAssetMenu(fileName = "New FrostArrow", menuName = "Skill/PassiveSkill/Drow Ranger/FrostArrow")]
public class FrostArrowsScriptibleObject : PassiveSkill
{
    [SerializeField] private int _maxArrowStacks;
    public int MaxArrowStacks => _maxArrowStacks;
    [SerializeField] private float _damagePerStack;
    public float DamagePerStack => _damagePerStack;
    [SerializeField] private float _stackDuraction;
    public float StackDuraction => _stackDuraction;

    public override string SkillFullDescription()
    {
        return $"Урон за стак - {DamagePerStack} \nМаксимум стаков - {MaxArrowStacks}\nДлительность стаков (обновляется при попадании) - {StackDuraction} секунд";
    }
}
