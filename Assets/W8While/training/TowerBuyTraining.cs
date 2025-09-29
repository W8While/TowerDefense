using Unity.VisualScripting;
using UnityEngine;

public class TowerBuyTraining : MonoBehaviour, ICheckButton
{
    [SerializeField] private int _currentGirls;
    [SerializeField] private int _needTower;
    [SerializeField] private TowerPanel _towerPanel;
    private int _countTower;

    private void Start()
    {
        _countTower = 0;
        TowerBuild.BuildTower += BuyTower;
        TrainingCanvas.ICheckButtonSubs.Add(this);
    }

    private void BuyTower(TowerAttack tower)
    {
        _countTower++;
    }

    public bool CheckButton(int currentGirls)
    {
        if (_currentGirls == currentGirls)
            return Check();
        return false;
    }

    private bool Check()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            _towerPanel.gameObject.SetActive(!_towerPanel.gameObject.activeSelf);
        if (_countTower == _needTower)
        {
            TrainingCanvas.ICheckButtonSubs.Remove(this);
            return true;
        }
        return false;
    }
}
