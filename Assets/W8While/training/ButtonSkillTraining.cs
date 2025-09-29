using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSkillTraining : MonoBehaviour
{
    [SerializeField] private GameObject _textPangel;
    private Vector3 _textPos;
    
    private void Start()
    {
        _textPos = new Vector3(1200, 260, 0);
        _textPangel.transform.position = _textPos;
    }
}
