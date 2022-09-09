using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InstantiateBulletProvider : MonoBehaviour
{
    [Inject] private DiContainer _container;

    public void Instantiate(Bullet bullet, Transform shootPoint, Player target)
    {
        _container.InstantiatePrefab(bullet, shootPoint.position, target.gameObject.transform.rotation, shootPoint);
    }
    
    public void Instantiate(Bullet bullet, Transform shootPoint)
    {
        _container.InstantiatePrefab(bullet, shootPoint.position, Quaternion.identity, shootPoint);
    }
}
