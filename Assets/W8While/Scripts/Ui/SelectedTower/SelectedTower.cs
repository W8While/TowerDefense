using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectedTower : MonoBehaviour
{
    private List<SelectedTowerButton> _buttons = new List<SelectedTowerButton>();

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<SelectedTowerButton>())
            {
                _buttons.Add(transform.GetChild(i).GetComponent<SelectedTowerButton>());
            }
        }
        foreach (TowerObjects tower in AllTowers.Towers)
        {
            foreach (SelectedTowerButton butten in _buttons)
            {
                if (butten.isEmpty)
                {
                    butten.AddTower(tower);
                    break;
                }
            }
        }
    }
}
