using System.Collections.Generic;
using UnityEngine;

public class AllEffects : MonoBehaviour
{
    [SerializeField] private List<GameObject> _effectsPrefub = new List<GameObject>();
    static public List<GameObject> EffectsPrefub = new List<GameObject>();

    private void Start()
    {
        EffectsPrefub = _effectsPrefub;
    }
}
