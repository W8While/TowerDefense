using UnityEngine;

public class TowerStats : MonoBehaviour
{
    private const int MAXATTACKSPEED = 550;

    private TowerAttack _towerAttack;
    private TowerObjects _tower;
    private float _baseDamage;
    public float BaseDamage => _baseDamage;
    private float _currentAttackSpeed;
    public float CurrentAttackSpeed => _currentAttackSpeed;
    private float _currentDamage;
    public float CurrentDamage => _currentDamage;
    private float _currentAttackRange;
    public float CurrentAttackRange => _currentAttackRange;
    //private int _currentCost;
    //public int currentCost => _currentCost;

    private void Start()
    {
        _towerAttack = GetComponent<TowerAttack>();
        _tower = _towerAttack.Tower;


        _currentAttackSpeed = _tower.AttackSpeed;
        _baseDamage = _currentDamage = _tower.Damage;
        _currentAttackRange = _tower.AttackRange;
        //_currentCost = _tower.Cost;
    }

    public void ChangeAttackSpeed(float attackSpeed)
    {
        _currentAttackSpeed += attackSpeed;
        if (_currentAttackSpeed >= MAXATTACKSPEED)
            _currentAttackSpeed = MAXATTACKSPEED;
    }
    public void ChangeDamage(float damage)
    {
        _currentDamage += damage;
    }
    public void ChangeAttackRange(float attackRange)
    {
        _currentAttackRange += attackRange;
    }
    //public void ChangeCost(int cost)
    //{
    //    _currentCost = cost;
    //}


}
