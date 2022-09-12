using UnityEngine;

namespace Entities.Enemy.Attacks
{
    public class BasicNearAttack : IAttackable
    {
        //Animator
        private float _damage;
        private Player.Player _target;

        public BasicNearAttack(float damage, Player.Player target)
        {
            _damage = damage;
            _target = target;
        }

        public void Attack()
        {
            //Attack animation
            Debug.Log("Enemy attacks");
            _target.ApplyDamage(_damage);
            //via Visitor
        }
    }
}
