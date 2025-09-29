using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgrade : MonoBehaviour
{

    private void OnEnable()
    {
        Time.timeScale = 0.001f;
    }
    public void ChangeCanvas()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<PanelTowerUpgrade>())
            {
                for (int k = 0; k < transform.GetChild(i).GetComponent<PanelTowerUpgrade>().transform.childCount; k++)
                {
                    if (transform.GetChild(i).GetComponent<PanelTowerUpgrade>().transform.GetChild(k).GetComponent<ButtonSkills>())
                    {
                        transform.GetChild(i).GetComponent<PanelTowerUpgrade>().transform.GetChild(k).GetComponent<ButtonSkills>().ChangeCanvas();
                    }
                }
            }
        }
    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
}
