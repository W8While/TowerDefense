using UnityEngine;


[CreateAssetMenu(fileName = "Necromastery4", menuName = "Skill/PassiveSkill/ShadowFiend/Necromastery4")]
public class Necromastery4ScriptibleObj : NecromasterySctiptibleObjects
{
    [SerializeField] private int _goalAmount;
    public int goalAmount => _goalAmount;
    [SerializeField] private float _upperDamageX;
    public float UpperDamageX => _upperDamageX;


    public override string SkillFullDescription()
    {
        return $"Демедж за душу - {DamagePerNecromastery} \nПри попадании во врега наносит {goalAmount} врагам демедж равный атаке умноженной на {UpperDamageX}";
    }
}
