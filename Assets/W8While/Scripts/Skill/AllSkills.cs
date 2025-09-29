using System.Collections.Generic;
using UnityEngine;

public class AllSkills : MonoBehaviour
{
    [SerializeField] private List<Skill> _allSkills = new List<Skill>();    
    [SerializeField] private List<Skill> _firstLevelSkills = new List<Skill>();    
    
    static public List<Skill> firstLevelSkills = new List<Skill>();
    static public List<Skill> allSkills = new List<Skill>();
    static public List<Skill> useSkills = new List<Skill>();
    

    private void OnEnable()
    {
        firstLevelSkills = _firstLevelSkills;
        allSkills = _allSkills;
        useSkills = firstLevelSkills;
    }

}
