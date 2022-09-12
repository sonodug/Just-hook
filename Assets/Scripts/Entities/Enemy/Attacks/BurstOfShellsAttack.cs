using UnityEngine;

namespace Entities.Enemy.Attacks
{
    public class BurstOfShellsAttack : IAttackable
    {
        //Animator
        //Coroutine
        private BasicBullet _bullet;
        private Transform[] _shootPoints;
        private InstantiateBulletProvider _provider;

        public BurstOfShellsAttack(BasicBullet bullet, Transform[] shootPoints, InstantiateBulletProvider provider)
        {
            _bullet = bullet;
            _shootPoints = shootPoints;
            _provider = provider;
        }

        public void Attack()
        {
            foreach (var shootPoint in _shootPoints)
            {
                _provider.Instantiate(_bullet, shootPoint);
            }
        }
    }
}
