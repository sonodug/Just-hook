using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookRotator : MonoBehaviour
{
    [SerializeField] private bool _isRotateOverTime = true;
    [Range(0, 60)] [SerializeField] private float _rotationSpeed = 4;

    private Transform _gunPivot;

    private void Start()
    {
        _gunPivot = transform.parent.parent;
    }

    public void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        Vector3 distanceVector = lookPoint - _gunPivot.position;

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;

        if (_isRotateOverTime && allowRotationOverTime)
        {
            _gunPivot.rotation = Quaternion.Lerp(_gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * _rotationSpeed);
        }
        else
        {
            _gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
