using System.Collections.Generic;
using UnityEngine;

public class AllTowers : MonoBehaviour
{
    [SerializeField] private List<TowerObjects> _towers;
    static public List<TowerObjects> Towers = new List<TowerObjects>();
    static private TowerObjects _selectedTower;
    static public TowerObjects SelectedTower => _selectedTower;
    static public List<TowerAttack> AllPlaceTower = new List<TowerAttack>();
    static private List<DeleteSkillAfterEndGame> deleteSkillAfterEndGames = new List<DeleteSkillAfterEndGame>();
    static public List<AttackBall> AllAttackBall = new List<AttackBall>();

    private void OnEnable()
    {
        Towers = _towers;

    }

    private void Start()
    {
        foreach (TowerObjects tower in _towers)
        {
            AddRandomSkill(tower);
        }
    }

    private void AddRandomSkill(TowerObjects tower)
    {
        int rand = Random.Range(0, AllSkills.useSkills.Count);
        AllSkills.useSkills[rand].isAvailible = true;
        tower.firstSkill = AllSkills.useSkills[rand];
        var scriptTower = System.Type.GetType(tower.firstSkill.skillScript);
        tower.Prebuf.AddComponent(scriptTower);
        deleteSkillAfterEndGames.Add(new DeleteSkillAfterEndGame(tower, AllSkills.useSkills[rand]));
        AllSkills.useSkills.RemoveAt(rand);
    }

    static public void AddSkill(TowerObjects tower, Skill skill)
    {
        tower.secondSkill = skill; 
        var scriptTower = System.Type.GetType(tower.secondSkill.skillScript);
        tower.Prebuf.AddComponent(scriptTower);
        foreach (TowerAttack placeTower in AllPlaceTower)
            if (placeTower.Tower == tower)
                placeTower.gameObject.AddComponent(scriptTower);
        foreach (var towerSkill in deleteSkillAfterEndGames)
        {
            if (towerSkill.Tower == tower)
            {
                towerSkill.Skill2 = skill;
                return;
            }
        }
    }

    static public void UpgradeSkill(TowerObjects tower, Skill deleteSkill, Skill newSkill, int numberSkill)
    {
        foreach (DeleteSkillAfterEndGame deleteSkills in deleteSkillAfterEndGames)
            if (deleteSkills.Tower == tower)
                if (deleteSkills.Skill1 == deleteSkill)
                    deleteSkills.Skill1 = newSkill;
                else if (deleteSkills.Skill2 == deleteSkill)
                    deleteSkills.Skill2 = newSkill;
        foreach (TowerObjects towers in AllTowers.Towers)
            if (towers == tower)
            {
                if (numberSkill == 1)
                {
                    var oldComp = System.Type.GetType(tower.firstSkill.skillScript);
                    var a = tower.Prebuf.GetComponent(oldComp);
                    DestroyImmediate(a, true);
                    var newComp = System.Type.GetType(newSkill.skillScript);
                    tower.Prebuf.AddComponent(newComp);
                    tower.firstSkill = newSkill;
                    foreach (TowerAttack towerAttack in AllPlaceTower)
                        if (towerAttack.Tower == tower)
                        {
                            var destroy = towerAttack.GetComponent(oldComp);
                            Destroy(destroy);
                            towerAttack.gameObject.AddComponent(newComp);
                        }
                }
                else if (numberSkill == 2)
                {
                    var oldComp = System.Type.GetType(tower.secondSkill.skillScript);
                    var a = tower.Prebuf.GetComponent(oldComp);
                    DestroyImmediate(a, true);
                    var newComp = System.Type.GetType(newSkill.skillScript);
                    tower.Prebuf.AddComponent(newComp);
                    tower.secondSkill = newSkill;
                    foreach (TowerAttack towerAttack in AllPlaceTower)
                        if (towerAttack.Tower == tower)
                        {
                            var destroy = towerAttack.GetComponent(oldComp);
                            Destroy(destroy);
                            towerAttack.gameObject.AddComponent(newComp);
                        }
                }
            }
    }

    static public void ChangeSelectTower(TowerObjects tower)
    {
        _selectedTower = tower;
    }

    static public void DeleteAllTowerSkills()
    {
        foreach (DeleteSkillAfterEndGame deleteSkill in deleteSkillAfterEndGames)
        {
            var comp = System.Type.GetType(deleteSkill.Skill1.skillScript);
            var a = deleteSkill.Tower.Prebuf.GetComponent(comp);
            DestroyImmediate(a, true);
            if (deleteSkill.Skill2 != null)
            {
                var comp2 = System.Type.GetType(deleteSkill.Skill2.skillScript);
                var a2 = deleteSkill.Tower.Prebuf.GetComponent(comp2);
                DestroyImmediate(a2, true);
            }
        }
        foreach (TowerObjects tower in Towers)
        {
            tower.firstSkill = null;
            tower.secondSkill = null;
        }
    }
    private void OnApplicationQuit()
    {
        DeleteAllTowerSkills();
    }
}

public class DeleteSkillAfterEndGame
{
    public DeleteSkillAfterEndGame(TowerObjects _tower, Skill skill1) { Tower = _tower; Skill1 = skill1; }
    public TowerObjects Tower;
    public Skill Skill1;
    public Skill Skill2;
}
