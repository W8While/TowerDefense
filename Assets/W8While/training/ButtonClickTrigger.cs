using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClickTrigger : MonoBehaviour, IPointerClickHandler, ICheckButton
{
    [SerializeField] private int _currentGirls;
    private SelectedTowerButton _selectedTowerButton;
    private bool _firstTimeClickTraining;
    private bool _isClicedRight;

    private void Start()
    {
        _firstTimeClickTraining = false;
        _isClicedRight = false;
        _selectedTowerButton = GetComponent<SelectedTowerButton>();
        _selectedTowerButton.ChangeCanBuy(false);
        TrainingCanvas.ICheckButtonSubs.Add(this);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            _isClicedRight = true;
            return;
        }
    }

    public bool CheckButton(int currentGirls)
    {
        if (_currentGirls == currentGirls)
            return Check();
        return false;
    }

    private bool Check()
    {
        if (_isClicedRight)
        {
            _selectedTowerButton.towerDetails.ChangeDetails(_selectedTowerButton.Tower);
            if (!_firstTimeClickTraining)
            {
                _firstTimeClickTraining = true;
                return true;
            }
            return false;
        }
        return false;
    }
    private void LateUpdate()
    {
        _isClicedRight = false;
    }
    private void OnDestroy()
    {
        TrainingCanvas.ICheckButtonSubs.Remove(this);
    }
}
