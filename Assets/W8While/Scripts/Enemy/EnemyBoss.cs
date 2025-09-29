using System.Collections;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    [SerializeField] private int _cost;
    public int Cost => _cost;
    [SerializeField] private float _coolDawnTime;
    public float CoolDawnTime => _coolDawnTime;
    [SerializeField] private int _pointLevelReward;

    private void Start()
    {
        GetComponent<EnemyStats>().Dies += EnemyDie;
    }



    private void EnemyDie(AttackBall ball, EnemyStats stats)
    {
        ExpepeancePoint.ChangePoint(_pointLevelReward, 1);
    }

    private void OnDisable()
    {
        GetComponent<EnemyStats>().Dies -= EnemyDie;
    }

}
