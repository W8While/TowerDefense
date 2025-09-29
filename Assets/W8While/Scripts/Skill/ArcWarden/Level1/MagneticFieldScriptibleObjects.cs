using UnityEngine;

[CreateAssetMenu(fileName = "New Magnetic Field", menuName = "Skill/ActiveSkill/Arc Warden/MagneticField")]
public class MagneticFieldScriptibleObjects : ActiveSkill
{
    [SerializeField] private float _fieldRadius;
    public float FieldRadius => _fieldRadius;
    [SerializeField] private float _fieldDuraction;
    public float FieldDuraction => _fieldDuraction;
    [SerializeField] private float _attackSpeedBonus;
    public float AttackSpeedBonus => _attackSpeedBonus;
    [SerializeField] private GameObject _fieldObject;
    public GameObject FieldObject => _fieldObject;

    public override string SkillFullDescription()
    {
        return $"Радиус поля - {FieldRadius} \nДействие поля - {FieldDuraction} секунд\nАттак спид бонус - {AttackSpeedBonus}";
    }
}
