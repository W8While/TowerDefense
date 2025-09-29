using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerBuild : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject _currentTower;
    private bool _isEmpty;
    public bool IsEmpty => _isEmpty;

    static public event Action<TowerAttack> BuildTower;
    private void Start()
    {
        _isEmpty = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if ((eventData.button == 0) && _isEmpty && AllTowers.SelectedTower != null && EnouhgGold())
        {
            _isEmpty = false;
            Vector3 positionSpawn = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            GameObject tower = Instantiate(AllTowers.SelectedTower.Prebuf, positionSpawn, Quaternion.identity);
            BuildTower?.Invoke(tower.GetComponent<TowerAttack>());
            int newCost = (int)Math.Round(AllTowers.SelectedTower.NewCost * AllTowers.SelectedTower.UpCost, 0);
            if (newCost == AllTowers.SelectedTower.NewCost)
                newCost++;
            AllTowers.SelectedTower.ChangeCost(newCost);
            AllTowers.AllPlaceTower.Add(tower.GetComponent<TowerAttack>());
            AllTowers.ChangeSelectTower(null);
        }
    }
    private bool EnouhgGold()
    {
        int cost = AllTowers.SelectedTower.NewCost;
        return cost <= Gold.CurrentGold;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_isEmpty && AllTowers.SelectedTower != null)
        {
            Vector3 positionSpawn = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            _currentTower = Instantiate(AllTowers.SelectedTower.PrevierPrebuf, positionSpawn, Quaternion.identity);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_currentTower != null)
            Destroy(_currentTower);
    }
}
