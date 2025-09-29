using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class YouDiedPanel : MonoBehaviour
{
    private Image image;
    private float _speed;

    private void Start()
    {
        image = GetComponent<Image>();
        _speed = 0.2f;
        Invoke(nameof(LoadMenu), 7f);
    }
    void Update()
    {
        Color newCol = image.color;
        newCol.a += _speed * Time.deltaTime;
        image.color = newCol;
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
