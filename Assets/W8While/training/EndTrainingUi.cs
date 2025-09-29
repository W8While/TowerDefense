using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndTrainingUi : MonoBehaviour
{
    [SerializeField] private GameObject _mainCanvas;
    private Image _image;
    [SerializeField] private TMP_Text _Text;
    private bool _isReturn;
    private float _speed;
    private void Start()
    {
        _mainCanvas.SetActive(false);
        _Text.gameObject.SetActive(false);
        _image = GetComponent<Image>();
        _isReturn = false;
        _speed = 0.5f;            
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (_isReturn)
            return;
        _image.color = new Color(0, 0, 0, _image.color.a + _speed * Time.deltaTime);
        if (_image.color.a >= 1)
        {
            _isReturn = true;
            _Text.gameObject.SetActive(true);
            Invoke(nameof(NumyLoad), 3f);
        }
    }
    private void NumyLoad()
    {
        AllTowers.DeleteAllTowerSkills();
        SceneManager.LoadScene("MainMenu");
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
