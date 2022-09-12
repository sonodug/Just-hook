using Entities.Enemy.Attacks;
using Entities.Enemy.DyingPolicies;
using UnityEngine;

namespace Entities.Enemy.Enemies
{
    [RequireComponent(typeof(InstantiateBulletProvider))]
    public class StaticGroundTurret : global::Enemy
    {
        [SerializeField] private BasicBullet _bulletTemplate;
        [SerializeField] private Transform[] _shootPoints;
        [SerializeField] private float _health;
        [SerializeField] private float _observeRadius;
        [SerializeField] private float _delayBetweenShots;

        private EnemyEnvironment _enemy;
        private Health _eventHealth;
        private float _delay;
        private InstantiateBulletProvider _provider;

        private void Awake()
        {
            _enemy = GetComponent<EnemyEnvironment>();
            _provider = GetComponent<InstantiateBulletProvider>();

            InitBehaviours();
        }

        private void Update()
        {
            _delay += Time.deltaTime;

            if (Vector3.Distance(transform.position, Target.gameObject.transform.position) < _observeRadius)
            {
                if (_delay >= _delayBetweenShots)
                {
                    Attackable.Attack();
                    _delay = 0;
                }
            }
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
            Attackable = new BurstOfShellsAttack(_bulletTemplate, _shootPoints, _provider);
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

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _observeRadius);
        }
    }
}
