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
    [SerializeField] private float _damage;

    private Rigidbody2D _rigidbody;
    private float _maxHealth;
    private int _overallScoreAmount;
    private int _currentGemsCollected;
    private int _gemsCollectToFinish; //GameManager should initialize this and others values

    public float Damage => _damage;

    public event UnityAction LevelScoreChanged;
    public event UnityAction<float, float> HealthChanged;
    public event UnityAction<Transform> ControlPointChanged;
    public event UnityAction Died;

    private void Awake()
    {
        Instantiate(_hook, _hookPivot);

        _rigidbody = GetComponent<Rigidbody2D>();
        _currentGemsCollected = 0;
    }

    private void Start()
    {
        //DontDestroyOnLoad(gameObject);
        _maxHealth = _health;
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
        else if (collision.gameObject.TryGetComponent<ControlPoint>(out ControlPoint controlPoint))
        {
            ControlPointChanged?.Invoke(controlPoint.transform);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Obstacle>(out Obstacle obstacle))
        {
            HealthChanged?.Invoke(0, _maxHealth);
            Died?.Invoke();
        }
    }

    public void ApplyDamage(float damage)
    {
        _health -= damage;
        HealthChanged?.Invoke(_health, _maxHealth);

        _rigidbody.AddForce(new Vector2(Random.Range(-1, 1), 1) * 200);

        if (_health <= 0)
        {
            Died?.Invoke();
            _health = _maxHealth;
        }

        Debug.Log("Player get damage");
    }
}
