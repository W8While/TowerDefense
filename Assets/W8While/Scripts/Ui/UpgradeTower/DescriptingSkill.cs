using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DescriptingSkill : MonoBehaviour
{
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private TMP_Text _description;

    public void ChangeDescription(Skill skill)
    {
        if (skill.Level == 1)
        {
            _cost.text = skill.Cost.ToString() + " �����.";
        }
        else
        {
            _cost.text = skill.Cost.ToString() + " �����. \n" + skill.Level.ToString() + "����� �������";
        }
        _description.text = skill.SkillFullDescription();
    }
}
