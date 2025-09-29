using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SplitShot : MonoBehaviour, IFindEnemys, ICheckTargetFromNull, ICheckRangeBetweenEnemy, IAttack, IEnemyDies, IEnemyGoOutRange
{
    private SplitShotScriptibleObjects _splitShotScriptibleObjects;

    private TowerAttack _towerAttack;
    private TowerStats _towerStats;

    private List<EnemyStats> _allTargetStats = new List<EnemyStats>();
    private void Start()
    {
        _towerAttack = GetComponent<TowerAttack>();
        _towerStats = GetComponent<TowerStats>();
        _towerAttack.IFindEnemysSubs.Add(this);
        _towerAttack.ICheckTargetFromNullSubs.Add(this);
        _towerAttack.ICheckRangeBetweenEnemySubs.Add(this);
        _towerAttack.IAttackSubs.Add(this);
        _towerAttack.IEnemyDiesSubs.Add(this);
        _towerAttack.IEnemyGoOutRangeSubs.Add(this);

        try
        {
            _splitShotScriptibleObjects = (SplitShotScriptibleObjects)_towerAttack.Tower.firstSkill;
        }
        catch (InvalidCastException)
        {
            _splitShotScriptibleObjects = (SplitShotScriptibleObjects)_towerAttack.Tower.secondSkill;
        }
    }

    public bool FindEnemys()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, _towerStats.CurrentAttackRange);
        foreach (Collider col in cols)
        {
            if (col.TryGetComponent<EnemyStats>(out EnemyStats enemy))
            {
                if (Vector3.Distance(enemy.transform.position, transform.position) <= _towerStats.CurrentAttackRange)
                { 
                    if (_allTargetStats.Count < _splitShotScriptibleObjects.GoalsAmount)
                    {
                        bool isAdd = true;
                        foreach (EnemyStats targetStats in _allTargetStats)
                            if (enemy == targetStats)
                                isAdd = false;
                        if (isAdd)
                        {
                            _allTargetStats.Add(enemy);
                            enemy.Dies += _towerAttack.EnemyDies;
                        }
                    }
                    else
                        break;
                }
            }
        }
        _towerAttack._targetStats = _allTargetStats.Count > 0 ? _allTargetStats[0] : null;
        return _allTargetStats.Count > 0;
    }

    public bool CheckTargetFromNull()
    {
        if (_allTargetStats.Count < _splitShotScriptibleObjects.GoalsAmount)
        {
            if (_towerAttack.FindEnemys())
                _towerAttack.AttackEnemy();
            return _allTargetStats.Count > 0;
        }
        return true;
    }

    public void CheckRangeBetweenEnemy()
    {
        List<EnemyStats> deleteEnemy = new List<EnemyStats>();
        foreach (EnemyStats _enemyStats in _allTargetStats)
        {
            if (Vector3.Distance(_enemyStats.transform.position, transform.position) > _towerStats.CurrentAttackRange)
                deleteEnemy.Add(_enemyStats);
        }
        foreach (EnemyStats _enemyStats in deleteEnemy)
            _towerAttack.EnemyGoOutRange(_enemyStats);
        _towerAttack._targetStats = _allTargetStats.Count > 0 ? _allTargetStats[0] : null;
        if (_allTargetStats.Count > 0)
        {
            _towerAttack._targetStats = _allTargetStats[0];
            foreach (EnemyStats _enemyStats in _allTargetStats)
            _towerAttack.AttackEnemy();
            return;
        }
    }

    public void Attack()
    {
        foreach (EnemyStats _enemyStats in _allTargetStats)
        {
            GameObject ball = _towerAttack.CreateBall(_towerAttack.Tower.FireBallPrebuf);
            GetComponent<ExpireanceTower>().Attack(ball);
            ball.GetComponent<AttackBall>().Create(damage: _towerAttack.CalculateDamage(_towerStats.CurrentDamage), targerStats: _enemyStats, towerAttack: _towerAttack);
        }
    }

    public void EnemyDies(AttackBall attackBall, EnemyStats enemy)
    {
        if (enemy == null)
            Debug.Log("SplitShotEnemyDies ÎØÈÁÊÀ, ÕÇ ÑÍÀ×ÀËÀ ÏÐÎÊÀËÀ ÏÎÒÎÌ ÍÅÒ È ß ÐÅØÈË ×ÒÎ ÏÎÔÈÊÑÈË ÅÅ ÅÑËÈ ÒÛ ÒÓÒ ÒÎ ÍÅ ÏÎÔÈÊÑÈË (");
        _allTargetStats.Remove(enemy);
        enemy.Dies -= _towerAttack.EnemyDies;
        _towerAttack._targetStats = _allTargetStats.Count > 0 ? _allTargetStats[0] : null;
    }

    public void EnemyGoOutRange(EnemyStats enemy)
    {
        _allTargetStats.Remove(enemy);
        enemy.Dies -= _towerAttack.EnemyDies;
        _towerAttack._targetStats = _allTargetStats.Count > 0 ? _allTargetStats[0] : null;
    }

    private void OnDestroy()
    {
        _towerAttack.IFindEnemysSubs.Remove(this);
        _towerAttack.ICheckTargetFromNullSubs.Remove(this);
        _towerAttack.ICheckRangeBetweenEnemySubs.Remove(this);
        _towerAttack.IAttackSubs.Remove(this);
        _towerAttack.IEnemyDiesSubs.Remove(this);
        _towerAttack.IEnemyGoOutRangeSubs.Remove(this);
    }
}
