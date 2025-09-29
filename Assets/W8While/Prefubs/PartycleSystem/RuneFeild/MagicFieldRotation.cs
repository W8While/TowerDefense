using UnityEngine;

public class MagicFieldRotation : MonoBehaviour
{
    private float _rotatinSpeed;

    private void Start()
    {
        _rotatinSpeed = 20f;
    }

    void Update()
    {
        transform.Rotate(_rotatinSpeed * Time.deltaTime * Vector3.one);
    }
}
