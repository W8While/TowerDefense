using System;
using UnityEngine;

public class FrostArrows : MonoBehaviour, IAttackBallAttackEnemy
{
    private FrostArrowsScriptibleObject _frostArrowsScriptibleObject;

    private TowerAttack _towerAttack;

    private void Start()
    {
        _towerAttack = GetComponent<TowerAttack>();

        _towerAttack.IAttackBallAttackEnemySubs.Add(this);

        try
        {
            _frostArrowsScriptibleObject = (FrostArrowsScriptibleObject)_towerAttack.Tower.firstSkill;
        }
        catch (InvalidCastException)
        {
            _frostArrowsScriptibleObject = (FrostArrowsScriptibleObject)_towerAttack.Tower.secondSkill;
        }


    }

    public void AttackBallAttackEnemy(AttackBall ball, EnemyStats enemy)
    {
        EnemyDebuffTower enemytowerDebuff = enemy.GetComponent<EnemyDebuffTower>();
        enemytowerDebuff.FrostArorwsAttack(_frostArrowsScriptibleObject.MaxArrowStacks, _frostArrowsScriptibleObject.StackDuraction, _frostArrowsScriptibleObject.DamagePerStack);
    }

    private void OnDestroy()
    {
        _towerAttack.IAttackBallAttackEnemySubs.Remove(this);
    }
}
