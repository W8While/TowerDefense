using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenuAttribute(fileName = "New Tower", menuName = "CreateTower")]
public class TowerObjects : ScriptableObject
{
    public const float BASEATTACKTIME = 170f;
    [SerializeField] private GameObject _prebuf;
    public GameObject Prebuf => _prebuf;
    [SerializeField] private GameObject _previerPrebuf;
    public GameObject PrevierPrebuf => _previerPrebuf;
    [SerializeField] private Sprite _sprite;
    public Sprite Sprite => _sprite;
    [SerializeField] private GameObject _fireBallPrefub;
    public GameObject FireBallPrebuf => _fireBallPrefub;
    [SerializeField] private float _damage;
    public float Damage => _damage;
    [SerializeField] private float _attackRange;
    public float AttackRange => _attackRange;
    [SerializeField] private float _attackSpeed;
    public float AttackSpeed => _attackSpeed;
    [SerializeField] private int _cost;
    public int Cost => _cost;
    private int _newCost;
    public int NewCost => _newCost;
    private float _upCost;
    public float UpCost => _upCost;
    [SerializeField] private string _name;
    public Skill firstSkill;
    public Skill secondSkill;
    [SerializeField] private float _levelUpDamageBuff;
    public float LevelUpDamageBuff => _levelUpDamageBuff;
    [SerializeField] private float _levelUpAttackSpeedBuff;
    public float LevelUpAttackSpeedBuff => _levelUpAttackSpeedBuff;
    private void OnEnable()
    {
        _upCost = 1.05f;
        _newCost = _cost;
    }

    public void ChangeCost(int newCost)
    {
        _newCost = newCost;
    }
    public string GetDescription()
    {
        return $"{_name} \nDamage - {_damage} \nAttackSpeed - {_attackSpeed} \nAttackRange - {_attackRange} \nLevelUpDamageBuff - {_levelUpDamageBuff} \nLevelUpAttackSpeedBuff - {_levelUpAttackSpeedBuff} ";
    }
}
