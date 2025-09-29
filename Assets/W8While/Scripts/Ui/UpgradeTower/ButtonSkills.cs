using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonSkills : MonoBehaviour,  IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private DescriptingSkill DescpiptionPanel;
    [SerializeField] private bool _isLeftPanel;
    [SerializeField] private SelectSkillUi _selectSkillUi;
    [SerializeField] private Skill _skill;
    [SerializeField] private GameObject _lastSkillObj;
    private Skill _lastSkill;
    public Skill skill => _skill;

    static public Skill selectedSkill;
    private void Start()
    {
        if (_lastSkillObj != null)
        _lastSkill = _lastSkillObj.GetComponent<ButtonSkills>().skill;
        ChangeCanvas();
    }

    public void ChangeCanvas()
    {
        if (_skill != null)
        {
            GetComponent<Image>().sprite = _skill.sprite;

            if (!_skill.isAvailible)
            {
                GetComponentInChildren<Lock>(true).gameObject.SetActive(true);
            }
            else
            {
                GetComponentInChildren<Lock>(true).gameObject.SetActive(false);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_skill.isAvailible)
            return;
        if (_skill.Level == 1)
        {
            if (Gold.CurrentGold >= skill.Cost)
            {
                Gold.Buy(skill.Cost);
                _selectSkillUi.gameObject.SetActive(true);
                selectedSkill = _skill;
                _skill.isAvailible = true;
                GetComponentInParent<TowerUpgrade>().gameObject.SetActive(false);
            }
            return;
        }
        if (_lastSkill.isAvailible && ExpepeancePoint.AllPoints[_skill.Level-2].pointCount >= 1 && Gold.CurrentGold >= skill.Cost)
        {
            Gold.Buy(skill.Cost);
            ExpepeancePoint.ChangePoint(_skill.Level, -1);
            foreach (TowerObjects towers in AllTowers.Towers)
            {
                if (towers.firstSkill == _lastSkill)
                    AllTowers.UpgradeSkill(towers, _lastSkill, skill, 1);
                if (towers.secondSkill == _lastSkill)
                    AllTowers.UpgradeSkill(towers, _lastSkill, skill, 2);
            }
            GetComponentInParent<TowerUpgrade>().gameObject.SetActive(false);
            _skill.isAvailible = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DescpiptionPanel.gameObject.SetActive(true);
        if (!_isLeftPanel)
            DescpiptionPanel.transform.position = new Vector2(transform.position.x - 150f, transform.position.y+ 100f);
        else
            DescpiptionPanel.transform.position = new Vector2(transform.position.x + 620f, transform.position.y + 100f);
        DescpiptionPanel.ChangeDescription(_skill);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DescpiptionPanel.gameObject.SetActive(false);
    }
}
