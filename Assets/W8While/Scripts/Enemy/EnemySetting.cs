using UnityEngine;
using UnityEngine.UI;

public class EnemySetting : MonoBehaviour
{
    [SerializeField] private Sprite _enemySprite;
    public Sprite EnemySprite => _enemySprite;
    [SerializeField] private int _id;
    public int Id => _id;
}
