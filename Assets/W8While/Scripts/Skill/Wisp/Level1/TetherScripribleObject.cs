using UnityEngine;

[CreateAssetMenu(fileName = "New Tether", menuName = "Skill/PassiveSkill/Wisp/Tether")]
public class TetherScripribleObject : PassiveSkill
{
    [SerializeField] private int _maxTowersWithBuff;
    public int MaxTowersWithBuff => _maxTowersWithBuff;
    [SerializeField] private float _attackSpeedBuff;
    public float AttakcSpeedBuff => _attackSpeedBuff;
    [SerializeField] private float _damageBuff;
    public float DamageBuff => _damageBuff;
    [SerializeField] private GameObject _tetherEffect;
    public GameObject TetherEffect => _tetherEffect;

    public override string SkillFullDescription()
    {
        return $"В старте дает бафф {MaxTowersWithBuff} товерам.\nБафф - {DamageBuff} демеджа, {AttakcSpeedBuff} атак спида";
    }
}
