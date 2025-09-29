using UnityEngine;


[CreateAssetMenu(fileName = "FrostArrow4", menuName = "Skill/PassiveSkill/Drow Ranger/FrostArrow4")]
public class FrostArrows4ScriptibleObj : FrostArrowsScriptibleObject
{
    [SerializeField] private float _radiusExplosion;
    public float RadiusExplosion => _radiusExplosion;
    [SerializeField] private float _damageProcently;
    public float DamageProcently => _damageProcently;
    [SerializeField] private float _timeBeforeExplosion;
    public float TimeBeforeExplosion => _timeBeforeExplosion;
    [SerializeField] private GameObject _explosionEffect;
    public GameObject ExplosionEffect => _explosionEffect;

    public override string SkillFullDescription()
    {
        return $"���� �� ���� - {DamagePerStack} \n�������� ������ - {MaxArrowStacks}\n������������ ������ (����������� ��� ���������) - {StackDuraction} ������\n ����� ������ {TimeBeforeExplosion} ������ ���� ����������, ������ ������ � ������� {RadiusExplosion} ����, ������ {DamageProcently}% �� ����������� ����� �� ����� � ���������� ������";
    }
}
