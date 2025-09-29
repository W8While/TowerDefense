using System;
using UnityEngine;

public class FrostArrows4 : MonoBehaviour, IAttackBallAttackEnemy
{
    private FrostArrows4ScriptibleObj _frostArrows4ScriptibleObject;

    private TowerAttack _towerAttack;

    private void Start()
    {
        _towerAttack = GetComponent<TowerAttack>();

        _towerAttack.IAttackBallAttackEnemySubs.Add(this);

        try
        {
            _frostArrows4ScriptibleObject = (FrostArrows4ScriptibleObj)_towerAttack.Tower.firstSkill;
        }
        catch (InvalidCastException)
        {
            _frostArrows4ScriptibleObject = (FrostArrows4ScriptibleObj)_towerAttack.Tower.secondSkill;
        }
    }

    public void AttackBallAttackEnemy(AttackBall ball, EnemyStats enemy)
    {
        EnemyDebuffTower enemytowerDebuff = enemy.GetComponent<EnemyDebuffTower>();
        enemytowerDebuff.FrostArorwsAttack(_frostArrows4ScriptibleObject.MaxArrowStacks, _frostArrows4ScriptibleObject.StackDuraction, _frostArrows4ScriptibleObject.DamagePerStack);
        enemytowerDebuff.AttackFrostArrowsEplosion(_frostArrows4ScriptibleObject.TimeBeforeExplosion, _frostArrows4ScriptibleObject.RadiusExplosion, _frostArrows4ScriptibleObject.DamageProcently, _frostArrows4ScriptibleObject.ExplosionEffect);
    }

    private void OnDestroy()
    {
        _towerAttack.IAttackBallAttackEnemySubs.Remove(this);
    }
}
