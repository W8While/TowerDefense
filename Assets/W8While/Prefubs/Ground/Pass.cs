using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Directional { Up, Down, Right };
public class Pass : MonoBehaviour
{
    private const float TOPMAXIMUMBLOCK = 19;
    private const float BOTMAXIMUMBLOCK = -9;
    private const float RIGHTMAXIMUMBLOCK = 20;
    [SerializeField] private GameObject _enemyRoadPrebuf;
    [SerializeField] private GameObject _enemyRoadRotatePrebuf;
    private Vector3 _newPosition;
    [SerializeField] private Vector3 _lastPosition;
    //[SerializeField] private TreePosition[] _prebufForest;
    private Directional _directional;
    private Directional _oldDirectional;
    private void Start()
    {
        _directional = Directional.Right;
        _oldDirectional = _directional;
        _newPosition = _lastPosition;
        
        while (_newPosition.x < RIGHTMAXIMUMBLOCK)
        {
            _lastPosition = _newPosition;
            _oldDirectional = _directional;
            int a = UnityEngine.Random.Range(1, 11);
            if (a < 7)
            {

                if (_directional == Directional.Up)
                {
                    _newPosition = new Vector3(_lastPosition.x, 0, _lastPosition.z + 2);
                    if (!CheckEndZone(_newPosition))
                    {
                        _directional = Directional.Right;
                        _newPosition = new Vector3(_lastPosition.x + 2, 0, _lastPosition.z);
                        BuildRoat(_newPosition);
                        continue;
                    }
                }
                if (_directional == Directional.Down)
                {
                    _newPosition = new Vector3(_lastPosition.x, 0, _lastPosition.z - 2);
                    if (!CheckEndZone(_newPosition))
                    {
                        _directional = Directional.Right;
                        _newPosition = new Vector3(_lastPosition.x + 2, 0, _lastPosition.z);
                        BuildRoat(_newPosition);
                        continue;
                    }
                }
                if (_directional == Directional.Right)
                    _newPosition = new Vector3(_lastPosition.x + 2, 0, _lastPosition.z);
                BuildRoat(_newPosition);
                continue;
            }
            if (_directional != Directional.Right)
            {
                _directional = Directional.Right;
                _newPosition = new Vector3(_lastPosition.x + 2, 0, _lastPosition.z);
                //if (!CheckEndZone(_newPosition))
                //{
                //    _directional = _oldDirectional;
                //    _newPosition = _lastPosition;
                //    continue;
                //}
                BuildRoat(_newPosition);
                continue;
            }
            int b = UnityEngine.Random.Range(0, 2);
            switch (b)
            {
                case 0:
                    _directional = Directional.Up;
                    _newPosition = new Vector3(_lastPosition.x, 0, _lastPosition.z + 2);
                    break;
                case 1:
                    _directional = Directional.Down;
                    _newPosition = new Vector3(_lastPosition.x, 0, _lastPosition.z - 2);
                    break;
            }
            if (!CheckEndZone(_newPosition))
            {
                _directional = _oldDirectional;
                _newPosition = _lastPosition;
                continue;
            }
            BuildRoat(_newPosition);

        }
    }

    private void BuildRoat(Vector3 position)
    {
        Quaternion quaternion = Quaternion.Euler(0, 90, 0);
        GameObject f = Instantiate(_enemyRoadPrebuf, position, quaternion);
        f.transform.SetParent(transform);
    }

    private bool CheckEndZone(Vector3 pos)
    {
        if (pos.z == TOPMAXIMUMBLOCK || pos.z == BOTMAXIMUMBLOCK)
            return false;
        else return true;
    }
}




