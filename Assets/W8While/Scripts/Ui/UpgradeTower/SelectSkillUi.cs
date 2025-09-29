using System.Collections.Generic;
using UnityEngine;

public class SelectSkillUi : MonoBehaviour
{
    private List<TowerDetails> _towersDetails = new List<TowerDetails>();


    private void OnEnable()
    {
        Time.timeScale = 0.001f;
    }
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<TowerDetails>())
            {
                _towersDetails.Add(transform.GetChild(i).GetComponent<TowerDetails>());
            }
        }

        foreach (TowerObjects tower in AllTowers.Towers)
        {
            foreach (TowerDetails towerDetails in _towersDetails)
            {
                if (towerDetails.Tower == null)
                {
                    towerDetails.ChangeDetails(tower);
                    break;
                }
            }
        }
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
}
