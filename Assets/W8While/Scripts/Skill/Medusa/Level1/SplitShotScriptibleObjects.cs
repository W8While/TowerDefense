using UnityEngine;

[CreateAssetMenu(fileName = "New SplitSot", menuName = "Skill/PassiveSkill/Medusa/SplitShot")]
public class SplitShotScriptibleObjects : PassiveSkill
{
    [SerializeField] private int _goalsAmount;
    public int GoalsAmount => _goalsAmount;

    public override string SkillFullDescription()
    {
        return $"{GoalsAmount} целей";
    }
}
