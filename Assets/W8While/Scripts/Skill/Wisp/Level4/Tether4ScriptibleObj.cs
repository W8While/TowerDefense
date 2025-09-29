using UnityEngine;

[CreateAssetMenu(fileName = "New Tether4", menuName = "Skill/ActiveSkill/Wisp/Tether4")]
public class Tether4ScriptibleObj : TetherScripribleObject
{
    [SerializeField] private float _timeBetweenGetBuff;
    public float TimeBetweenGetBuff => _timeBetweenGetBuff;

    public override string SkillFullDescription()
    {
        return $"� ������ ���� ���� {MaxTowersWithBuff} �������. ����� ���� ���� ������ {TimeBetweenGetBuff} ���.\n���� - {DamageBuff} �������, {AttakcSpeedBuff} ���� �����";
    }
}
