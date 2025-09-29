using UnityEngine;


[CreateAssetMenu(fileName = "FrostArrow4", menuName = "Skill/PassiveSkill/Drow Ranger/FrostArrow4")]
public class FrostArrows4ScriptibleObj : FrostArrowsScriptibleObject
{
    [SerializeField] private float _radiusExplosion;
    public float RadiusExplosion => _radiusExplosion;
    [SerializeField] private float _damageProcently;
    public float DamageProcently => _damageProcently;
    [SerializeField] private float _timeBeforeExplosion;
    public float TimeBeforeExplosion => _timeBeforeExplosion;
    [SerializeField] private GameObject _explosionEffect;
    public GameObject ExplosionEffect => _explosionEffect;

    public override string SkillFullDescription()
    {
        return $"Урон за стак - {DamagePerStack} \nМаксимум стаков - {MaxArrowStacks}\nДлительность стаков (обновляется при попадании) - {StackDuraction} секунд\n Также каждые {TimeBeforeExplosion} секунд враг взрываестя, нанося врагам в радиусе {RadiusExplosion} урон, равный {DamageProcently}% от нанесенного урона за время с прошедшего взрыва";
    }
}
