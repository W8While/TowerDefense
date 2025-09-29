using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectedTowerButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TowerDetails _towerDetails;
    public TowerDetails towerDetails => _towerDetails;
    [SerializeField] private TMP_Text _cost;
    private Image _towerSprite;

    private TowerObjects _tower;
    public TowerObjects Tower => _tower;
    private bool _isEmpty = true;
    public bool isEmpty => _isEmpty;
    private bool _canBay = true;
    public bool canBay => _canBay;

    private void Awake()
    {
        _towerSprite = GetComponent<Image>();
    }

    private void OnEnable()
    {
        if (_tower != null)
        RefreshCost();
    }

    public void AddTower(TowerObjects tower)
    {
        _tower = tower;
        _isEmpty = false;
        _towerSprite.sprite = tower.Sprite;
        _cost.text = tower.NewCost.ToString();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_canBay)
            return;
        if (_isEmpty)
            return;
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            AllTowers.ChangeSelectTower(_tower);
            GetComponentInParent<MainCanvas>().ChangeActiveTowerPanel(false);
            return;
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            _towerDetails.ChangeDetails(_tower);
        }
    }
    public void ChangeCanBuy(bool what)
    {
        _canBay = what;
    }
    public void OnDestroy()
    {
        _tower = null;
        _isEmpty = true;
        _towerSprite.sprite = null;
        _cost.text = null;
    }

    private void RefreshCost()
    {
        _cost.text = _tower.NewCost.ToString();
    }
}
