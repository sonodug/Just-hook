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

    private void Start()
    {
        //DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Gem>(out Gem gem))
        {
            _currentGemsCollected++;

            if (gem.enabled == true)
                LevelScoreChanged?.Invoke();
            
            gem.gameObject.SetActive(false);
            gem.enabled = false;
        }
    }
}
