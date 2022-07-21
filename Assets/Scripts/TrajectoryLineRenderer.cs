using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryLineRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private GrapplingRope _grapplingRope;
    [SerializeField] private GrapplingHook _grapplingHook;
    [SerializeField] private float _maxDistance = 5;

    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private Vector3 _mousePosition;
    private Vector3 _mouseDirection;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        _lineRenderer.enabled = false;
    }

    private void Update()
    {
        _startPosition = transform.root.position;
        _startPosition.z = 0;
        _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        _mouseDirection = _mousePosition - transform.root.position;
        _mouseDirection.z = 0;
        _mouseDirection = _mouseDirection.normalized;

        //Условие по рейкасту на платформу + isGrappling
        if (!_grapplingRope.enabled)
        {
            _lineRenderer.enabled = true;

            _lineRenderer.SetPosition(0, _startPosition);
            _endPosition = _mousePosition;
            _endPosition.z = 0;

            float capLength = Mathf.Clamp(Vector2.Distance(_startPosition, _endPosition), 0, _maxDistance);
            _endPosition = _startPosition + (_mouseDirection * capLength);
            _lineRenderer.SetPosition(1, _endPosition);

        }
        else
        {
            _lineRenderer.enabled = false;
        }
    }
}
