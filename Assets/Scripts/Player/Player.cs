using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Hook _hook;
    [SerializeField] private Transform _hookPivot;
    [SerializeField] private float _health;
    [SerializeField] private float _hookDamage;
    [SerializeField] private int _priceLevelCount;

    private void Awake()
    {
        Instantiate(_hook, _hookPivot);
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (TryGetComponent<Obstacle>(out Obstacle obstacle))
        {
            Destroy(gameObject);
        }
    }
}
