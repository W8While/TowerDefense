using UnityEngine;

public class ImageMove : MonoBehaviour
{
    private float _speed;
    private float xDirectional;
    private float yDirectional;
    private void Start()
    {
        _speed = 70f;
        xDirectional = yDirectional = 1f;
    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (xDirectional == 1 && transform.position.x >= Screen.currentResolution.width)
            xDirectional = -1f;
        if (xDirectional == -1 && transform.position.x <= 0)
            xDirectional = 1f;
        if (yDirectional == 1 && transform.position.y >= Screen.currentResolution.height)
            yDirectional = -1f;
        if (yDirectional == -1 && transform.position.y <= 0)
            yDirectional = 1f;
        Vector2 newPos = transform.position;
        newPos.x += xDirectional * _speed * Time.deltaTime;
        newPos.y += yDirectional * _speed * Time.deltaTime;
        transform.position = newPos;
    }
}
