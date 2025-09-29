using System;
using UnityEngine;

public class Fevor : MonoBehaviour, IAttack
{
    private FevorScroptibleObject _fevorScroptibleObject;

    private TowerAttack _towerAttack;
    private TowerStats _towerStats;

    private EnemyStats _lastEnemy;
    private int _currentCount;

    void Start()
    {
        _towerAttack = GetComponent<TowerAttack>();
        _towerStats = GetComponent<TowerStats>();
        _towerAttack.IAttackSubs.Add(this);

        _currentCount = 0;

        try
        {
            _fevorScroptibleObject = (FevorScroptibleObject)_towerAttack.Tower.firstSkill;
        }
        catch (InvalidCastException)
        {
            _fevorScroptibleObject = (FevorScroptibleObject)_towerAttack.Tower.secondSkill;
        }

    }

    public void Attack()
    {
        if (_towerAttack._targetStats == _lastEnemy)
        {
            _towerStats.ChangeAttackSpeed(_fevorScroptibleObject.AttackSpeedBuff);
            _currentCount++;
        }
        else
        {
            _towerStats.ChangeAttackSpeed(-_fevorScroptibleObject.AttackSpeedBuff * _currentCount);
            _currentCount = 0;
        }
        _lastEnemy = _towerAttack._targetStats;
        GameObject ball = _towerAttack.CreateBall(_towerAttack.Tower.FireBallPrebuf);
        GetComponent<ExpireanceTower>().Attack(ball);
        ball.GetComponent<AttackBall>().Create(damage: _towerAttack.CalculateDamage(_towerStats.CurrentDamage), targerStats: _towerAttack._targetStats, towerAttack: _towerAttack);
    }

    private void OnDestroy()
    {
        _towerAttack.IAttackSubs.Remove(this);
    }
}
