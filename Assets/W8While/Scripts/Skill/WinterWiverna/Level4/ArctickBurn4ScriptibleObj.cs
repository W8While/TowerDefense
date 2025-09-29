using UnityEngine;

[CreateAssetMenu(fileName = "ArctickBurn4", menuName = "Skill/PassiveSkill/WinterWiverna/ArctickBurn4")]
public class ArctickBurn4ScriptibleObj : ArctickBurnScriptibleObject
{
    [SerializeField] private float _underEnemyProcentlyHealth;
    public float UnderEnemyProcentlyHealth => _underEnemyProcentlyHealth;

    public override string SkillFullDescription()
    {
        return $"���� - {ProcenteDamageBuff}% �� ���� �� �����\n ������ ����� ��������� �� ����� �� {UnderEnemyProcentlyHealth}% + ������ ������";
    }
}
