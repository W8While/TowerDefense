using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tether : MonoBehaviour
{
    private TetherScripribleObject _tetherScripribleObject;

    private TowerAttack _towerAttack;
    private TowerStats _towerStats;

    private List<TowerStats> _towerWithTeatherBuff = new List<TowerStats>();

    private void Start()
    {
        _towerAttack = GetComponent<TowerAttack>();
        _towerStats = GetComponent<TowerStats>();
        try
        {
            _tetherScripribleObject = (TetherScripribleObject)_towerAttack.Tower.firstSkill;
        }
        catch (InvalidCastException)
        {
            _tetherScripribleObject = (TetherScripribleObject)_towerAttack.Tower.secondSkill;
        }

        _towerWithTeatherBuff = StaticTether.GetTitherTower(_towerStats, _tetherScripribleObject.MaxTowersWithBuff);
        foreach (TowerStats towerStats in _towerWithTeatherBuff)
        {
            towerStats.GetComponent<TowerSkills>().TeatherBuffAmount(1);
            GameObject partNew = Instantiate(_tetherScripribleObject.TetherEffect, towerStats.transform.position, Quaternion.identity);
            partNew.transform.SetParent(towerStats.transform);
            towerStats.ChangeAttackSpeed(_tetherScripribleObject.AttakcSpeedBuff);
            towerStats.ChangeDamage(_tetherScripribleObject.DamageBuff);
        }
    }

    private void OnDisable()
    {
        StaticTether.TowerUpp();
        foreach (TowerStats buffTower in _towerWithTeatherBuff)
        {
            if (buffTower.IsDestroyed())
                return;
            Destroy(buffTower.GetComponentInChildren<FieldTetherParticle>().gameObject);
            buffTower.GetComponent<TowerSkills>().TeatherBuffAmount(-1);
        }
        foreach (TowerStats buffTower in _towerWithTeatherBuff)
        {
            buffTower.ChangeAttackSpeed(-_tetherScripribleObject.AttakcSpeedBuff);
            buffTower.ChangeDamage(-_tetherScripribleObject.DamageBuff);
            Destroy(buffTower.GetComponentInChildren<FieldTetherParticle>().gameObject);
        }
    }
}
