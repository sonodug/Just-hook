using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InstantiateBulletProvider : MonoBehaviour
{
    [Inject] private DiContainer _container;

    public void Instantiate(Bullet bullet, Transform shootPoint, Player target)
    {
        Vector3 dir = target.transform.position - shootPoint.position;
        _container.InstantiatePrefab(bullet, shootPoint.position, Quaternion.LookRotation(dir.normalized), shootPoint);
    }
    
    public void Instantiate(Bullet bullet, Transform shootPoint)
    {
        _container.InstantiatePrefab(bullet, shootPoint.position, Quaternion.identity, shootPoint);
    }
}
