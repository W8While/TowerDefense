using UnityEngine;

public class TrainingTowerUpgrade : MonoBehaviour, ICheckButton
{
    [SerializeField] private int _currentGirls;
    [SerializeField] private TowerPanel towerPanel;

    public bool CheckButton(int currentGirls)
    {
        if (_currentGirls == currentGirls)
            return Check();
        return false;
    }

    private bool Check()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            towerPanel.gameObject.SetActive(true);
            TrainingCanvas.ICheckButtonSubs.Remove(this);
            return true;
        }
        return false;
    }

    private void Start()
    {
        TrainingCanvas.ICheckButtonSubs.Add(this);
    }
}
