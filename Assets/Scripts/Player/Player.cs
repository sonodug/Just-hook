using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GrapplingHook _hook;
    [SerializeField] private Transform _hookPivot;
    [SerializeField] private float _health;
    [SerializeField] private float _hookDamage;
    [SerializeField] private int _itemCount;

    private void Awake()
    {
        Instantiate(_hook, _hookPivot);
    }
}
