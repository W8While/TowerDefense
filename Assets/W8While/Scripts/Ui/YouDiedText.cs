using TMPro;
using UnityEngine;

public class YouDiedText : MonoBehaviour
{
    private TMP_Text image;
    private float _speed;

    private void Start()
    {
        image = GetComponent<TMP_Text>();
        _speed = 0.2f;
    }
    void Update()
    {
        Color newCol = image.color;
        newCol.a += _speed * Time.deltaTime;
        image.color = newCol;
    }
}