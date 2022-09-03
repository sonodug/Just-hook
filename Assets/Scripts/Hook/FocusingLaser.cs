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

    private readonly EnvironmentToColorMatcherVisitor _platformVisitor = new EnvironmentToColorMatcherVisitor();

    private Camera _camera;

    private RaycastHit2D _hit;

    private Vector3[] _points;
    private Vector3 _targetPosition;

    private GrappleApplyIndicator _indicator;

    public Vector2 JointVector => _targetPosition - transform.position;

    private void Start()
    {
        _platformVisitor.Init(this);
        _camera = Camera.main;
        _lineRenderer.enabled = false;

        _platformTracker.EnvironmentFocusChanged += OnPlatformFocusChanged;

        _indicator = GetComponentInChildren<GrappleApplyIndicator>();
        _indicator.gameObject.SetActive(false);
        //SetSegments();
    }

    private void Update()
    {

    }

    public void DrawStraightTrajectory(Vector3 startPosition, bool changeable)
    {
        startPosition.z = 0;

        _targetPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        _targetPosition.z = 0;

        _hit = Physics2D.Raycast(transform.position, JointVector.normalized);

        if (_hit)
        {
            bool isGrappleEnvironment = _platformTracker.TryTrack(_hit, changeable);

            if (!isGrappleEnvironment)
            {
                SetLaserColor(Color.white);
            }

            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(0, startPosition);
            _lineRenderer.SetPosition(1, _hit.point);

            if (changeable)
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
                _indicator.gameObject.SetActive(false);
            }
        }
        else
        {
            _lineRenderer.enabled = false;
            _indicator.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        _platformTracker.EnvironmentFocusChanged -= OnPlatformFocusChanged;
    }

    private void SetSegments()
    {
        _points = new Vector3[_accuracy];
        _lineRenderer.positionCount = _points.Length;
    }

    private void OnPlatformFocusChanged(Environment environment)
    {
        environment.Accept(_platformVisitor);
    }

    public void Disable()
    {
        _lineRenderer.enabled = false;
        _indicator.gameObject.SetActive(false);
    }

    public void SetLaserColor(Color color)
    {
        if (color != Color.white)
            _indicator.gameObject.SetActive(true);

        Color setColor = color;
        _lineRenderer.startColor = setColor;
        _lineRenderer.endColor = setColor;
    }
}
