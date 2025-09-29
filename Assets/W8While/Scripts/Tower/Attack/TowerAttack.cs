using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour, ICreateBall, ICalculateDamage, IAttack, IAttackReload, IEnemyDies, IFindEnemys, ICheckTargetFromNull, ICheckRangeBetweenEnemy, IEnemyGoOutRange, IAttackBallAttackEnemy
{
    [SerializeField] private TowerObjects _tower;
    public TowerObjects Tower => _tower;
    private TowerStats _towerStats;

    public EnemyStats _targetStats;
    private bool _isAttackReady;
    public bool IsAttackReady => _isAttackReady;

    private bool _canAttack;
    public bool CanAttack => _canAttack;
    public void ChangeAttack(bool what)
    {
        _canAttack = what;
    }


    public List<ICreateBall> ICreateBallSubs = new List<ICreateBall>();
    public List<ICalculateDamage> ICalculateDamageSubs = new List<ICalculateDamage>();
    public List<IAttack> IAttackSubs = new List<IAttack>();
    public List<IAttackReload> IAttackReloadSubs = new List<IAttackReload>();
    public List<IEnemyDies> IEnemyDiesSubs = new List<IEnemyDies>();
    public List<IFindEnemys> IFindEnemysSubs = new List<IFindEnemys>();
    public List<ICheckTargetFromNull> ICheckTargetFromNullSubs = new List<ICheckTargetFromNull>();
    public List<ICheckRangeBetweenEnemy> ICheckRangeBetweenEnemySubs = new List<ICheckRangeBetweenEnemy>();
    public List<IEnemyGoOutRange> IEnemyGoOutRangeSubs = new List<IEnemyGoOutRange>();
    public List<IAttackBallAttackEnemy> IAttackBallAttackEnemySubs = new List<IAttackBallAttackEnemy>();

    private void Start()
    {
        _canAttack = true;
        _isAttackReady = true;
        _towerStats = GetComponent<TowerStats>();
    }

    private void Update()
    {
        if (!_canAttack)
            return;
        if (!CheckTargetFromNull()) return;
        CheckRangeBetweenEnemy();
    }

    public bool CheckTargetFromNull()
    {
        if (ICheckTargetFromNullSubs.Count > 0)
        {
            bool ret = true;
            foreach (var checkTartgetFrommNull in ICheckTargetFromNullSubs)
            {
                ret = checkTartgetFrommNull.CheckTargetFromNull();
            }
            return ret;
        }
        if (_targetStats == null)
        {
            if (FindEnemys())
                AttackEnemy();
            return false;
        }
        return true;
    }

    public void CheckRangeBetweenEnemy()
    {
        if (ICheckRangeBetweenEnemySubs.Count > 0)
        {
            foreach (var checkRangeBetweenEnemy in ICheckRangeBetweenEnemySubs)
            {
                checkRangeBetweenEnemy.CheckRangeBetweenEnemy();
            }
            return;
        }
        if (Vector3.Magnitude(_targetStats.transform.position - transform.position) <= _towerStats.CurrentAttackRange)
        {
            AttackEnemy();
            return;
        }
        EnemyGoOutRange(_targetStats);
    }

    public bool FindEnemys()
    {
        if (IFindEnemysSubs.Count > 0)
        {
            bool ret = true;
            foreach (var findEnemys in IFindEnemysSubs)
            {
                ret = findEnemys.FindEnemys();
            }
            return ret;
        }
        Collider[] cols = Physics.OverlapSphere(transform.position, _towerStats.CurrentAttackRange);
        foreach (Collider col in cols)
        {
            if (col.GetComponent<EnemyStats>())
            {
                if (Vector3.Distance(col.GetComponent<EnemyStats>().transform.position, transform.position) <= _towerStats.CurrentAttackRange) {
                    _targetStats = col.GetComponent<EnemyStats>();
                    _targetStats.Dies += EnemyDies;
                    return true;
                }
            }
            _targetStats = null;
        }
        return false;
    }

    public void EnemyDies(AttackBall ball, EnemyStats enemy)
    {
        GetComponent<ExpireanceTower>().EnemyDyes(ball, enemy);
        if (IEnemyDiesSubs.Count > 0)
        {
            foreach (var enemyDies in IEnemyDiesSubs)
            {
                enemyDies.EnemyDies(ball, enemy);
            }
            return;
        }
        _targetStats.Dies -= EnemyDies;
        _targetStats = null;
    }

    public void EnemyGoOutRange(EnemyStats enemy)
    {
        if (IEnemyGoOutRangeSubs.Count > 0)
        {
            foreach (var enemyOutRange in IEnemyGoOutRangeSubs)
            {
                enemyOutRange.EnemyGoOutRange(enemy);
            }
            return;
        }
        _targetStats.Dies -= EnemyDies;
        _targetStats = null;
    }

    public void AttackEnemy()
    {
        transform.LookAt(_targetStats.transform.position);
        if (_isAttackReady)
        {
            _isAttackReady = false;
            Attack();
            StartCoroutine(AttackReload(_towerStats.CurrentAttackSpeed));
        }
    }


    public IEnumerator AttackReload(float attackSpeed)
    {
        if (IAttackReloadSubs.Count > 0)
        {
            foreach (var attackReload in IAttackReloadSubs)
            {
                StartCoroutine(attackReload.AttackReload(attackSpeed));
            }
            yield break;
        }
        float _time = TowerObjects.BASEATTACKTIME / attackSpeed;
        yield return new WaitForSeconds(_time);
        AtackReadyChange(true);
        yield break;
    }

    public void AtackReadyChange(bool changing)
    {
        _isAttackReady = changing;
    }






    public GameObject CreateBall(GameObject ballPrefub)
    {
        if (ICreateBallSubs.Count > 0)
        {
            GameObject balll = null;
            foreach (var ball in ICreateBallSubs)
            {
                balll = ball.CreateBall(ballPrefub);
            }
            return balll;
        }
        Vector3 positionBall = transform.forward;
        positionBall.y += 1f;
        positionBall += transform.position;
        GameObject CurrentBall = Instantiate(ballPrefub, positionBall, Quaternion.identity);
        return CurrentBall;
    }


    public float CalculateDamage(float currentDamage)
    {
        float resultDamage = currentDamage;
        if (ICalculateDamageSubs.Count > 0)
        {
            foreach (var damage in ICalculateDamageSubs)
            {
                return damage.CalculateDamage(resultDamage);
            }
            return 0;
        }
        return resultDamage;
    }

    public void Attack()
    {
        if (IAttackSubs.Count > 0)
        {
            foreach (var attack in IAttackSubs)
            {
                attack.Attack();
            }
            return;
        }
        GameObject ball = CreateBall(_tower.FireBallPrebuf);
        GetComponent<ExpireanceTower>().Attack(ball);
        ball.GetComponent<AttackBall>().Create(damage: CalculateDamage(_towerStats.CurrentDamage), targerStats: _targetStats, towerAttack: this);
    }

    public void AttackBallAttackEnemy(AttackBall ball, EnemyStats enemy)
    {
        if (IAttackBallAttackEnemySubs.Count > 0)
        {
            foreach (var attackBallEnemy in IAttackBallAttackEnemySubs)
            {
                attackBallEnemy.AttackBallAttackEnemy(ball, enemy);
            }
            return;
        }
    }
}










public interface ICreateBall
{
    public GameObject CreateBall(GameObject ballPrefub);
}
public interface ICalculateDamage
{
    public float CalculateDamage(float currentDamage);
}
public interface IAttack
{
    public void Attack();
}
public interface IAttackReload
{
    public IEnumerator AttackReload(float _time);
}
public interface IEnemyDies
{
    public void EnemyDies(AttackBall attackBall, EnemyStats enemy);
}
public interface IFindEnemys
{
    public bool FindEnemys();
}
public interface ICheckTargetFromNull
{
    public bool CheckTargetFromNull();
}
public interface ICheckRangeBetweenEnemy
{
    public void CheckRangeBetweenEnemy();
}
public interface IEnemyGoOutRange
{
    public void EnemyGoOutRange(EnemyStats enemy);
}
public interface IAttackBallAttackEnemy
{
    public void AttackBallAttackEnemy(AttackBall ball, EnemyStats enemy);
}