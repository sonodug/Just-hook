using System;
using UnityEngine;
using UnityEngine.Events;

public class HookController : MonoBehaviour
{
    [SerializeField] protected Launch_Type LaunchType;
    [SerializeField] protected float _launchSpeed;

    [Header("Scripts Reference:")]
    [SerializeField] private GrapplingRope _grapplingRope;

    [SerializeField] private HookRotator _rotator;

    [Header("Layers Settings:")]
    [SerializeField] private LayerMask _grappableLayer;

    [SerializeField] private Transform _shotPoint;

    [SerializeField] private float _maxDistance = 20;
    [SerializeField] private FocusingLaser _focusingLaser;

    [Header("Launching:")]
    [SerializeField] private bool _isLaunchToPoint = true; //hardcode

    [Header("No Launch To Point")]
    [SerializeField] private bool _autoConfigureDistance = false;
    [SerializeField] private float _targetDistance = 3;
    [SerializeField] private float _targetFrequncy = 1;

    protected enum Launch_Type
    {
        Transform_Launch,
        Physics_Launch
    }

    private Camera _camera;

    private Transform _gunHolder;
    private Transform _gunPivot;

    private bool _isReadyToJerk = false;

    protected SpringJoint2D _springJoint2D;
    private Rigidbody2D _rigidbody;

    public Vector2 GrapplePoint { get; private set; }

    public Vector2 GrappleDistanceVector { get; private set; }

    public Transform ShotPoint => _shotPoint;


    protected virtual void Start()
    {
        _camera = Camera.main;

        _gunHolder = transform.root;
        _gunPivot = transform.parent.parent;

        _springJoint2D = GetComponentInParent<Player>().GetComponent<SpringJoint2D>();
        _rigidbody = GetComponentInParent<Player>().GetComponent<Rigidbody2D>();

        _grapplingRope.enabled = false;
        _springJoint2D.enabled = false;
    }

    private void Update()
    {
        _focusingLaser.DrawStraightTrajectory(transform.position, _grapplingRope);

        if (Input.GetMouseButtonDown(0))
        {
            SetGrapplePoint();
        }
        else if (Input.GetMouseButton(0))
        {
            if (_grapplingRope.enabled)
            {
                _rotator.RotateGun(GrapplePoint, false);
            }
            else
            {
                Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
                _rotator.RotateGun(mousePos, true);
            }

            //related to attracting hook --------------------------------------------------------------!!!!!!!!!!
            if (_isLaunchToPoint && _grapplingRope.IsGrappling)
            {
                _isReadyToJerk = true;

                if (LaunchType == Launch_Type.Transform_Launch)
                {
                    Vector2 firePointDistnace = _shotPoint.position - _gunHolder.localPosition;
                    Vector2 targetPos = GrapplePoint - firePointDistnace;
                    _gunHolder.position = Vector2.MoveTowards(_gunHolder.position, targetPos, Time.deltaTime * _launchSpeed);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _grapplingRope.enabled = false;
            _springJoint2D.enabled = false;
            _rigidbody.gravityScale = 1;

            if (_isReadyToJerk)
            {
                switch (LaunchType)
                {
                    case Launch_Type.Transform_Launch:
                        _rigidbody.velocity = GrappleDistanceVector;
                        break;
                }
            }

            _isReadyToJerk = false;
        }
        else
        {
            Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            _rotator.RotateGun(mousePos, true);
        }
    }

    private void SetGrapplePoint()
    {
        Vector2 distanceVector = _camera.ScreenToWorldPoint(Input.mousePosition) - _gunPivot.position;

        if (Physics2D.Raycast(_shotPoint.position, distanceVector.normalized))
        {
            RaycastHit2D _hitLayer = Physics2D.Raycast(_shotPoint.position, distanceVector.normalized, _maxDistance, _grappableLayer);

            if (_hitLayer)
            {
                CalculateGrapplePoint(_hitLayer);
            }
        }
    }

    private void CalculateGrapplePoint(RaycastHit2D _hit)
    {
        GrapplePoint = _hit.point;
        GrappleDistanceVector = GrapplePoint - (Vector2)_gunPivot.position;
        _grapplingRope.enabled = true;
    }

    public void Grapple() //template method actually
    {
        _springJoint2D.autoConfigureDistance = false;

        if (!_isLaunchToPoint && !_autoConfigureDistance)
        {
            _springJoint2D.distance = _targetDistance;
            _springJoint2D.frequency = _targetFrequncy;
        }

        if (!_isLaunchToPoint)
        {
            if (_autoConfigureDistance)
            {
                _springJoint2D.autoConfigureDistance = true;
                _springJoint2D.frequency = 0;
            }

            _springJoint2D.connectedAnchor = GrapplePoint;
            _springJoint2D.enabled = true;
        }
        else
        {
            switch (LaunchType) //override via template method
            {
                case Launch_Type.Physics_Launch:
                    _springJoint2D.connectedAnchor = GrapplePoint;

                    Vector2 distanceVector = _shotPoint.position - _gunHolder.position;

                    _springJoint2D.distance = distanceVector.magnitude;
                    _springJoint2D.frequency = _launchSpeed;
                    _springJoint2D.enabled = true;
                    break;

                case Launch_Type.Transform_Launch:
                    _rigidbody.gravityScale = 0;
                    _rigidbody.velocity = Vector2.zero;
                    break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_shotPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_shotPoint.position, _maxDistance);
        }
    }

    public void BreakConnection()
    {
        _isLaunchToPoint = true;
    }
}
