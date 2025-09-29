using UnityEngine;

public class SelectSkillTreining : MonoBehaviour, ICheckButton
{
    [SerializeField] private GameObject textPanelk;
    [SerializeField] private int _currentGirls;
    [SerializeField] private int _nextCurrentGirls;
    void Start()
    {
        Time.timeScale = 0.001f;
        textPanelk.transform.position = new Vector3(500, 0, 0);
        TrainingCanvas.ICheckButtonSubs.Add(this);
    }

    public bool CheckButton(int currentGirls)
    {
        if (_currentGirls != currentGirls)
            return false;
        if (_currentGirls != _nextCurrentGirls) {
            _currentGirls = _nextCurrentGirls;
            return true;
        }
        return false;
    }


    private void OnDisable()
    {
        _nextCurrentGirls--;
        Invoke(nameof(RemoveAtList), 0.5f);
        Time.timeScale = 0.001f;
    }
    private void RemoveAtList()
    {
        TrainingCanvas.ICheckButtonSubs.Remove(this);
    }
}
