using UnityEngine;
using UnityEngine.EventSystems;

public class TowerClickHandler : MonoBehaviour, IPointerClickHandler, IAttackBallAttackEnemy
{
    private MainCanvas mainCanvas;
    private TowerAttack _towerAttack;
    private void Start()
    {
        mainCanvas = FindAnyObjectByType<MainCanvas>();
        _towerAttack = GetComponent<TowerAttack>();
        _towerAttack.IAttackBallAttackEnemySubs.Add(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        mainCanvas.ActiveTowerDescription();
        TowerDescription.ChangeTowerDescription(GetComponent<TowerAttack>().Tower, this);
    }

    private void OnDisable()
    {
        _towerAttack.IAttackBallAttackEnemySubs.Remove(this);
    }

    public void AttackBallAttackEnemy(AttackBall ball, EnemyStats enemy)
    {
        if (TowerDescription.SelectedTower == this)
            TowerDescription.ChangeTowerDescription(_towerAttack.Tower, this);
    }
}
