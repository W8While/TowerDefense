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
            _cost.text = skill.Cost.ToString() + " демец.";
        }
        else
        {
            _cost.text = skill.Cost.ToString() + " демец. \n" + skill.Level.ToString() + "кебек онхмрнб";
        }
        _description.text = skill.SkillFullDescription();
    }
}
