using UnityEngine;
using System;

public class AttackBall : MonoBehaviour
{
    private EnemyStats _targerStats;
    private float _damage;
    public float Damage => _damage;
    private float _speed;
    public TowerAttack tower;
    public bool CanMove;
    private GameObject effect;
    private void Start()
    {
        CanMove = true;
        _speed = 8f;
        AllTowers.AllAttackBall.Add(this);
    }

    private void Update()
    {
        if (!CanMove)
            return;
        if (_targerStats != null)
        {
            Move();
            return;
        }
        Destroy(gameObject);
    }

    private void Move()
    {
        Vector3 enemyPosition = new Vector3(0, 0.5f, 0);
        enemyPosition += _targerStats.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, enemyPosition, _speed * Time.deltaTime);
        if (transform.position == enemyPosition)
        {
            _targerStats.TakeAttackBall(this);
            tower.AttackBallAttackEnemy(this, _targerStats);
            if (effect != null){
                GameObject a = Instantiate(effect, transform.position, Quaternion.identity);
                a.transform.SetParent(_targerStats.transform);
            }
            Destroy(gameObject);
        }
    }

    private void EnemyDies(AttackBall ball, EnemyStats enemy)
    {
        Destroy(gameObject);
    }


    public void Create(float damage, EnemyStats targerStats, TowerAttack towerAttack, GameObject effectAfterAttackEnemy = null)
    {
        tower = towerAttack;
        _targerStats = targerStats;
        _targerStats.Dies += EnemyDies;
        _damage = damage;
        effect = effectAfterAttackEnemy;
    }

    private void OnDestroy()
    {
        AllTowers.AllAttackBall.Remove(this);
        _targerStats.Dies -= EnemyDies;
    }
}
