using UnityEngine;

public class ActiveSkill : Skill
{
    [SerializeField] private float _coolDawn;
    public float CoolDawn => _coolDawn;
}
