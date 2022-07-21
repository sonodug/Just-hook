using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DistanceJoint2D))]
public class Hook : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _bingingMask;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float _pullUpSpeed;

    private DistanceJoint2D _joint;
    private Vector3 _targetPosition;
    private Camera _camera;
    RaycastHit2D _raycastHit;

    public Vector2 JointVector => _targetPosition - transform.position;

    private void Start()
    {
        _joint = GetComponent<DistanceJoint2D>();
        _joint.enabled = false;
        _lineRenderer.enabled = false;

        _camera = Camera.main;
    }

    private void Update()
    {
        _targetPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        _targetPosition.z = 0;

        if (Input.GetMouseButton(1))
        {
            Debug.DrawRay(transform.position, JointVector, Color.cyan);
        }

        if (_joint.distance > 4.0f)
        {
            _joint.distance -= _pullUpSpeed;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _raycastHit = Physics2D.Raycast(transform.position, JointVector, _distance, _bingingMask);

            if (_raycastHit)
            {
                _joint.enabled = true;

                if (_raycastHit.collider.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidBody))
                {
                    _joint.connectedBody = rigidBody;
                }

                _joint.distance = Vector2.Distance(transform.position, _raycastHit.point);

                _lineRenderer.enabled = true;
                _lineRenderer.SetPosition(0, transform.position);
                _lineRenderer.SetPosition(1, _raycastHit.point);

            }
        }

        if (Input.GetMouseButton(0))
        {
            _lineRenderer.SetPosition(0, transform.position);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _joint.enabled = false;
            _lineRenderer.enabled = false;
        }

    }
}
