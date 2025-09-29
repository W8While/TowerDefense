using System.Collections;
using TMPro;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    private TMP_Text text;
    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }


    private void Update()
    {
        int randR = Random.Range(0, 256);
        int randG = Random.Range(0, 256);
        int randB = Random.Range(0, 256);
        Color color = new Color(randR / 255f, randG / 255f, randB / 255f);
        text.color = color;
    }

}
