using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExpepeancePoint : MonoBehaviour
{
    [SerializeField] private List<AllPoints> _allPoints = new List<AllPoints>();
    static private List<AllPoints> allPoints = new List<AllPoints>();
    static public List<AllPoints> AllPoints => allPoints;

    private void Start()
    {
        allPoints = _allPoints;
    }

    static public void ChangePoint(int pointNumber, int pointCount)
    {
        for (int i = 0; i < allPoints.Count; i++)
        {
            if (i+2 == pointNumber)
            {
                allPoints[i].pointCount += pointCount;
                allPoints[i].pointText.text = allPoints[i].pointCount.ToString();
            }
        }
    }
}

[Serializable]
public class AllPoints
{
    public TMP_Text pointText;
    public int pointCount;
}
