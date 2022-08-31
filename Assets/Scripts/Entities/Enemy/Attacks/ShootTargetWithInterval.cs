using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ShootTargetWithInterval : IAttackable
{
    //Animator
    //Coroutine
    private Bullet _bullet;
    private Transform _shootPoint;
    private InstantiateBulletProvider _provider;
    private Player _target;

    public ShootTargetWithInterval(Bullet bullet, Transform shootPoint, InstantiateBulletProvider provider, Player target)
    {
        _bullet = bullet;
        _shootPoint = shootPoint;
        _provider = provider;
        _target = target;
    }

    public void Attack()
    {
        _provider.Instantiate(_bullet, _shootPoint, _target);
    }
}
