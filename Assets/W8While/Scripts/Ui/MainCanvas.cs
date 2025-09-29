using UnityEngine;
using System.Collections;
public class MainCanvas : MonoBehaviour
{
    [SerializeField] private int _goldStartGame;
    [SerializeField] private GameObject _buyEnemy;
    private TowerPanel _towerPanel;
    private TowerUpgrade _towerUpgrade;
    [SerializeField] private GameObject _towerDescription;

    public bool GameReady = true;

    private void Start()
    {
        Gold.StartGold(_goldStartGame);
        _towerPanel = GetComponentInChildren<TowerPanel>();
        _towerUpgrade = GetComponentInChildren<TowerUpgrade>(true);
        ChangeActiveTowerPanel(false);
        ChangeActiveTowerUpgrade(false);
    }
    private void Update()
    {
        if (!GameReady)
            return;
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ChangeActiveTowerPanel(!_towerPanel.gameObject.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            ChangeActiveTowerUpgrade(!_towerUpgrade.gameObject.activeSelf);
            _towerUpgrade.ChangeCanvas();
        }
    }


    public void ChangeActiveTowerUpgrade(bool active)
    {
        _towerUpgrade.gameObject.SetActive(active);
    }


    public void ChangeActiveTowerPanel(bool active)
    {
        if (active)
        {
            Time.timeScale = 0.001f;
            _towerDescription.gameObject.SetActive(false);
            _buyEnemy.gameObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            if (_towerDescription.gameObject.activeSelf)
            _towerDescription.gameObject.SetActive(true);
            _buyEnemy.gameObject.SetActive(true);
        }
        _towerPanel.gameObject.SetActive(active);
    }

    public void ActiveTowerDescription()
    {
        if (!_towerDescription.gameObject.activeSelf)
            _towerDescription.gameObject.SetActive(true);
    }
    public void OnDestroy()
    {
        Gold.RefreshGold();
    }
}
