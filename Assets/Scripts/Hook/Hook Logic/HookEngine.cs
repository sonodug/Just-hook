using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class HookEngine : MonoBehaviour
{
    [SerializeField] protected GrapplingRope GrapplingRope;
    [SerializeField] protected Transform ShotPoint;

    [SerializeField] private HookRotator _rotator;
    [SerializeField] private FocusingLaser _focusingLaser;
    [SerializeField] private LayerMask _grappableLayer;

    [SerializeField] private float _maxDistance = 20;

    private Camera _camera;

    private Transform _hookPivot;
    protected Transform HookHolder;

    protected SpringJoint2D SpringJoint2D;
    protected Rigidbody2D Rigidbody;

    public Vector2 GrapplePoint { get; private set; }

    public Vector2 GrappleDistanceVector { get; private set; }

    public Transform FirePoint => ShotPoint;


    private void Start()
    {
        _camera = Camera.main;

        _hookPivot = transform.parent.parent;
        HookHolder = transform.root;

        SpringJoint2D = GetComponentInParent<Player>().GetComponent<SpringJoint2D>();
        SpringJoint2D.autoConfigureDistance = false;

        Rigidbody = GetComponentInParent<Player>().GetComponent<Rigidbody2D>();

        GrapplingRope.enabled = false;
        SpringJoint2D.enabled = false;
    }

    private void Update()
    {
        _focusingLaser.DrawStraightTrajectory(transform.position, GrapplingRope);

        ListenInput();
    }

    private void SetGrapplePoint()
    {
        Vector2 distanceVector = _camera.ScreenToWorldPoint(Input.mousePosition) - _hookPivot.position;

        if (Physics2D.Raycast(ShotPoint.position, distanceVector.normalized))
        {
            RaycastHit2D _hitLayer = Physics2D.Raycast(ShotPoint.position, distanceVector.normalized, _maxDistance, _grappableLayer);

            if (_hitLayer)
            {
                CalculateGrapplePoint(_hitLayer);
            }
        }
    }

    private void CalculateGrapplePoint(RaycastHit2D _hit)
    {
        GrapplePoint = _hit.point;
        GrappleDistanceVector = GrapplePoint - (Vector2)_hookPivot.position;
        GrapplingRope.enabled = true;
    }

    private void ListenInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetGrapplePoint();
        }
        else if (Input.GetMouseButton(0))
        {
            if (GrapplingRope.enabled)
            {
                _rotator.RotateGun(GrapplePoint, false);
            }
            else
            {
                Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
                _rotator.RotateGun(mousePos, true);
            }

            MoveHookHolderAtLaunch();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            GrapplingRope.enabled = false;
            SpringJoint2D.enabled = false;
            Rigidbody.gravityScale = 1;

            MoveHookHolderAfterLaunch();
        }
        else
        {
            Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            _rotator.RotateGun(mousePos, true);
        }
    }

    public abstract void Grapple();

    public abstract void MoveHookHolderAtLaunch();

    public abstract void MoveHookHolderAfterLaunch();

    private void OnDrawGizmosSelected()
    {
        if (ShotPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(ShotPoint.position, _maxDistance);
        }
    }
}
