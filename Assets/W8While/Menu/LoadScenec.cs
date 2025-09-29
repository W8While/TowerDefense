using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScenec : MonoBehaviour
{
    [SerializeField] private GameObject MenuPanel;
    [SerializeField] private GameObject trainingPanel;
    [SerializeField] private Image Black;
    private AudioSource _audioSource;

    private void Start()    {

        _audioSource = GetComponent<AudioSource>();
    }
    public void StartGame()
    {
        _audioSource.Play();
        StartCoroutine(OffMenu());
    }
    IEnumerator OffMenu()
    {
        Black.gameObject.SetActive(true);
        float alpha = 0;
        while (true)
        {
            alpha += 0.3f * Time.deltaTime;
            if (alpha > 1)
                break;
            Black.color = new Color(Black.color.r, Black.color.g, Black.color.b, alpha);
            yield return null;
        }
        alpha = 1f;
        MenuPanel.SetActive(false);
        trainingPanel.SetActive(true);
        while (true)
        {
            alpha -= 0.03f * Time.deltaTime;
            if (alpha < 0)
                break;
            Black.color = new Color(Black.color.r, Black.color.g, Black.color.b, alpha);
            yield return null;
        }
        Black.gameObject.SetActive(false);
        yield break;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
