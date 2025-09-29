using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticField : MonoBehaviour
{
    private MagneticFieldScriptibleObjects _magneticFieldScriptibleObjects;
    private bool _isCoolDawn;

    private TowerAttack _towerAttack;
    private TowerStats _towerStats;
    private List<TowerStats> _fieldBuffTowers = new List<TowerStats>();

    private GameObject _currentField;
    private void Update()
    {
        if (!_isCoolDawn)
        {
            CreateField();
        }
    }
    private void Start()
    {
        _towerAttack = GetComponent<TowerAttack>();
        _towerStats = GetComponent<TowerStats>();

        _isCoolDawn = false;

        try
        {
            _magneticFieldScriptibleObjects = (MagneticFieldScriptibleObjects)_towerAttack.Tower.firstSkill;
        }
        catch (InvalidCastException)
        {
            _magneticFieldScriptibleObjects = (MagneticFieldScriptibleObjects)_towerAttack.Tower.secondSkill;
        }
    }

    private void CreateField()
    {
        GameObject field = Instantiate(_magneticFieldScriptibleObjects.FieldObject, transform.position, Quaternion.identity);
        _currentField = field;
        StartCoroutine(FieldScaleChange(field, _magneticFieldScriptibleObjects.FieldRadius));
        _isCoolDawn = true;
        Invoke(nameof(CoolDawnField), _magneticFieldScriptibleObjects.CoolDawn);
        StartCoroutine(DeleteField(field, _magneticFieldScriptibleObjects.FieldDuraction));

        Collider[] towerd = Physics.OverlapSphere(transform.position, _magneticFieldScriptibleObjects.FieldRadius);
        foreach (Collider collider in towerd)
            if (collider.TryGetComponent<TowerStats>(out TowerStats tower))
            {
                _fieldBuffTowers.Add(tower);
                tower.ChangeAttackSpeed(_magneticFieldScriptibleObjects.AttackSpeedBonus);
            }
    }

    private IEnumerator DeleteField(GameObject field, float time)
    {
        yield return new WaitForSeconds(time);
        foreach (var tower in _fieldBuffTowers)
            tower.ChangeAttackSpeed(-_magneticFieldScriptibleObjects.AttackSpeedBonus);
        _fieldBuffTowers.Clear();
        while (field.transform.localScale.x > 0.2f)
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
