using System.Collections.Generic;
using Entities.Player;
using Progress_Zones;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public abstract class Enemy : MonoBehaviour
{
    [Inject] protected Player Target;

    [SerializeField] protected List<TriggerZone> Triggers;
    
    protected IDamageable Health;
    protected IMovable Movement;
    protected IAttackable Attackable;

    protected abstract void InitBehaviours();
    protected abstract void OnDied();
}
