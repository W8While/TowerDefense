using System;
using UnityEngine;

public class ArctickBurn4 : MonoBehaviour, IAttackBallAttackEnemy, IAttack
{
    private ArctickBurn4ScriptibleObj _arctickBurn4ScriptibleObject;

    private TowerAttack _towerAttack;
    private TowerStats _towerStats;

    private void Start()
    {
        _towerAttack = GetComponent<TowerAttack>();
        _towerStats = GetComponent<TowerStats>();

        try
        {
            _arctickBurn4ScriptibleObject = (ArctickBurn4ScriptibleObj)_towerAttack.Tower.firstSkill;
        }
        catch (InvalidCastException)
        {
            _arctickBurn4ScriptibleObject = (ArctickBurn4ScriptibleObj)_towerAttack.Tower.secondSkill;
        }

        _towerAttack.IAttackBallAttackEnemySubs.Add(this);
        _towerAttack.IAttackSubs.Add(this);
    }

    public void AttackBallAttackEnemy(AttackBall ball, EnemyStats enemy)
    {
        if (enemy.HealtPoint * 100 / enemy.BaseHealthPoint >= _arctickBurn4ScriptibleObject.UnderEnemyProcentlyHealth)
        {
            enemy.HealthPointReduse(enemy.BaseHealthPoint * _arctickBurn4ScriptibleObject.UnderEnemyProcentlyHealth * 0.01f);
        }
        EnemyDebuffTower enemytowerDebuff = enemy.GetComponent<EnemyDebuffTower>();
        enemy.TakeDamage(enemy.HealtPoint * _arctickBurn4ScriptibleObject.ProcenteDamageBuff * 0.01f);
    }

    public void Attack()
    {
        GameObject ball = _towerAttack.CreateBall(_towerAttack.Tower.FireBallPrebuf);
        GetComponent<ExpireanceTower>().Attack(ball);
        ball.GetComponent<AttackBall>().Create(damage: _towerAttack.CalculateDamage(_towerStats.CurrentDamage), targerStats: _towerAttack._targetStats, towerAttack: _towerAttack);
        EnemyStats newTarget = null;
        Collider[] cols = Physics.OverlapSphere(transform.position, _towerStats.CurrentAttackRange);
        foreach (Collider col in cols)
        {
            if (col.GetComponent<EnemyStats>())
            {
                if (col.GetComponent<EnemyStats>() == _towerAttack._targetStats)
                    continue;
                if (Vector3.Distance(col.GetComponent<EnemyStats>().transform.position, transform.position) <= _towerStats.CurrentAttackRange)
                {
                    newTarget = col.GetComponent<EnemyStats>();
                    newTarget.Dies += _towerAttack.EnemyDies;
                    break;
                }
            }
        }
        if (newTarget == null)
            return;
        _towerAttack._targetStats.Dies -= _towerAttack.EnemyDies;
        _towerAttack._targetStats = newTarget;
    }

private void OnDestroy()
    {
        _towerAttack.IAttackBallAttackEnemySubs.Remove(this);
        _towerAttack.IAttackSubs.Remove(this);
    }
}
