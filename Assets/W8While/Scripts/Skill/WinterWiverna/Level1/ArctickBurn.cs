using System;
using UnityEngine;

public class ArctickBurn : MonoBehaviour, IAttackBallAttackEnemy
{
    private ArctickBurnScriptibleObject _arctickBurnScriptibleObject;

    private TowerAttack _towerAttack;
    private TowerStats _towerStats;

    private void Start()
    {
        _towerAttack = GetComponent<TowerAttack>();
        _towerStats = GetComponent<TowerStats>();

        try
        {
            _arctickBurnScriptibleObject = (ArctickBurnScriptibleObject)_towerAttack.Tower.firstSkill;
        }
        catch (InvalidCastException)
        {
            _arctickBurnScriptibleObject = (ArctickBurnScriptibleObject)_towerAttack.Tower.secondSkill;
        }

        _towerAttack.IAttackBallAttackEnemySubs.Add(this);
    }

    public void AttackBallAttackEnemy(AttackBall ball, EnemyStats enemy)
    {
        EnemyDebuffTower enemytowerDebuff = enemy.GetComponent<EnemyDebuffTower>();
        enemy.TakeDamage(enemy.HealtPoint * _arctickBurnScriptibleObject.ProcenteDamageBuff * 0.01f);
    }

    private void OnDestroy()
    {
        _towerAttack.IAttackBallAttackEnemySubs.Remove(this);
    }
}
