using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private float _smoothTime = 0.3f;

    private Vector3 _velocity = Vector3.zero;

    private void Update()
    {
        Vector3 targetPosition = _target.transform.TransformPoint(new Vector3(0, 0, -10));

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
    }
}
