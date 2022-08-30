using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FollowerMovement))]
public class StalkingEnemy : Enemy
{
    [SerializeField] private float _health;
    [SerializeField] private float _damage;

    private FollowerMovement _movement;
    private EnemyEnvironment _enemy;
    private Health _eventHealth;

    private void Awake()
    {
        _movement = GetComponent<FollowerMovement>();
        _enemy = GetComponent<EnemyEnvironment>();

        InitBehaviours();
    }

    private void OnEnable()
    {
        _enemy.HookConnected += OnHookConnected;
        _eventHealth.Died += OnDied;
    }

    private void OnDisable()
    {
        _enemy.HookConnected -= OnHookConnected;
        _eventHealth.Died -= OnDied;
    }

    protected override void InitBehaviours()
    {
        _eventHealth = new Health(new NormalDyingPolicy(), _health);

        Health = _eventHealth;
        Attackable = new BasicNearAttack(_damage, Target);
        Movement = _movement;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            Attackable.Attack();
        }
    }

    private void OnHookConnected()
    {
        Health.ApplyDamage(Target.Damage);
    }

    protected override void OnDied()
    {
        gameObject.SetActive(false);
    }
}
