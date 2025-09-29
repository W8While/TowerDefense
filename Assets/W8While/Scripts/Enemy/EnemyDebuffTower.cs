using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDebuffTower : MonoBehaviour
{
    private int _geminateBugCount;
    private float _geminateBugSlow;
    private float _geminateBugDamage;
    private float _geminateBugAttackSpeed;

    private int _frostArrowCount;
    private float _frostArrowTimer;
    private float _frostArrowaDamagePerStack;

    private float _frostArrowsExplosionTimer;
    private float _frostArrowsExplosionDamage;
    private bool _isFrostArrowsExplosionActive;


    private EnemyMove _enemyMove;
    private EnemyStats _enemyStats;

    private void Start()
    {
        _enemyMove = GetComponent<EnemyMove>();
        _enemyStats = GetComponent<EnemyStats>();

        _frostArrowsExplosionDamage = 0;
        _isFrostArrowsExplosionActive = false;
        _frostArrowCount = 0;
    }

    public float AllDamageChanging(float damage)
    {
        float newDamage = damage;
        if (_frostArrowCount > 0)
        newDamage += _frostArrowaDamagePerStack * _frostArrowCount;


        return newDamage;
    }

    // ----------------------------------------------------------Geminate Bug -----------------------------------------------------------------------------------------------

    public void GeminateBugAdd(float damage, float attackSpeed, float slow)
    {
        _geminateBugAttackSpeed = attackSpeed;
        _geminateBugDamage = damage;
        _geminateBugSlow = slow;
        _geminateBugCount++;
        if (_geminateBugCount == 1 && gameObject.activeSelf)
            StartCoroutine(GeminateBugAttackCoroutine());

    }


    private IEnumerator GeminateBugAttackCoroutine()
    {
        while (_enemyStats.HealtPoint != 0)
        {
            GeminateBugAttack();
            yield return new WaitForSeconds(170f / _geminateBugAttackSpeed);
        }
    }

    private void GeminateBugAttack()
    {
        _enemyMove.ChangeSpeed(_enemyMove.Speed - _enemyMove.Speed * 0.01f * _geminateBugSlow);
        _enemyStats.TakeDamage(_geminateBugDamage);
    }



    // ----------------------------------------------------------FROST ARROWS -----------------------------------------------------------------------------------------------

    public void FrostArorwsAttack(int maxFrostArrowsAmount, float stackDuraction, float damagePerStack)
    {
        if (_frostArrowCount == 0 && gameObject.activeSelf)
            StartCoroutine(FrostArrowsTimer(stackDuraction));
        _frostArrowaDamagePerStack = damagePerStack;
        _frostArrowTimer = 0;
        if (_frostArrowCount < maxFrostArrowsAmount)
            _frostArrowCount++;
    }

    private IEnumerator FrostArrowsTimer(float stackDuraction)
    {
        while (true)
        {
            _frostArrowTimer += Time.deltaTime;
            if (_frostArrowTimer >= stackDuraction)
            {
                _frostArrowTimer = 0;
                _frostArrowCount = 0;
                yield break;
            }
            yield return null;
        }
    }


    // ----------------------------------------------------------FROST ARROWS EXPLOSION-------------------------------------------------------------------------------------

    public void AttackFrostArrowsEplosion(float timeBeforeExplosion, float radiusExplosion, float damageExplosionProcently, GameObject effect)
    {
        if (!_isFrostArrowsExplosionActive)
        {
            _isFrostArrowsExplosionActive = true;
            _frostArrowsExplosionTimer = 0;
            StartCoroutine(FrostArrowsExplosionTimer(timeBeforeExplosion, radiusExplosion, damageExplosionProcently, effect));
        }
    }

    private IEnumerator FrostArrowsExplosionTimer(float timeBeforeExplosion, float radiusExplosion, float damageExplosionProcently, GameObject effect)
    {
        _enemyStats.GetDamage += FrostArrowsExplosionGetDamage;
        while (_frostArrowsExplosionTimer < timeBeforeExplosion)
        {
            _frostArrowsExplosionTimer += Time.deltaTime;
            yield return null;
        }
        FrostArrowsExplosionDoing(radiusExplosion, damageExplosionProcently, _frostArrowsExplosionDamage, effect);
        _enemyStats.GetDamage -= FrostArrowsExplosionGetDamage;
        _frostArrowsExplosionDamage = 0;
        _frostArrowsExplosionTimer = 0;
        _isFrostArrowsExplosionActive = false;
        yield break;
    }

    private void FrostArrowsExplosionDoing(float radiusExplosion, float damageExplosionProcently, float damage, GameObject effect)
    {
        Instantiate(effect, transform.position, Quaternion.identity);
        Collider[] cols = Physics.OverlapSphere(transform.position, radiusExplosion);
        foreach (Collider col in cols)
        {
            if (col.GetComponent<EnemyStats>())
            {
                float resultDamage = damage * damageExplosionProcently * 0.01f;
                col.GetComponent<EnemyStats>().TakeDamage(resultDamage);
            }
        }
    }

    private void FrostArrowsExplosionGetDamage(float damage)
    {
        _frostArrowsExplosionDamage += damage;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
