using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List<GrapplingHook> _hooks;
    [SerializeField] private Transform _hookPivot;
    [SerializeField] private float _health;
    [SerializeField] private float _hookDamage;
    [SerializeField] private int _itemCount;

    private PlatformTracker _tracker;
    private GrapplingHook _currentHook;

    private void Awake()
    {
        _tracker = GetComponent<PlatformTracker>();
        _currentHook = _hooks[0];
        Instantiate(_currentHook, _hookPivot);
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
