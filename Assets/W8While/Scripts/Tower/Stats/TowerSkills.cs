using System;
using UnityEngine;

public class TowerSkills : MonoBehaviour
{
    private int _teatherBuffAmount;

    public void TeatherBuffAmount(int amount)
    {
        _teatherBuffAmount += amount;
    }
}
