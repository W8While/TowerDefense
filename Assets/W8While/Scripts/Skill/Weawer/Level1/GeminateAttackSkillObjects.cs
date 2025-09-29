using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/ActiveSkill/Weawer/GeminateAttack")]
public class GeminateAttackSkillObjects : ActiveSkill
{
    [SerializeField] private float _timeSecondAttack;
    public float TimeSecondAttack => _timeSecondAttack;

    public override string SkillFullDescription()
    {
        return $"Даблт тычка каждые {CoolDawn} сек";
    }
}
