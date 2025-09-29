using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeminateAttack4 : MonoBehaviour, IAttackReload, ICreateBall, IAttackBallAttackEnemy
{
    [SerializeField] private GeminateAttack4ScriptibleObj _geminateAttack4SkillObjects;

    private TowerAttack _towerAttack;

    private bool _isCoolDawn;
    private List<AttackBall> _secondAttackBalls = new List<AttackBall>();
    private bool _isSecondBall;

    private void Start()
    {
        _towerAttack = GetComponent<TowerAttack>();

        _isCoolDawn = false;
        _isSecondBall = false;

        try
        {
            _geminateAttack4SkillObjects = (GeminateAttack4ScriptibleObj)_towerAttack.Tower.firstSkill;
        }
        catch (InvalidCastException)
        {
            _geminateAttack4SkillObjects = (GeminateAttack4ScriptibleObj)_towerAttack.Tower.secondSkill;
        }


        _towerAttack.IAttackReloadSubs.Add(this);
        _towerAttack.ICreateBallSubs.Add(this);
        _towerAttack.IAttackBallAttackEnemySubs.Add(this);
    }

    public IEnumerator AttackReload(float attackSpeed)
    {
        if (_isCoolDawn)
        {
            float _time = TowerObjects.BASEATTACKTIME / attackSpeed;
            yield return new WaitForSeconds(_time);
            _towerAttack.AtackReadyChange(true);
            yield break;
        }
        yield return new WaitForSeconds(_geminateAttack4SkillObjects.TimeSecondAttack);
        _isSecondBall = true;
        _towerAttack.AtackReadyChange(true);

        _isCoolDawn = true;
        yield return new WaitForSeconds(_geminateAttack4SkillObjects.CoolDawn);
        _isCoolDawn = false;
        yield break;
    }

    public GameObject CreateBall(GameObject ballPrefub)
    {
        Vector3 positionBall = transform.forward;
        positionBall.y += 1f;
        positionBall += transform.position;
        GameObject CurrentBall = Instantiate(ballPrefub, positionBall, Quaternion.identity);
        if (_isSecondBall)
            _secondAttackBalls.Add(CurrentBall.GetComponent<AttackBall>());
        _isSecondBall = false;
        return CurrentBall;
    }

    public void AttackBallAttackEnemy(AttackBall ball, EnemyStats enemy)
    {
        AttackBall destroyBall = null;
        foreach (AttackBall attackBall in _secondAttackBalls)
        {
            if (attackBall == ball)
            {
                float rand = UnityEngine.Random.Range(0, 360);
                Quaternion _rotarion = Quaternion.Euler(-50, rand, 0);
                Vector3 bugPosition = enemy.transform.position;
                bugPosition.y += 0.8f;
                GameObject bug = Instantiate(_geminateAttack4SkillObjects.Bug, bugPosition, _rotarion);
                bug.transform.SetParent(enemy.transform);
                enemy.GetComponent<EnemyDebuffTower>().GeminateBugAdd(_geminateAttack4SkillObjects.BugDamage, _geminateAttack4SkillObjects.BugAttackSpeed, _geminateAttack4SkillObjects.BugSpeedReduse);
                destroyBall = attackBall;
                break;
            }
        }
        if (destroyBall != null)
            _secondAttackBalls.Remove(destroyBall);
    }

    private void OnDestroy()
    {
        _towerAttack.IAttackReloadSubs.Remove(this);
        _towerAttack.ICreateBallSubs.Remove(this);
        _towerAttack.IAttackBallAttackEnemySubs.Remove(this);
    }
}
