using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldArmor : IDamageable
{
    private IDamageable _damageable;
    private float _shieldHealth;

    public ShieldArmor(IDamageable damageable, float shieldHealth)
    {
        _damageable = damageable;
        _shieldHealth = shieldHealth;
    }

    public void ApplyDamage(float damage)
    {
        _shieldHealth -= damage;

        if (_shieldHealth <= 0)
            _damageable.ApplyDamage(damage);
    }
}
