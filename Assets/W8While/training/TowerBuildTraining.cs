using UnityEngine;
using UnityEngine.EventSystems;

public class TowerBuildTraining : MonoBehaviour, ICheckButton
{
    [SerializeField] private int _currentGirls;
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
        if (_countTower == 1)
        {
            TrainingCanvas.ICheckButtonSubs.Remove(this);
            return true;
        }
        return false;
    }
}
