using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected Player Target;

    protected IDamageable Health;
    protected IMovable Movement;
    protected IAttackable Attackable;

    protected abstract void InitBehaviours();
    protected abstract void OnDied();
}
