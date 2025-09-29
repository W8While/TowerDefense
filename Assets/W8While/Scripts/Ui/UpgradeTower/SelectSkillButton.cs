using UnityEngine;
using UnityEngine.EventSystems;

public class SelectSkillButton : MonoBehaviour, IPointerClickHandler
{
    private Skill skill;
    [SerializeField] private TowerDetails _towerDetails;
    [SerializeField] private SelectSkillUi _selectSkillUi;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_towerDetails.Tower.secondSkill == null)
        {
            skill = ButtonSkills.selectedSkill;
            ButtonSkills.selectedSkill = null;
            AllTowers.AddSkill(_towerDetails.Tower, skill);
            _towerDetails.ChangeDetails(_towerDetails.Tower);
            skill.isAvailible = true;
            _selectSkillUi.gameObject.SetActive(false);
        }
    }
}
