using System;
using System.Collections;
using UnityEngine;

public class GeminateAttack : MonoBehaviour, IAttackReload
{
    [SerializeField] private GeminateAttackSkillObjects _geminateAttackSkillObjects;

    private TowerAttack _towerAttack;

    private bool _isCoolDawn;

    private void Start()
    {
        _towerAttack = GetComponent<TowerAttack>();

        _isCoolDawn = false;

        try
        {
            _geminateAttackSkillObjects = (GeminateAttackSkillObjects)_towerAttack.Tower.firstSkill;
        }
        catch (InvalidCastException)
        {
            _geminateAttackSkillObjects = (GeminateAttackSkillObjects)_towerAttack.Tower.secondSkill;
        }


        _towerAttack.IAttackReloadSubs.Add(this);
    }

    public IEnumerator AttackReload(float attackSpeed)
    {
        if (_isCoolDawn)
        {
            float _time = TowerObjects.BASEATTACKTIME / attackSpeed;
            yield return new WaitForSeconds(_time);
            _towerAttack.AtackReadyChange(true);
            yield break;
        }
        yield return new WaitForSeconds(_geminateAttackSkillObjects.TimeSecondAttack);
        _towerAttack.AtackReadyChange(true);
        _isCoolDawn = true;
        yield return new WaitForSeconds(_geminateAttackSkillObjects.CoolDawn);
        _isCoolDawn = false;
        yield break;
    }


    private void OnDestroy()
    {
        _towerAttack.IAttackReloadSubs.Remove(this);
    }
}
