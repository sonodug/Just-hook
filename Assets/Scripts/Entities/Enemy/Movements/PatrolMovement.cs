using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PatrolMovement : MonoBehaviour, IMovable
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;

    private Vector2[] _waypointsPosition;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _waypointsPosition = ConvertToVectorArray(_waypoints);
        _rigidbody2D = GetComponent<Rigidbody2D>();

        Move();
    }

    public void Move()
    {
        Tween tween = _rigidbody2D.DOPath(_waypointsPosition, 100.0f / _speed, PathType.Linear).SetOptions(true);

        tween.SetEase(Ease.Linear).SetLoops(-1);
    }

    private Vector2[] ConvertToVectorArray(Transform[] array)
    {
        Vector2[] vectorArray = new Vector2[array.Length];

        for (int i = 0; i < array.Length; i++)
        {
            vectorArray[i] = array[i].position;
        }

        return vectorArray;
    }
}
