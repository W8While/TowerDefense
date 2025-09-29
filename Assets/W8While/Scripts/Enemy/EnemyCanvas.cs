using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCanvas : MonoBehaviour
{
    private Slider _healthPointSlider;
    private EnemyStats _enemyStats;
    private TMP_Text _damageText;
    private List<GameObject> allDamage = new List<GameObject>();


    private void Start()
    {
        _enemyStats = GetComponentInParent<EnemyStats>();
        _healthPointSlider = GetComponentInChildren<Slider>();
        _damageText = GetComponentInChildren<TMP_Text>();
        _healthPointSlider.value = 0f;

        _enemyStats.GetDamage += GetDamage;
        _enemyStats.Dies += EnemyDies;
    }

    private void EnemyDies(AttackBall ball, EnemyStats enemy)
    {
        StopAllCoroutines();
    }


    private void GetDamage(float damage)
    {
        damage = (float)Math.Round(damage, 1);
        _healthPointSlider.value = 1 - _enemyStats.HealtPoint / _enemyStats.BaseHealthPoint;
        GameObject text = Instantiate(_damageText.gameObject, _damageText.transform.position, Quaternion.identity);
        allDamage.Add(text);
        text.GetComponent<TMP_Text>().text = damage.ToString();
        text.transform.SetParent(transform);
        text.transform.LookAt(Camera.main.transform);
        Vector3 rotat = text.transform.rotation.eulerAngles;
        rotat.y = 0;
        rotat.x *= -1f;
        text.transform.rotation = Quaternion.Euler(rotat);
        StartCoroutine(DamageTextMove(text));
    }


    private void OnDisable()
    {
        _enemyStats.GetDamage -= GetDamage;
        StopAllCoroutines();
    }



    private IEnumerator DamageTextMove(GameObject text)
    {
        float timer = 0;
        while (timer < 0.5f)
        {
            timer += Time.deltaTime;
            text.transform.Translate(0, 6 * Time.deltaTime, 0);
            yield return null;
        }
        allDamage.Remove(text);
        Destroy(text);
        yield break;
    }

    public void DirectionalChange()
    {
        foreach (GameObject gameObject in allDamage)
        {
            gameObject.SetActive(false);
        }
    }
}
