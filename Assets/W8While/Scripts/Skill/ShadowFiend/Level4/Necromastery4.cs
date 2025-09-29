using System;
using UnityEngine;
using System.Collections.Generic;
using Unity.Hierarchy;

public class Necromastery4 : MonoBehaviour, IEnemyDies, ISkillDescription
{
    private Necromastery4ScriptibleObj _necromastery4ScriptibleObjects;

    private TowerAttack _towerAttack;
    private TowerStats _towerStats;
    private int _currentNecromastery;
    public int CurrentNecromastery => _currentNecromastery;
    private string _description => " Доп урон с душ - " + CurrentNecromastery * _necromastery4ScriptibleObjects.DamagePerNecromastery;

    private void Start()
    {
        _towerAttack = GetComponent<TowerAttack>();
        _towerStats = GetComponent<TowerStats>();
        _towerAttack.IEnemyDiesSubs.Add(this);
        _currentNecromastery = 0;

        try
        {
            _necromastery4ScriptibleObjects = (Necromastery4ScriptibleObj)_towerAttack.Tower.firstSkill;
        }
        catch (InvalidCastException)
        {
            _necromastery4ScriptibleObjects = (Necromastery4ScriptibleObj)_towerAttack.Tower.secondSkill;
        }


        _necromastery4ScriptibleObjects.ISkillDescriptionSubs.Add(this);
    }

    public void EnemyDies(AttackBall attackBall, EnemyStats enemy)
    {
        if (attackBall.tower == _towerAttack)
        {
            AddNecromastery(1);
            CreateBall(enemy.transform.position);
        }
    }


    private void AddNecromastery(int count)
    {
        _currentNecromastery += count;
        _towerStats.ChangeDamage(count * _necromastery4ScriptibleObjects.DamagePerNecromastery);
    }


    public string SkillDescription(TowerClickHandler towerClick)
    {
        if (towerClick == GetComponent<TowerClickHandler>())
            return _necromastery4ScriptibleObjects.Description + _description;
        return null;
    }

    private void CreateBall(Vector3 pos)
    {
        List<EnemyStats> enemyPosition = new List<EnemyStats>();

        Collider[] cols = Physics.OverlapSphere(pos, 30f);
        foreach (Collider collider in cols)
        {
            if (enemyPosition.Count >= _necromastery4ScriptibleObjects.goalAmount)
                break;
            if (collider.GetComponent<EnemyStats>())
                enemyPosition.Add(collider.GetComponent<EnemyStats>());
        }
        foreach (EnemyStats enemyStat in enemyPosition)
        {
            Vector3 positionBall = pos + Vector3.up;
            GameObject CurrentBall = Instantiate( _towerAttack.Tower.FireBallPrebuf, positionBall, Quaternion.identity);
            CurrentBall.GetComponent<AttackBall>().Create(damage: _currentNecromastery * _necromastery4ScriptibleObjects.UpperDamageX, targerStats: enemyStat, towerAttack: _towerAttack);

        }
    }


    private void OnDestroy()
    {
        _towerAttack.IEnemyDiesSubs.Remove(this); 
        _necromastery4ScriptibleObjects.ISkillDescriptionSubs.Remove(this);
    }
}
