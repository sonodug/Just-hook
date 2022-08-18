using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookEngine : MonoBehaviour
{
    [SerializeField] protected float _launchSpeed;
    [SerializeField] private GrapplingRope _grapplingRope;
    [SerializeField] private HookRotator _rotator;
    [SerializeField] private LayerMask _grappableLayer;
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private float _maxDistance = 20;
    [SerializeField] private FocusingLaser _focusingLaser;

    private Camera _camera;

    private Transform _gunHolder;
    private Transform _gunPivot;

    protected SpringJoint2D _springJoint2D;
    private Rigidbody2D _rigidbody;

    public Vector2 GrapplePoint { get; private set; }

    public Vector2 GrappleDistanceVector { get; private set; }

    public Transform ShotPoint => _shotPoint;

    private void Start()
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

    }
}
