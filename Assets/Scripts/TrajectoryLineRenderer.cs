using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrajectoryLineRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float _maxDistance;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private int _accuracy = 100;

    private Camera _camera;

    private RaycastHit2D _hit;

    private Vector3[] _points;

    private Vector3 _targetPosition;

    public Vector2 JointVector => _targetPosition - transform.position;

    private void Start()
    {
        _camera = Camera.main;
        _lineRenderer.enabled = false;

        //SetSegments();
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
                _lineRenderer.enabled = true;

                Color setColor = GetPlatformTypeColor(_hit);
                _lineRenderer.startColor = setColor;
                _lineRenderer.endColor = setColor;
                

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

    public void DrawSegmentedTrajectory(Vector3 startPosition, GrapplingRope grapplingRope)
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

    private void SetSegments()
    {
        _points = new Vector3[_accuracy];
        _lineRenderer.positionCount = _points.Length;
    }

    private Color GetPlatformTypeColor(RaycastHit2D hit)
    {
        if (hit.collider.TryGetComponent<AttractingPlatform>(out AttractingPlatform attractingPlatform))
        {
            return Color.blue;
        }
        else if (hit.collider.TryGetComponent<WaveringPlatform>(out WaveringPlatform waveringPlatform))
        {
            return Color.red;
        }
        else if (hit.collider.TryGetComponent<BouncePlatform>(out BouncePlatform bouncePlatform))
        {
            return Color.yellow;
        }
        else if (hit.collider.TryGetComponent<TransporterPlatform>(out TransporterPlatform transporterPlatform))
        {
            return Color.green;
        }
        else
        {
            return Color.white;
        }
    }
}
