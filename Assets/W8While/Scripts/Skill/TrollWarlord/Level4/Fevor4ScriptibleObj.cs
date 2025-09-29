using UnityEngine;

[CreateAssetMenu(fileName = "Fevor4", menuName = "Skill/ActiveSkill/TrollWarlord/Fevor4")]
public class Fevor4ScriptibleObj : FevorScroptibleObject
{
    [SerializeField] private float _stopTimeDuraction;
    public float stopTimeDuraction => _stopTimeDuraction;
    [SerializeField] private float _stopTimeCoolDawn;
    public float StopTimeCoolDawn => _stopTimeCoolDawn;

    public override string SkillFullDescription()
    {
        return $"Атак спид бафф за каждый стак - {AttackSpeedBuff}\n Останавливает время на {stopTimeDuraction} каждые {StopTimeCoolDawn} сек(с)";
    }
}
