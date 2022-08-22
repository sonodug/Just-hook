using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private Hook _hook;
    [SerializeField] private Transform _hookPivot;
    [SerializeField] private float _health;
    [SerializeField] private float _hookDamage;

    private Rigidbody2D _rigidbody;
    private int _overallScoreAmount;
    private int _currentGemsCollected;
    private int _gemsCollectToFinish; //GameManager should initialize this and others values

    public event UnityAction LevelScoreChanged;

    private void Awake()
    {
        Instantiate(_hook, _hookPivot);
        _rigidbody = GetComponent<Rigidbody2D>();
        _currentGemsCollected = 0;
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Obstacle>(out Obstacle obstacle))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.TryGetComponent<Gem>(out Gem gem))
        {
            _currentGemsCollected++;
            LevelScoreChanged?.Invoke();
            gem.gameObject.SetActive(false);
        }
    }
}
