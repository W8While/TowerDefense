using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DefaultUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _goldText;

    private void Start()
    {
        Gold.GoldChanging += ChangeUi;
        Gold.Start();
    }

    public void ChangeUi(int gold)
    {
        _goldText.text = gold.ToString();
    }

    private void OnDisable()
    {
        Gold.GoldChanging -= ChangeUi;
    }
}
