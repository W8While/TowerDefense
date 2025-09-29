using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.UI;


public class SoundGame : MonoBehaviour
{
    [SerializeField] private Slider _volume;
    [SerializeField] private List<AudioClip> _allSongsList = new List<AudioClip>();
    private List<AudioClip> _allSongs = new List<AudioClip>();
    private AudioSource _soundGame;
    private float _sound;

    private void Start()
    {
        _soundGame = GetComponent<AudioSource>();
        _sound = _soundGame.volume;

        while (_allSongsList.Count > 0)
        {
            AudioClip clip = _allSongsList[Random.Range(0, _allSongsList.Count)];
            _allSongs.Add(clip);
            _allSongsList.Remove(clip);
        }

        StartCoroutine(PlaySound(_allSongs));
        _volume.value = _sound;
    }


    private IEnumerator PlaySound(List<AudioClip> allSongs)
    {
        while (true)
        foreach (AudioClip clip in allSongs)
        {
            _soundGame.clip = clip;
            _soundGame.Play();
            yield return new WaitWhile(() => _soundGame.isPlaying);
        }
    }

    public void Mute()
    {
        if (_soundGame.volume == 0) {
            _soundGame.volume = _sound;
            return;
                }
        _soundGame.volume = 0;
    }

    public void ChangeVolume()
    {
        _soundGame.volume = _volume.value / 2;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
