using System;
using UnityEngine;

public class Necromastery : MonoBehaviour, IEnemyDies, ISkillDescription
{
    private NecromasterySctiptibleObjects _necromasteryScriptibleObjects;

    private TowerAttack _towerAttack;
    private TowerStats _towerStats;
    private int _currentNecromastery;
    public int CurrentNecromastery => _currentNecromastery;
    private string _description => " Доп урон с душ - " + CurrentNecromastery * _necromasteryScriptibleObjects.DamagePerNecromastery;

    private void Start()
    {
        _towerAttack = GetComponent<TowerAttack>();
        _towerStats = GetComponent<TowerStats>();
        _towerAttack.IEnemyDiesSubs.Add(this);
        _currentNecromastery = 0;

        try
        {
            _necromasteryScriptibleObjects = (NecromasterySctiptibleObjects)_towerAttack.Tower.firstSkill;
        }
        catch (InvalidCastException)
        {
            _necromasteryScriptibleObjects = (NecromasterySctiptibleObjects)_towerAttack.Tower.secondSkill;
        }


        _necromasteryScriptibleObjects.ISkillDescriptionSubs.Add(this);
    }

    public void EnemyDies(AttackBall attackBall, EnemyStats enemy)
    {
        if (attackBall.tower == _towerAttack)
            AddNecromastery(1);
    }


    private void AddNecromastery(int count)
    {
        _currentNecromastery += count;
        _towerStats.ChangeDamage(count * _necromasteryScriptibleObjects.DamagePerNecromastery);
    }


    public string SkillDescription(TowerClickHandler towerClick)
    {
        if (towerClick == GetComponent<TowerClickHandler>())
        return _necromasteryScriptibleObjects.Description + _description;
        return null;
    }

    private void OnDestroy()
    {
        _towerAttack.IEnemyDiesSubs.Remove(this);
        _necromasteryScriptibleObjects.ISkillDescriptionSubs.Remove(this);
    }
}
