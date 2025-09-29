using System;
using System.Collections;
using UnityEngine;

public class Fevor4 : MonoBehaviour, IAttack
{
    private Fevor4ScriptibleObj _fevor4ScroptibleObject;

    private TowerAttack _towerAttack;
    private TowerStats _towerStats;

    private EnemyStats _lastEnemy;
    private int _currentCount;
    static private bool _isTimerStarting = false;
    static private bool _isCoolDawn;

    void Start()
    {
        _towerAttack = GetComponent<TowerAttack>();
        _towerStats = GetComponent<TowerStats>();
        _towerAttack.IAttackSubs.Add(this);
        _currentCount = 0;
        _isCoolDawn = false;
        try
        {
            _fevor4ScroptibleObject = (Fevor4ScriptibleObj)_towerAttack.Tower.firstSkill;
        }
        catch (InvalidCastException)
        {
            _fevor4ScroptibleObject = (Fevor4ScriptibleObj)_towerAttack.Tower.secondSkill;
        }

        if (!_isTimerStarting)
        {
            ChangeStopGame();
            _isTimerStarting = true;
        }
    }

    public void Attack()
    {
        if (_towerAttack._targetStats == _lastEnemy)
        {
            _towerStats.ChangeAttackSpeed(_fevor4ScroptibleObject.AttackSpeedBuff);
            _currentCount++;
        }
        else
        {
            _towerStats.ChangeAttackSpeed(-_fevor4ScroptibleObject.AttackSpeedBuff * _currentCount);
            _currentCount = 0;
        }
        _lastEnemy = _towerAttack._targetStats;
        GameObject ball = _towerAttack.CreateBall(_towerAttack.Tower.FireBallPrebuf);
        GetComponent<ExpireanceTower>().Attack(ball);
        ball.GetComponent<AttackBall>().Create(damage: _towerAttack.CalculateDamage(_towerStats.CurrentDamage), targerStats: _towerAttack._targetStats, towerAttack: _towerAttack);
    }

    private void ChangeStopGame()
    {
        foreach (TowerAttack towers in AllTowers.AllPlaceTower)
        {
            if (towers.GetComponent<Fevor4>())
            {
                if (!_isCoolDawn)
                    towers.GetComponent<TowerStats>().ChangeDamage(-towers.GetComponent<TowerStats>().CurrentDamage);
                else
                    towers.GetComponent<TowerStats>().ChangeDamage(towers.GetComponent<TowerStats>().BaseDamage);
                continue;
            }
            towers.ChangeAttack(_isCoolDawn);
        }

        foreach (EnemyStats enemys in EnemySpawner.AllPlaceEnemy)
            enemys.GetComponent<EnemyMove>().ChangeMove(_isCoolDawn);
        foreach (AttackBall attackBall in AllTowers.AllAttackBall)
            attackBall.CanMove = _isCoolDawn;
        if (!_isCoolDawn)
        {
            Vector3 positionEffects = new Vector3(0, 10, 0);
            foreach (GameObject effect in AllEffects.EffectsPrefub)
                if (effect.GetComponent<EffectsFevor>())
                {
                    GameObject effects = Instantiate(effect, positionEffects, Quaternion.identity);
                    StartCoroutine(DestroyEffect(effects));
                    break;
                }
            Invoke(nameof(ReloadField), _fevor4ScroptibleObject.stopTimeDuraction);
        }
        else
            Invoke(nameof(ReloadField), _fevor4ScroptibleObject.StopTimeCoolDawn);
    }
    private void ReloadField()
    {
        _isCoolDawn = !_isCoolDawn;
        ChangeStopGame();
    }

    private IEnumerator DestroyEffect(GameObject effect)
    {
        yield return new WaitForSeconds(_fevor4ScroptibleObject.stopTimeDuraction);
        Destroy(effect);
    }

    private void OnDisable()
    {
        _towerAttack.IAttackSubs.Remove(this);
        _isTimerStarting = false;
    }
}
