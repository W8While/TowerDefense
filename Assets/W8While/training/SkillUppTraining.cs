using UnityEngine;

public class SkillUppTraining : MonoBehaviour, ICheckButton
{
    [SerializeField] private int _currentGirls;
    [SerializeField] private TowerUpgrade _towerUpgrade;
    private void Start()
    {
        TrainingCanvas.ICheckButtonSubs.Add(this);
    }
    public bool CheckButton(int currentGirls)
    {
        if (_currentGirls == currentGirls)
            return Check();
        return false;
    }

    private bool Check()
    {
        if (Input.GetKeyDown(KeyCode.CapsLock)) { 
            _towerUpgrade.gameObject.SetActive(true);
            TrainingCanvas.ICheckButtonSubs.Remove(this);
            return true;
        }
        return false;
    }
}
