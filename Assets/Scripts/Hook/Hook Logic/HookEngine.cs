using System;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public abstract class HookEngine : MonoBehaviour
{
    [SerializeField] protected GrapplingRope GrapplingRope;
    [SerializeField] protected Transform ShotPoint;

    [SerializeField] private HookRotator _rotator;
    [SerializeField] private LayerMask _grappableLayer;
    [SerializeField] private EnvironmentTracker _environmentTracker;

    [SerializeField] private float _maxDistance = 20;

    protected bool IsReadyToMove = false;

    private Camera _camera;
    private Player _hookHolder;

    private Transform _hookPivot;
    protected Transform HookHolder;

    protected SpringJoint2D SpringJoint2D;
    protected Rigidbody2D Rigidbody;

    protected Environment TargetEnvironment;

    public Vector2 GrapplePoint;

    public Vector2 GrappleDistanceVector { get; private set; }

    public Transform FirePoint => ShotPoint;

    private void Awake()
    {
        _hookHolder = GetComponentInParent<Player>();
    }

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

    private void OnEnable()
    {
        _hookHolder.Died += OnHookHolderDied;
    }

    private void OnDisable()
    {
        _hookHolder.Died -= OnHookHolderDied;
    }

    private void Update()
    {
        if (GrapplingRope.Affectable)
        {
            GrapplingRope.enabled = false;
            SpringJoint2D.enabled = false;

            MoveHookHolderAfterLaunchWithEffect();
            GrapplingRope.Affectable = false;
        }
        else
        {
            Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            _rotator.RotateGun(mousePos, true);
        }

        if (IsReadyToMove && GrapplingRope.IsGrappling)
        {
            MoveHookHolderAtLaunch();
        }

        TargetEnvironment = _environmentTracker.LastTarget;
    }

    private bool TrySetGrapplePoint()
    {
        Vector2 distanceVector = _camera.ScreenToWorldPoint(Input.mousePosition) - _hookPivot.position;

        if (Physics2D.Raycast(ShotPoint.position, distanceVector.normalized))
        {
            RaycastHit2D _hitLayer = Physics2D.Raycast(ShotPoint.position, distanceVector.normalized, _maxDistance, _grappableLayer);

            if (_hitLayer)
            {
                CalculateGrapplePoint(_hitLayer);
                return true;
            }
        }

        return false;
    }

    private void CalculateGrapplePoint(RaycastHit2D _hit)
    {
        GrapplePoint = _hit.point;
        GrappleDistanceVector = GrapplePoint - (Vector2)_hookPivot.position;

        GrapplingRope.enabled = true;
    }

    public void Enable()
    {
        if (GrapplingRope.IsGrappling)
        {
            GrapplingRope.enabled = false;
            SpringJoint2D.enabled = false;
            Rigidbody.gravityScale = 1;
        }

        if (TrySetGrapplePoint())
        {
            if (GrapplingRope.enabled)
            {
                _rotator.RotateGun(GrapplePoint, true);
            }
            else
            {
                Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
                _rotator.RotateGun(mousePos, false);
            }

            IsReadyToMove = true;
        }
    }

    public void Disable()
    {
        IsReadyToMove = false;
        GrapplingRope.enabled = false;
        SpringJoint2D.enabled = false;
        Rigidbody.gravityScale = 1;

        MoveHookHolderAfterLaunch();
    }

    public void DisableWithoutMoving()
    {
        IsReadyToMove = false;
        GrapplingRope.enabled = false;
        SpringJoint2D.enabled = false;
        Rigidbody.gravityScale = 1;
    }

    public void OnHookHolderDied()
    {
        DisableWithoutMoving();
        Debug.Log("disable");
    }

    public abstract void Grapple();
    protected abstract void MoveHookHolderAtLaunch();
    protected abstract void MoveHookHolderAfterLaunch();
    protected abstract void MoveHookHolderAfterLaunchWithEffect();

    private void OnDrawGizmosSelected()
    {
        if (ShotPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(ShotPoint.position, _maxDistance);
        }
    }
}
