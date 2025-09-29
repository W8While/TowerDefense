using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyMove : MonoBehaviour
{
    private Vector3 _directional;
    private int _currentPositionIndex;
    [SerializeField] private float _baseSpeed;
    public float BaseSpeed => _baseSpeed;
    private float _speed;
    public float Speed => _speed;
    private bool _canMove;
    private bool _isStan;
    public void ChangeMove(bool what)
    {
        _canMove = what;
    }
    private float _timer;
    private float _timeBash;
    void Start()
    {
        _canMove = true;
        _isStan = false;
        _speed = _baseSpeed;
        _currentPositionIndex = 0;
        transform.position = EnemyRoad._startPosition + new Vector3(0, 1f, 0);
        _directional = EnemyRoad._rotationBlocksPosition[_currentPositionIndex];
        transform.LookAt(_directional);
    }

    private void Update()
    {
        if (_canMove)
            Move();
        else if (_isStan)
            Stan();
    }


    private void Move()
    {
        _directional.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, _directional, _speed * Time.deltaTime);
        if (transform.position == _directional)
        {
            if (_currentPositionIndex == EnemyRoad._rotationBlocksPosition.Count)
                _directional = EnemyRoad._endPosition;
            else
                _directional = EnemyRoad._rotationBlocksPosition[_currentPositionIndex++];
            _directional.y = transform.position.y;
            GetComponentInChildren<EnemyCanvas>().DirectionalChange();
            transform.LookAt(_directional);
        }
        if (transform.position == EnemyRoad._endPosition + new Vector3(0,  1, 0))
        {
            FindAnyObjectByType<FinaleGame>().EndGame();
        }
    }

    public void StopMove(float time, bool isSound = true)
    {
        if (GetComponentInChildren<EffectBash>())
            Destroy(GetComponentInChildren<EffectBash>().gameObject);
        foreach (GameObject effects in AllEffects.EffectsPrefub)
        {
            if (effects.GetComponent<EffectBash>())
            {
                GameObject bashEffect = Instantiate(effects, transform.position + effects.GetComponent<EffectBash>().yPosition * Vector3.up, Quaternion.identity);
                bashEffect.transform.SetParent(transform);
                 bashEffect.GetComponent<AudioSource>().enabled = isSound;
                break;
            }
        }
        _timer = 0;
        _timeBash = time;
        _canMove = false;
        _isStan = true;
    }

    private void Stan()
    {
        _timer += Time.deltaTime;
        if (_timer >= _timeBash)
        {
            _canMove = true;
            _isStan = false;
            Destroy(GetComponentInChildren<EffectBash>().gameObject);
        }
    }

    public void ChangeSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }

    //IEnumerator Rotate(Vector3 rotatePosition)
    //{
    //    while (transform.rotation != Quaternion.LookRotation(_directional))
    //    {
    //        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_directional), _rotatinSpeed * Time.deltaTime);
    //        yield return null;
    //    }
    //}

}
