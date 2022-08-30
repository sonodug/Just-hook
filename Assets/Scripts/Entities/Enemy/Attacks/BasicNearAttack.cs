using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicNearAttack : IAttackable
{
    //Animator
    private float _damage;
    private Player _target;

    public BasicNearAttack(float damage, Player target)
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
