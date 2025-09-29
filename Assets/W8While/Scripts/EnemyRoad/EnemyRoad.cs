using System.Collections.Generic;
using System;
using UnityEngine;

public enum Directionale { Top, Bot, Right};
public class EnemyRoad : MonoBehaviour
{
    static public Vector3 _startPosition = new Vector3 (-34, 0, 6);
    static public Vector3 _endPosition = new Vector3(32, 0, 12);
    static public List<Vector3> _rotationBlocksPosition = new List<Vector3>();

    private void OnEnable()
    {
        for (int i = 0; i < transform.childCount; i++)
            if (transform.GetChild(i).GetComponent<RotatinBlock>())
                _rotationBlocksPosition.Add(transform.GetChild(i).transform.position);
    }
}

