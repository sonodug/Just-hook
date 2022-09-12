using Entities.Enemy.Attacks;
using Entities.Enemy.DyingPolicies;
using UnityEngine;

namespace Entities.Enemy.Enemies
{
    [RequireComponent(typeof(PatrolMovement))]
    public class PatrolEnemy : global::Enemy
    {
        [SerializeField] private float _health;
        [SerializeField] private float _damage;

        private PatrolMovement _movement;
        private EnemyEnvironment _enemy;
        private Health _eventHealth;

        private void Awake()
        {
            _movement = GetComponent<PatrolMovement>();
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
            if (collision.gameObject.TryGetComponent<Player.Player>(out Player.Player player))
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
            foreach (var trigger in Triggers)
            {
                trigger.OpenPass();
            }
            
            gameObject.SetActive(false);
        }
    }
}
