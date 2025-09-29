using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;


public class Skill : ScriptableObject, ISkillDescription
{ 
    [SerializeField] private Sprite _sprite;
    public Sprite sprite => _sprite;
    [SerializeField] private string _description;
    public string Description => _description;
    [SerializeField] private string _skillScript;
    public string skillScript => _skillScript;
    public bool isAvailible;
    [SerializeField] private int _cost;
    public int Cost => _cost;
    [SerializeField] private int _level;
    public int Level => _level;

    public List<ISkillDescription> ISkillDescriptionSubs = new List<ISkillDescription>();

    public string SkillDescription(TowerClickHandler towerClick)
    {
        if (ISkillDescriptionSubs.Count > 0)
        {
            foreach (var skillDescription in ISkillDescriptionSubs)
            {
                if (skillDescription.SkillDescription(towerClick) != null)
                return skillDescription.SkillDescription(towerClick);
            }
        }
        return Description;
    }

    public virtual string SkillFullDescription()
    {
        return null;
    }

    private void OnEnable()
    {
        isAvailible = false;
    }
}


public interface ISkillDescription
{
    public string SkillDescription(TowerClickHandler towerClick);
}
