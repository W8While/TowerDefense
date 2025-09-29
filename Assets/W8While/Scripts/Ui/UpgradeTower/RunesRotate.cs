using UnityEngine;

public class RunesRotate : MonoBehaviour
{
    private float _rotateSpeed;

    private void Start()
    {
        _rotateSpeed = Random.Range(-10, 10);
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }
}
