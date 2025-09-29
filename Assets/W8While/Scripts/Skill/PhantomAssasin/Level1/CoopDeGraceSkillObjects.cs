using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/PassiveSkill/PhantomAssasin/CoopDeGrace")]
public class CoopDeGraceSkillObjects : PassiveSkill
{
    [Range(1, 100)]
    [SerializeField] private int _critChance;
    public int CritChance => _critChance;
    [SerializeField] private float _critDamage;
    public float CritDamage => _critDamage;

    public override string SkillFullDescription()
    {
        return $"Крит шанс - {CritChance}\nКрит демедж - {CritDamage*100}%";
    }
}
