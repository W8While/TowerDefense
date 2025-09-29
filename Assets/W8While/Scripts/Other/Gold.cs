using System;
using Unity.VisualScripting;
using UnityEngine;

static public class Gold
{
    static private int _currentGold =0;
    static public int CurrentGold => _currentGold;

    static public event Action<int> GoldChanging;
    
    static public void StartGold(int a)
    {
        _currentGold = a;
        GoldChanging?.Invoke(_currentGold);
    }

    static public void Start()
    {
        TowerBuild.BuildTower += BuyTower;
        GoldChanging?.Invoke(_currentGold);
    }

    static public void BuyTower(TowerAttack tower)
    {
        _currentGold -= tower.Tower.NewCost;
        GoldChanging?.Invoke(_currentGold);
    }
    static public void Buy(int cost)
    {
        _currentGold -= cost;
        GoldChanging?.Invoke(_currentGold);

    }
    static public void EnemyDies(int gold)
    {
        _currentGold += gold;
        GoldChanging?.Invoke(_currentGold);
    }
    static public void RefreshGold()
    {
        TowerBuild.BuildTower -= BuyTower;
    }
}
