using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EnemyBuyButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject _enemyPrebuf;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Image _lock;
    [SerializeField] private Image _imageTimer;
    [SerializeField] private List<AudioClip> coolDawnAudio = new List<AudioClip>();
    private AudioSource _audioSource;
    private TMP_Text _costText;
    private EnemyBoss _enemyBoss;
    private bool _inCoolDown = false;

    private float currentTime;
    private void Start()
    {
        GetComponent<Image>().sprite = _enemyPrebuf.GetComponent<EnemySetting>().EnemySprite;
        _audioSource = GetComponent<AudioSource>();
        _enemyBoss = _enemyPrebuf.GetComponent<EnemyBoss>();
        _costText = GetComponentInChildren<TMP_Text>();
        _costText.text = _enemyBoss.Cost.ToString();
    }

    private void OnEnable()
    {
        if (_inCoolDown)
            StartCoroutine(ReloadTimer());
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (_inCoolDown)
            {
                _audioSource.clip = coolDawnAudio[UnityEngine.Random.Range(0, coolDawnAudio.Count)];
                _audioSource.Play();
                return;
            }
            if (Gold.CurrentGold >= _enemyBoss.Cost)
            {
                GameObject a = Instantiate(_enemyPrebuf, EnemyRoad._startPosition, Quaternion.identity);
                _enemySpawner.BayEnemyBoss(a.GetComponent<EnemyStats>());
                EnemySpawner.AllPlaceEnemy.Add(a.GetComponent<EnemyStats>());
                Gold.Buy(_enemyBoss.Cost);
                _inCoolDown = true;
                currentTime = 0;
                StartCoroutine(ReloadTimer());
            }
        }
    }
    private IEnumerator ReloadTimer()
    {
        _lock.gameObject.SetActive(true);
        _imageTimer.gameObject.SetActive(true);
        while (currentTime < _enemyBoss.CoolDawnTime)
        {
            currentTime += Time.deltaTime;
            _imageTimer.rectTransform.offsetMax = new Vector2(_imageTimer.rectTransform.offsetMax.x, -(currentTime/_enemyBoss.CoolDawnTime * 180));
            yield return null;
        }
        _inCoolDown = false;
        _lock.gameObject.SetActive(false);
        _imageTimer.gameObject.SetActive(false);
        yield break;
    }

}
