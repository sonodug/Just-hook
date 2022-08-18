using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FocusingLaser : MonoBehaviour
{
    [SerializeField] private PlatformTracker _platformTracker;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float _maxDistance;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private int _accuracy = 100;

    private readonly PlatformToColorMatcherVisitor _platformVisitor = new PlatformToColorMatcherVisitor();

    private Camera _camera;

    private RaycastHit2D _hit;

    private Vector3[] _points;
    private Vector3 _targetPosition;

    public Vector2 JointVector => _targetPosition - transform.position;

    private void Start()
    {
        _platformVisitor.Init(this);
        _camera = Camera.main;
        _lineRenderer.enabled = false;

        _platformTracker.PlatformFocusChanged += OnPlatformFocusChanged;
        //SetSegments();
    }

    private void Update()
    {
        
    }

    public void DrawStraightTrajectory(Vector3 startPosition, GrapplingRope grapplingRope)
    {
        startPosition.z = 0;

        _targetPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        _targetPosition.z = 0;

        //Условие по рейкасту на платформу + isGrappling
        if (!grapplingRope.enabled)
        {
            _hit = Physics2D.Raycast(transform.position, JointVector.normalized);

            if (_hit)
            {
                bool isPlatform = _platformTracker.TryTrack(_hit);

                if (!isPlatform)
                {
                    SetLaserColor(Color.white);
                }

                _lineRenderer.enabled = true;

                //Accept

                _lineRenderer.SetPosition(0, startPosition);

                _lineRenderer.SetPosition(1, _hit.point);
            }
            else
            {
                _lineRenderer.enabled = false;
            }
        }
        else
        {
            _lineRenderer.enabled = false;
        }
    }

    public void DrawSegmentedTrajectory(Vector3 startPosition, GrapplingRope grapplingRope) //TRACk 
    {
        Vector3 targetPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = 0;

        if (!grapplingRope.enabled)
        {
            _hit = Physics2D.Raycast(transform.position, JointVector.normalized);
            Vector3 speed = (_hit.point - (Vector2)startPosition) * 100;

            if (_hit)
            {
                _lineRenderer.enabled = true;

                for (int i = 0; i < _points.Length; i++)
                {
                    float time = i * 0.1f;

                    _points[i] = startPosition + speed * time;
                    _points[i].z = 0;
                }

                _lineRenderer.SetPositions(_points);
            }
            else
            {
                _lineRenderer.enabled = false;
            }
        }
        else
        {
            _lineRenderer.enabled = false;
        }
    }

    private void OnDisable()
    {
        _platformTracker.PlatformFocusChanged -= OnPlatformFocusChanged;
    }

    private void SetSegments()
    {
        _points = new Vector3[_accuracy];
        _lineRenderer.positionCount = _points.Length;
    }

    private void OnPlatformFocusChanged(Platform platform)
    {
        platform.Accept(_platformVisitor);
    }

    public void SetLaserColor(Color color)
    {
        Color setColor = color;
        _lineRenderer.startColor = setColor;
        _lineRenderer.endColor = setColor;
    }
}
