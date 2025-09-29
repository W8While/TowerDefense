using UnityEngine;
using System.Collections.Generic;

public class ExpireanceTower : MonoBehaviour, IAttackBallAttackEnemy
{
    private TowerAttack _towerAttack;
    private TowerStats _towerStats;

    private int _needExp;
    public int NeedExp => _needExp;
    private int _currentLevel;
    public int CurrentLevel => _currentLevel;
    private int _currentExp;
    public int CurrentExp => _currentExp;

    private List<AttackBall> _towerBalls = new List<AttackBall>();
    private void Start()
    {
        _towerAttack = GetComponent<TowerAttack>();
        _towerStats= GetComponent<TowerStats>();
        _towerAttack.IAttackBallAttackEnemySubs.Add(this);
        _currentLevel = 1;
        _needExp = Expireance.GetNewLevel(_currentLevel+1);
    }

    public void EnemyDyes(AttackBall ball, EnemyStats enemy)
    {
        foreach (AttackBall attackBall in _towerBalls)
        {
            if (attackBall == ball)
            {
                ExpAdded((int)(enemy.HealtPoint / 2));
                break;
            }
        }
    }

    public void Attack(GameObject balls)
    {
        AttackBall ball = balls.GetComponent<AttackBall>();
        foreach (AttackBall attackBall in _towerBalls)
        {
            if (attackBall == ball)
                return;
        }
        _towerBalls.Add(ball);
    }

    public void AttackBallAttackEnemy(AttackBall ball, EnemyStats enemy)
    {
        foreach (AttackBall attackBall in _towerBalls)
        {
            if (attackBall == ball)
            {
                ExpAdded((int)ball.Damage);
                break;
            }
        }
        _towerBalls.Remove(ball);
    }
    private void ExpAdded(int exp)
    {
        _currentExp += exp;
        if (_currentExp > _needExp)
        {
            _currentLevel++;
            _currentExp -= _needExp;
            _needExp = Expireance.GetNewLevel(_currentLevel + 1);
            _towerStats.ChangeAttackSpeed(_towerAttack.Tower.LevelUpAttackSpeedBuff);
            _towerStats.ChangeDamage(_towerAttack.Tower.LevelUpDamageBuff);
            foreach (GameObject effect in AllEffects.EffectsPrefub)
                if (effect.GetComponent<LevelUpEffect>())
                {
                    GameObject a = Instantiate(effect, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
                    Destroy(a, 3f);
                    break;
                }

        }
    }

    private void OnDisable()
    {
        _towerAttack.IAttackBallAttackEnemySubs.Remove(this);
    }
}
