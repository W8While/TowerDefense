using System;
using System.Collections.Generic;
using UnityEngine;

public class BashOfTheDeep4 : MonoBehaviour, IAttackBallAttackEnemy, IAttack
{
    private BashOfTheDeep4ScriptibleObj _bashOfTheDeep4ScriptibleObject;

    private TowerAttack _towerAttack;
    private TowerStats _towerStats;
    private List<AttackBall> _ballsToBash = new List<AttackBall>();
    private int _currenntCountAttack;
    void Start()
    {
        _towerAttack = GetComponent<TowerAttack>();
        _towerStats = GetComponent<TowerStats>();
        _towerAttack.IAttackBallAttackEnemySubs.Add(this);
        _towerAttack.IAttackSubs.Add(this);
        _currenntCountAttack = 0;

        try
        {
            _bashOfTheDeep4ScriptibleObject = (BashOfTheDeep4ScriptibleObj)_towerAttack.Tower.firstSkill;
        }
        catch (InvalidCastException)
        {
            _bashOfTheDeep4ScriptibleObject = (BashOfTheDeep4ScriptibleObj)_towerAttack.Tower.secondSkill;
        }
    }

    public void AttackBallAttackEnemy(AttackBall ball, EnemyStats enemy)
    {
        foreach (AttackBall bashBall in _ballsToBash)
            if (bashBall == ball)
            {
                foreach (EnemyStats enemys in EnemySpawner.AllPlaceEnemy)
                {
                    enemys.GetComponent<EnemyMove>().StopMove(_bashOfTheDeep4ScriptibleObject.AnotherBushDuraction, false);
                }
                enemy.GetComponent<EnemyMove>().StopMove(_bashOfTheDeep4ScriptibleObject.BashDuraction);
                break;
            }
        _ballsToBash.Remove(ball);
    }

    public void Attack()
    {
        _currenntCountAttack++;
        if (_currenntCountAttack == _bashOfTheDeep4ScriptibleObject.CountAttackToBash)
        {
            GameObject a = _towerAttack.CreateBall(_towerAttack.Tower.FireBallPrebuf);
            GetComponent<ExpireanceTower>().Attack(a);
            a.GetComponent<AttackBall>().Create(damage: _towerAttack.CalculateDamage(_towerStats.CurrentDamage + _bashOfTheDeep4ScriptibleObject.AdditionalDamage), targerStats: _towerAttack._targetStats, towerAttack: _towerAttack);
            _ballsToBash.Add(a.GetComponent<AttackBall>());
            _currenntCountAttack = 0;
            return;
        }
        GameObject ball = _towerAttack.CreateBall(_towerAttack.Tower.FireBallPrebuf);
        GetComponent<ExpireanceTower>().Attack(ball);
        ball.GetComponent<AttackBall>().Create(damage: _towerAttack.CalculateDamage(_towerStats.CurrentDamage), targerStats: _towerAttack._targetStats, towerAttack: _towerAttack);
    }





    private void OnDestroy()
    {
        _towerAttack.IAttackBallAttackEnemySubs.Remove(this);
        _towerAttack.IAttackSubs.Remove(this);
    }
}
