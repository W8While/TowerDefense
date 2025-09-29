using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticField4 : MonoBehaviour
{
    private MagneticField4ScriptibleObj _magneticField4ScriptibleObjects;
    public MagneticField4ScriptibleObj MagneticField4ScriptibleObjects => _magneticField4ScriptibleObjects;
    private bool _isCoolDawn;

    private TowerAttack _towerAttack;
    private TowerStats _towerStats;
    private List<TowerStats> _fieldBuffTowers = new List<TowerStats>();

    private GameObject _currentField;
    private void Start()
    {
        _towerAttack = GetComponent<TowerAttack>();
        _towerStats = GetComponent<TowerStats>();

        _isCoolDawn = false;
        try
        {
            _magneticField4ScriptibleObjects = (MagneticField4ScriptibleObj)_towerAttack.Tower.firstSkill;
        }
        catch (InvalidCastException)
        {
            _magneticField4ScriptibleObjects = (MagneticField4ScriptibleObj)_towerAttack.Tower.secondSkill;
        }
    }


    private void Update()
    {
        if (!_isCoolDawn)
        {
            CreateField();
        }
    }


    private void CreateField()
    {
        GameObject field = Instantiate(_magneticField4ScriptibleObjects.FieldObject, transform.position, Quaternion.identity);
        _currentField = field;
        _currentField.GetComponent<FieldTrigger>().OnStart(_magneticField4ScriptibleObjects);
        StartCoroutine(FieldScaleChange(field, _magneticField4ScriptibleObjects.FieldRadius));
        _isCoolDawn = true;
        Invoke(nameof(CoolDawnField), _magneticField4ScriptibleObjects.CoolDawn);
        StartCoroutine(DeleteField(field, _magneticField4ScriptibleObjects.FieldDuraction));

        Collider[] towerd = Physics.OverlapSphere(transform.position, _magneticField4ScriptibleObjects.FieldRadius);
        foreach (Collider collider in towerd)
            if (collider.TryGetComponent<TowerStats>(out TowerStats tower))
            {
                _fieldBuffTowers.Add(tower);
                tower.ChangeAttackSpeed(_magneticField4ScriptibleObjects.AttackSpeedBonus);
            }
    }

    private IEnumerator DeleteField(GameObject field, float time)
    {
        yield return new WaitForSeconds(time);
        foreach (var tower in _fieldBuffTowers)
            tower.ChangeAttackSpeed(-_magneticField4ScriptibleObjects.AttackSpeedBonus);
        _fieldBuffTowers.Clear();
        while (field.transform.lossyScale.x > 0.2f)
        {
            field.transform.localScale -= 4 * Time.deltaTime * Vector3.one;
            yield return null;
        }
        Destroy(field);
        yield break;
    }

    private void CoolDawnField()
    {
        _isCoolDawn = false;
    }

    private IEnumerator FieldScaleChange(GameObject field, float _scale)
    {
        while (field.transform.localScale.x < _scale)
        {
            field.transform.localScale += 4 * Time.deltaTime * Vector3.one;
            yield return null;
        }
        field.transform.localScale = _scale * Vector3.one;
        field.transform.SetParent(transform);
        yield break;
    }


    private void OnDestroy()
    {
        if (_currentField != null)
            Destroy(_currentField);
    }
}
