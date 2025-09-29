using System;
using UnityEngine;

public class CoopDeGrace : MonoBehaviour, ICalculateDamage, IAttack
{
    private CoopDeGraceSkillObjects _coopDeGraceSkillObjects;

    private TowerAttack _towerAttack;
    private TowerStats _towerStats;

    GameObject effect = null;
    private void Start()
    {
        _towerAttack = GetComponent<TowerAttack>();
        _towerStats = GetComponent<TowerStats>();
        _towerAttack.ICalculateDamageSubs.Add(this);
        _towerAttack.IAttackSubs.Add(this);


        try
        {
            _coopDeGraceSkillObjects = (CoopDeGraceSkillObjects)_towerAttack.Tower.firstSkill;
        }
        catch (InvalidCastException)
        {
            _coopDeGraceSkillObjects = (CoopDeGraceSkillObjects)_towerAttack.Tower.secondSkill;
        }
    }

    public float CalculateDamage(float currentDamage)
    {
        effect = null;
        int rand = UnityEngine.Random.Range(1, 100);
        if (rand <= _coopDeGraceSkillObjects.CritChance)
        {
            foreach (GameObject effects in AllEffects.EffectsPrefub)
                if (effects.GetComponent<EffectCoopDeGrace>())
                {
                    effect = effects;
                    break;
                }
            float newDamage = currentDamage * _coopDeGraceSkillObjects.CritDamage;
            return newDamage;
        }
        return currentDamage;
    }


    private void OnDestroy()
    {
        _towerAttack.ICalculateDamageSubs.Remove(this);
        _towerAttack.IAttackSubs.Remove(this);
    }

    public void Attack()
    {
        GameObject ball = _towerAttack.CreateBall(_towerAttack.Tower.FireBallPrebuf);
        GetComponent<ExpireanceTower>().Attack(ball);
        ball.GetComponent<AttackBall>().Create(damage: _towerAttack.CalculateDamage(_towerStats.CurrentDamage), targerStats: _towerAttack._targetStats, towerAttack: _towerAttack, effectAfterAttackEnemy: effect);
    }
}
