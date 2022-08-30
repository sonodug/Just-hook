using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBulletProvider : MonoBehaviour
{
    public static void Instantiate(Bullet bullet, Transform shootPoint)
    {
        Instantiate(bullet, shootPoint.position, Quaternion.identity, shootPoint);
    }
}
