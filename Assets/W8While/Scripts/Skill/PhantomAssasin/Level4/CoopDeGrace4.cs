using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.VisualScripting;

public class CoopDeGrace4 : MonoBehaviour, ICalculateDamage, IAttackBallAttackEnemy, ICreateBall, ICheckTargetFromNull, IAttack
{
    private CoopDeGraceSkillObjects _coopDeGrace4SkillObjects;

    private TowerAttack _towerAttack;
    private TowerStats _towerStats;
    private int _countOfThisTower;
    private float _anotherDamage;

    GameObject effect = null;
    private void Start()
    {
        _towerAttack = GetComponent<TowerAttack>();
        _towerStats = GetComponent<TowerStats>();
        _towerAttack.ICalculateDamageSubs.Add(this);
        _towerAttack.ICreateBallSubs.Add(this);
        _towerAttack.ICheckTargetFromNullSubs.Add(this);
        _towerAttack.IAttackSubs.Add(this);

        try
        {
            _coopDeGrace4SkillObjects = (CoopDeGraceSkillObjects)_towerAttack.Tower.firstSkill;
        }
        catch (InvalidCastException)
        {
            _coopDeGrace4SkillObjects = (CoopDeGraceSkillObjects)_towerAttack.Tower.secondSkill;
        }
    }

    public float CalculateDamage(float currentDamage)
    {
        currentDamage += (float)(_anotherDamage / _countOfThisTower);
        int rand = UnityEngine.Random.Range(1, 100);
        if (rand <= _coopDeGrace4SkillObjects.CritChance)
        {
            foreach (GameObject effects in AllEffects.EffectsPrefub)
                if (effects.GetComponent<EffectCoopDeGrace>())
                {
                    effect = effects;
                    break;
                }
            float newDamage = currentDamage * _coopDeGrace4SkillObjects.CritDamage;
            return newDamage;
        }
        return currentDamage;
    }

    public void AttackBallAttackEnemy(AttackBall ball, EnemyStats enemy)
    {
    }

    public GameObject CreateBall(GameObject ballPrefub)
    {
        Vector3 positionBall = transform.forward;
        positionBall.y += 1f;
        positionBall += transform.position;
        GameObject CurrentBall = Instantiate(ballPrefub, positionBall, Quaternion.identity);
        return CurrentBall;
    }

    public bool CheckTargetFromNull()
    {
        _anotherDamage = 0;
        _countOfThisTower = 0;
        foreach (TowerAttack towerAttack in AllTowers.AllPlaceTower)
        {
            if (towerAttack.GetComponent<CoopDeGrace4>())
            {
                _countOfThisTower++;
            }
            else
            {
                _anotherDamage += towerAttack.GetComponent<TowerStats>().CurrentDamage;
                towerAttack.ChangeAttack(false);
            }
        }
        if (_towerAttack._targetStats == null)
        {
            if (_towerAttack.FindEnemys())
                _towerAttack.AttackEnemy();
            return false;
        }
        return true;
    }

    public void Attack()
    {
        GameObject ball = _towerAttack.CreateBall(_towerAttack.Tower.FireBallPrebuf);
        GetComponent<ExpireanceTower>().Attack(ball);
        ball.GetComponent<AttackBall>().Create(damage: _towerAttack.CalculateDamage(_towerStats.CurrentDamage), targerStats: _towerAttack._targetStats, towerAttack: _towerAttack, effectAfterAttackEnemy: effect);
    }

    private void OnDestroy()
    {
        _towerAttack.ICalculateDamageSubs.Remove(this);
        _towerAttack.ICreateBallSubs.Remove(this);
        _towerAttack.ICheckTargetFromNullSubs.Remove(this);
        _towerAttack.IAttackSubs.Remove(this);
    }
}
