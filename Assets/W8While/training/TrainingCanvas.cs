using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TrainingCanvas : MonoBehaviour
{
    private MainCanvas mainCanvas;
    [SerializeField] private TowerPanel _towerPanel;
    [SerializeField] private TowerUpgrade _towerUpgrade;
    [SerializeField] private List<GirlsSprites> _girlsList = new List<GirlsSprites>();
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _text;
    private AudioSource _audioSource;

    private bool _isCorutinaWork;
    private int _currentGirls;
    private bool _isWaitButton;
    private bool _isEnd;
    static public List<ICheckButton> ICheckButtonSubs = new List<ICheckButton>();

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        mainCanvas = GetComponent<MainCanvas>();
        _isWaitButton = false;
        _isCorutinaWork = false;
        _isEnd = false;
        _currentGirls = 0;
        Time.timeScale = 0.001f;
        ChangeGirls();
        StartCoroutine(MessengMove(_girlsList[_currentGirls].messeng));
        _audioSource.clip = _girlsList[_currentGirls].audioMesseng;
        _audioSource.Play();
        mainCanvas.GameReady = false;
        //mainCanvas.enabled = false;
    }


    private void Update()
    {
        if (_isEnd)
            return;
        if (_girlsList[_currentGirls].key != KeyCode.None) 
        {
            foreach (ICheckButton iCheckButtonSubs in ICheckButtonSubs)
            {
                if (iCheckButtonSubs.CheckButton(_currentGirls))
                {
                    NextMess();
                    return;
                }
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            NextMess();
        }

    }
    public void NextMess()
    {
        if (_currentGirls == _girlsList.Count-1)
        {
            FinishDiallog();
            return;
        }
        if (_isCorutinaWork)
        {
            StopAllCoroutines();
            _isCorutinaWork = false;
            _text.text = _girlsList[_currentGirls].messeng;
            _audioSource.Stop();
        }
        else
        {
            _currentGirls++; 
            ChangeGirls();
            StartCoroutine(MessengMove(_girlsList[_currentGirls].messeng));
            _audioSource.clip = _girlsList[_currentGirls].audioMesseng;
            _audioSource.Play();
            //_isWaitButton = _girlsList[_currentGirls].key != KeyCode.None;
        }
    }

    private void ChangeGirls()
    {
        _image.rectTransform.sizeDelta = new Vector2(_girlsList[_currentGirls].girls.rect.width/1.3f, _girlsList[_currentGirls].girls.rect.height/ 1.3f);
        _image.sprite = _girlsList[_currentGirls].girls;
        _image.gameObject.transform.position = _girlsList[_currentGirls].pos;
    }

    private IEnumerator MessengMove(string mess)
    {
        _isCorutinaWork = true;
        _text.text = "";
        int _currentPos = 0;
        float _count = 0;
        int count = 1;
        while (_currentPos < mess.Length)
        {
            while (_count < 1)
            {
                _count += 0.2f;
                yield return null;
            }
            _count = 0;
            for (int i = _currentPos; i < _currentPos + count; i++)
            {
                if (_currentPos == mess.Length)
                    break;
                _text.text += mess[i];
            }
            _currentPos += count;
            yield return null;
        }
        _isCorutinaWork = false;
        yield break;
    }

    private void FinishDiallog()
    {
        _isEnd = true;
        Time.timeScale = 1f;
        Destroy(_image.gameObject);
        Destroy(_text.transform.parent.gameObject);
        //mainCanvas.enabled = true;
        mainCanvas.GameReady = true;
    }
}


[Serializable]
public class GirlsSprites
{
    public Sprite girls;
    public string messeng;
    public AudioClip audioMesseng;
    public Vector2 pos;
    public KeyCode key;
}



public interface ICheckButton
{
    public bool CheckButton(int currentGirls);
}
