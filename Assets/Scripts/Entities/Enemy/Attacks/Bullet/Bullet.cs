using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected float Damage;
    [SerializeField] protected float Speed;
    [SerializeField] protected Vector2 Direction;
    [SerializeField] protected ParticleSystem ShotEffect;
    [SerializeField] protected ParticleSystem ExplosionEffect;

    public abstract void Move();
}
