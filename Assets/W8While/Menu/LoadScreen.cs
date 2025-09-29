using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadScreen : MonoBehaviour
{

    [SerializeField] private Slider _loadingSlider;

    public void Loading(string name)
    {
        gameObject.SetActive(true);

        StartCoroutine(LoadAcync(name));
    }

    private IEnumerator LoadAcync(string name)
    {
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(name);

        while (!loadAsync.isDone)
        {
            _loadingSlider.value = loadAsync.progress;
            yield return null;
        }
    }
}
