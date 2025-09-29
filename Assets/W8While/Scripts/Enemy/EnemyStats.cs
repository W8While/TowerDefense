using System;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private float _baseHealthPoint;
    public float BaseHealthPoint => _baseHealthPoint;
    private float _healtPoint;
    public float HealtPoint => _healtPoint;
    [SerializeField] private int _goldRewards;

    private EnemyDebuffTower _enemyDebuffTower;
    private AttackBall _lastAttackBall;
    public event Action<AttackBall, EnemyStats> Dies;
    public event Action<float> GetDamage;

    public bool _isDie = false;
    private void Start()
    {
        _healtPoint = _baseHealthPoint;
        _enemyDebuffTower = GetComponent<EnemyDebuffTower>();
    }
    public void TakeAttackBall(AttackBall attackBall)
    {
        _lastAttackBall = attackBall;
        TakeDamage(attackBall.Damage);
    }

    public void TakeDamage(float damage)
    {
        damage = _enemyDebuffTower.AllDamageChanging(damage);
        _healtPoint -= damage;
        GetDamage?.Invoke(damage);
        if (_healtPoint <= 0)
        {
            _isDie = true;
            _healtPoint = 0;
            Die();
        }
    }
    public void HealthPointReduse(float reduse)
    {
        float hp = _healtPoint;
        _healtPoint = reduse;
        GetDamage?.Invoke(hp - reduse);
        if (_healtPoint <= 0)
        {
            _isDie = true;
            Die();
        }
    }

    private void Die()
    {
        Dies?.Invoke(_lastAttackBall, this);
        Gold.EnemyDies(_goldRewards);
        gameObject.SetActive(false);
        Invoke(nameof(DestroyThis), 1f);
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }
}
