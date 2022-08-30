using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Enemy : MonoBehaviour
{
    [Inject] protected Player Target;

    protected IDamageable Health;
    protected IMovable Movement;
    protected IAttackable Attackable;

    protected abstract void InitBehaviours();
    protected abstract void OnDied();
}
