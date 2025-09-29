using System;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Tether4 : MonoBehaviour
{
    private Tether4ScriptibleObj _tether4ScripribleObject;

    private TowerAttack _towerAttack;
    private TowerStats _towerStats;

    private List<TowerStats> _towerWithTeatherBuff = new List<TowerStats>();
    private List<TowerStats> _newBuffedTowers = new List<TowerStats>();

    private void Start()
    {
        _towerAttack = GetComponent<TowerAttack>();
        _towerStats = GetComponent<TowerStats>();
        try
        {
            _tether4ScripribleObject = (Tether4ScriptibleObj)_towerAttack.Tower.firstSkill;
        }
        catch (InvalidCastException)
        {
            _tether4ScripribleObject = (Tether4ScriptibleObj)_towerAttack.Tower.secondSkill;
        }

        _towerWithTeatherBuff = StaticTether.GetTitherTower(_towerStats, _tether4ScripribleObject.MaxTowersWithBuff);
        foreach (TowerStats towerStats in _towerWithTeatherBuff)
        {
            towerStats.GetComponent<TowerSkills>().TeatherBuffAmount(1);
            GameObject partNew = Instantiate(_tether4ScripribleObject.TetherEffect, towerStats.transform.position, Quaternion.identity);
            partNew.transform.SetParent(towerStats.transform);
            towerStats.ChangeAttackSpeed(_tether4ScripribleObject.AttakcSpeedBuff);
            towerStats.ChangeDamage(_tether4ScripribleObject.DamageBuff);
        }

        StartCoroutine(TetherSpawner());
    }




    private IEnumerator TetherSpawner()
    {
        WaitForSeconds wait = new WaitForSeconds(_tether4ScripribleObject.TimeBetweenGetBuff);
        while (true)
        {
            int rand = UnityEngine.Random.Range(0, AllTowers.AllPlaceTower.Count);
            TowerStats tower = AllTowers.AllPlaceTower[rand].GetComponent<TowerStats>();
            tower.GetComponent<TowerSkills>().TeatherBuffAmount(1);
            GameObject partNew = Instantiate(_tether4ScripribleObject.TetherEffect, tower.transform.position, Quaternion.identity);
            partNew.transform.SetParent(tower.transform);
            tower.ChangeAttackSpeed(_tether4ScripribleObject.AttakcSpeedBuff);
            tower.ChangeDamage(_tether4ScripribleObject.DamageBuff);
            _newBuffedTowers.Add(tower);
            yield return wait;
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
            buffTower.ChangeAttackSpeed(-_tether4ScripribleObject.AttakcSpeedBuff);
            buffTower.ChangeDamage(-_tether4ScripribleObject.DamageBuff);
            Destroy(buffTower.GetComponentInChildren<FieldTetherParticle>().gameObject);
        }
        StopCoroutine(TetherSpawner());
    }
}
