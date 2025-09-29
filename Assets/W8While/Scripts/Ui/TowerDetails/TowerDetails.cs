using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerDetails : MonoBehaviour
{
    private TowerObjects _tower;
    public TowerObjects Tower => _tower;
    [SerializeField] private Image _towerSprite;
    [SerializeField] private Image _firstSkillImage;
    [SerializeField] private TMP_Text _firstSkillDescription;
    [SerializeField] private Image _secondSkillImage;
    public Image SecondSkillImage => _secondSkillImage;
    [SerializeField] private TMP_Text _secondSkillDescription;
    [SerializeField] private TMP_Text _towerDescription;



    public void ChangeDetails(TowerObjects tower)
    {
        _tower = tower;
        _towerSprite.sprite = tower.Sprite;
        _firstSkillImage.sprite = tower.firstSkill.sprite;
        _firstSkillDescription.text = tower.firstSkill.Description;
        _towerDescription.text = tower.GetDescription();
        if (tower.secondSkill == null)
        {
            _secondSkillImage.sprite = null;
            _secondSkillDescription.text = null;
            return;
        }
        if (!gameObject.activeSelf)
            return;
        _secondSkillImage.sprite = tower.secondSkill.sprite;
        _secondSkillDescription.text = tower.secondSkill.Description;
    }

}
