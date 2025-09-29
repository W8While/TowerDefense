using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemySpawn> _enemySpawn;
    private List<EnemySpawn> _currentWave = new List<EnemySpawn>();
    private List<GameObject> _currentWaveGameOjbect = new List<GameObject>();
    [SerializeField] private GameObject _nextWave;
    [SerializeField] private GameObject _waveSprite;
    static public List<EnemyStats> AllPlaceEnemy = new List<EnemyStats>();
    private int _countWave;

    [SerializeField] private TMP_Text _timer;
    [SerializeField] private bool _isTraining;

    [SerializeField] private GameObject _fireWorkObject;
    [SerializeField] private GameObject _winGameCancas;
    private void Start()
    {
        AllPlaceEnemy.Clear();
        _countWave = 0;
        StartCoroutine(enemySpawnTimer(_enemySpawn));
    }
    private IEnumerator enemySpawnTimer(List<EnemySpawn> enemySpawn)
    {
        ChangeWave(enemySpawn);
        foreach (EnemySpawn enemy in enemySpawn)
        {
            for (int i = 0; i < enemy.amount; i++)
            {
                WaitForSeconds wait = new WaitForSeconds(enemy.spawnTime);
                if (enemy.enemyPrebuf != null)
                {
                    GameObject a = Instantiate(enemy.enemyPrebuf, EnemyRoad._startPosition, Quaternion.identity);
                    a.GetComponent<EnemyStats>().Dies += EnemyDie;
                    AllPlaceEnemy.Add(a.GetComponent<EnemyStats>());
                }
                else
                StartCoroutine(TimerSpawn(enemy.spawnTime));
                yield return wait;
            }
        }
        StartCoroutine(FinishGame());
    }
    
    private IEnumerator TimerSpawn(float time)
    {
        while (time > 0)
        {
            _timer.text = time.ToString();
            time--;
            yield return new WaitForSeconds(1);
        }
        _timer.text = " ";
        yield break;
    }

    public void BayEnemyBoss(EnemyStats enemyStats)
    {
        enemyStats.Dies += EnemyDie;
    }
    private void EnemyDie(AttackBall ball, EnemyStats stats)
    {
        if (stats.GetComponent<EnemyBoss>())
        {
            AllPlaceEnemy.Remove(stats);
            stats.Dies -= EnemyDie;
            return;
        }
        foreach (EnemyStats enemyStats in AllPlaceEnemy)
        {
            if (enemyStats.gameObject == stats.gameObject)
            {
                for (int i = 0; i < _currentWave.Count; i++)
                {
                    if (_currentWave[i].enemyPrebuf.GetComponent<EnemySetting>().Id == enemyStats.gameObject.GetComponent<EnemySetting>().Id)
                    {
                        _currentWave[i].amount--;
                        UpdateWaveUi(_currentWave);
                        AllPlaceEnemy.Remove(stats);
                        stats.Dies -= EnemyDie;
                        int count = 0;
                        for (int k = 0; k < _currentWave.Count; k++)
                        {
                            if (_currentWave[k].amount == 0)
                                count++;
                        }
                        if (count == _currentWave.Count)
                            FinishWave();
                        return;
                    }
                }
            }
        }

    }

    private void UpdateWaveUi(List<EnemySpawn> enemySpawn)
    {
        for (int i = 0; i < _currentWaveGameOjbect.Count; i++)
        {
            _currentWaveGameOjbect[i].GetComponentInChildren<TMP_Text>().text = _currentWave[i].amount.ToString();
        }
    }
    
    private void FinishWave()
    {
        foreach (GameObject gameObject in _currentWaveGameOjbect)
            Destroy(gameObject.gameObject);
        _currentWaveGameOjbect.Clear();
        _currentWave.Clear();
        ChangeWave(_enemySpawn);
    }

    private void ChangeWave(List<EnemySpawn> enemySpawn)
    {
        _countWave++;
        int wave = 0;
        foreach (EnemySpawn enemy in enemySpawn)
        {
            if (enemy.enemyPrebuf != null)
            {
                GameObject waveSprite = Instantiate(_waveSprite, transform.position, Quaternion.identity);
                waveSprite.transform.SetParent(_nextWave.transform);
                waveSprite.transform.localScale = Vector3.one;
                waveSprite.GetComponent<Image>().sprite = enemy.enemyPrebuf.GetComponent<EnemySetting>().EnemySprite;
                waveSprite.GetComponentInChildren<TMP_Text>().text = enemy.amount.ToString();
                _currentWaveGameOjbect.Add(waveSprite);
                _currentWave.Add(new EnemySpawn(enemy.enemyPrebuf, enemy.amount, enemy.spawnTime));
            }
            else
            {
                wave++;
                if (wave == _countWave)
                {
                    break;
                }
                foreach (GameObject gameObject in _currentWaveGameOjbect)
                    Destroy(gameObject.gameObject);                
                _currentWaveGameOjbect.Clear();
                _currentWave.Clear();
                continue;
            }
        }
    }

    private IEnumerator FinishGame()
    {
        while (AllPlaceEnemy.Count > 0)
            yield return null;
        if (_isTraining)
        {
            _fireWorkObject.gameObject.SetActive(true);
            _winGameCancas.SetActive(true);
        }
        else
        {
            _fireWorkObject.gameObject.SetActive(true);
            _winGameCancas.SetActive(true);
        }
        yield break;
    }
}





[Serializable]
public class EnemySpawn
{
    public EnemySpawn(GameObject enemy, int count, float time)
    {
        enemyPrebuf = enemy;
        amount = count;
        spawnTime = time;
    }
    public GameObject enemyPrebuf;
    public int amount;
    public float spawnTime;
}
