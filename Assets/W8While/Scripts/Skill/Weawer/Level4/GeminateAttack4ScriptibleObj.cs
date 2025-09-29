using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/ActiveSkill/Weawer/GeminateAttack4")]
public class GeminateAttack4ScriptibleObj : GeminateAttackSkillObjects
{
    [SerializeField] private GameObject _bug;
    public GameObject Bug => _bug;
    [SerializeField] private float _bugSpeedReduse;
    public float BugSpeedReduse => _bugSpeedReduse;
    [SerializeField] private float _bugDamage;
    public float BugDamage => _bugDamage;
    [SerializeField] private float _bugAttackSpeed;
    public float BugAttackSpeed => _bugAttackSpeed;

    public override string SkillFullDescription()
    {
        return $"Даблт тычка каждые {CoolDawn} сек\n Вешает жучка каждую тычку(стакается), статы которого: Дамаг - {BugDamage}, Атак спид {BugAttackSpeed}, Замедление врагов {BugSpeedReduse}";
    }
}
