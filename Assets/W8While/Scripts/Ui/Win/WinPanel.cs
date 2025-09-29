using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private List<WinCash> _winCash = new List<WinCash>();
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _image;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private GameObject _mainCanvas;
    private int _count;
    private bool _isCorutinaWork;

    private bool _isEnd;

    void Start()
    {
        _mainCanvas.SetActive(false);
        _isEnd = false;
        _count = 0;
        NextMove();
    }

    void Update()
    {
        if (_isEnd)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            if (_isCorutinaWork)
            {
                StopAllCoroutines();
                _isCorutinaWork = false;
                _text.text = _winCash[_count]._text;
                _audioSource.Stop();
            }
            else
            {
                if (_count == _winCash.Count - 1)
                {
                    _isEnd = true;
                    StartCoroutine(Finaly());
                    return;
                }
                _count++;
                NextMove();
            }
        }
    }


    private IEnumerator Finaly()
    {
        _videoPlayer.Play();
        _text.transform.parent.gameObject.SetActive(false);
        _image.gameObject.SetActive(false);
        _videoPlayer.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        while (_videoPlayer.isPlaying)
            yield return null;
        yield return new WaitForSeconds(3f);
        AllTowers.DeleteAllTowerSkills();
        SceneManager.LoadScene("MainMenu");
    }

    private void NextMove()
    {
        StartCoroutine(MessengMove(_winCash[_count]._text));
        _image.sprite = _winCash[_count]._girlsSprite;
        _image.rectTransform.sizeDelta = new Vector2(_winCash[_count]._girlsSprite.rect.width, _winCash[_count]._girlsSprite.rect.height);
        _audioSource.clip = _winCash[_count].audio;
        _audioSource.Play();
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

}





[Serializable]
public class WinCash
{
    public AudioClip audio;
    public string _text;
    public Vector3 _girlsPos;
    public Sprite _girlsSprite;
}
