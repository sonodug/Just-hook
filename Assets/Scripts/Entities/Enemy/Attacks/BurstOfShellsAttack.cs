using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstOfShellsAttack : IAttackable
{
    //Animator
    //Coroutine
    private BasicBullet _bullet;
    private Transform[] _shootPoints;

    public BurstOfShellsAttack(BasicBullet bullet, Transform[] shootPoints)
    {
        _bullet = bullet;
        _shootPoints = shootPoints;
    }

    public void Attack()
    {
        foreach (var shootPoints in _shootPoints)
        {
            InstantiateBulletProvider.Instantiate(_bullet, shootPoints);
        }
    }
}
