using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;

public class ButtonClickTriggerBuy : MonoBehaviour, IPointerClickHandler, ICheckButton
{
    [SerializeField] private int _currentGirls;
    [SerializeField] private int _newGirls;
    private SelectedTowerButton _selectedTowerButton;
    private bool _isClicedLeft;

    private void Start()
    {
        _isClicedLeft = false;
        _selectedTowerButton = GetComponent<SelectedTowerButton>();
        _selectedTowerButton.ChangeCanBuy(false);
        TrainingCanvas.ICheckButtonSubs.Add(this);
        TowerBuild.BuildTower += BuyTower;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            _isClicedLeft = true;
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
        if (_isClicedLeft)
        {
            Transform parent = transform.parent;
            for (int i = 0; i < parent.childCount; i++)
            {
                if (parent.GetChild(i).GetComponent<SelectedTowerButton>())
                {
                    parent.GetChild(i).GetComponent<SelectedTowerButton>().ChangeCanBuy(true);
                }
            }
            Time.timeScale = 0.001f;
            return false;
        }
        return false;
    }

    private void BuyTower(TowerAttack towerAttack)
    {
        _currentGirls = _newGirls;
    }

    private void LateUpdate()
    {
        _isClicedLeft = false;
    }
    private void OnDisable()
    {
        _isClicedLeft = false;
    }
    private void OnDestroy()
    {
        TrainingCanvas.ICheckButtonSubs.Remove(this);
    }
}
