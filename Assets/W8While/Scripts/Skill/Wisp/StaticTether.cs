using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StaticTether : MonoBehaviour
{
    static public List<TowerStats> TowersWithTeatherBuff = new List<TowerStats>();
    static public List<TowerStats> TowersWithoutTeatherBuff = new List<TowerStats>();

    static public List<TowerStats> TeatherTowers = new List<TowerStats>();


    private void OnEnable()
    {
        TowerBuild.BuildTower += BuildTower;
    }


    static public List<TowerStats> GetTitherTower(TowerStats tower, int max)
    {
        List<TowerStats> forCurrentTower = new List<TowerStats>();

        bool isAdd = true;
        foreach (TowerStats towerSkill in TowersWithTeatherBuff)
            if (tower == towerSkill)
                isAdd = false;
        if (isAdd)
        {
            TowersWithTeatherBuff.Add(tower);
            TowersWithoutTeatherBuff.Remove(tower);
            forCurrentTower.Add(tower);
        }



        for (int i = forCurrentTower.Count; i < max; i++)
        {
            if (AllTowers.AllPlaceTower.Count == TowersWithTeatherBuff.Count)
                return forCurrentTower;
            int rand = UnityEngine.Random.Range(0, TowersWithoutTeatherBuff.Count);
            TowersWithTeatherBuff.Add(TowersWithoutTeatherBuff[rand]);
            forCurrentTower.Add(TowersWithoutTeatherBuff[rand]);
            TowersWithoutTeatherBuff.RemoveAt(rand);
        }
        return forCurrentTower;

    }


    static public void TowerUpp()
    {
        TowersWithTeatherBuff.Clear();
        TowersWithoutTeatherBuff.Clear();
        foreach (TowerAttack towerAttack in AllTowers.AllPlaceTower)
        {
            if (!towerAttack.IsDestroyed())
            TowersWithoutTeatherBuff.Add(towerAttack.GetComponent<TowerStats>());
        }
    }

    static private void BuildTower(TowerAttack tower)
    {
        TowersWithoutTeatherBuff.Add(tower.GetComponent<TowerStats>());
    }
}
