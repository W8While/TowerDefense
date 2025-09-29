using UnityEngine;

public class Pass2 : MonoBehaviour
{
    [SerializeField] private GameObject _prebuf;
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Instantiate(_prebuf, transform.GetChild(i).position, Quaternion.identity);
        }
    }
}
