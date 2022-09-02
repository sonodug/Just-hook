using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Transform _gunPivot;

    public void RotateWeapon(Vector3 targetPoint)
    {
        Vector3 distanceVector = targetPoint - _gunPivot.position;

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;

        _gunPivot.rotation = Quaternion.Lerp(_gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * _rotationSpeed);
    }
}
