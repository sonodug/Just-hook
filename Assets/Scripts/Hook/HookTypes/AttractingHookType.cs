using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractingHookType : HookEngine
{
    [SerializeField] private float _launchSpeed;
    [SerializeField] private float _lerpingTime;

    private bool _isReadyToJerk;

    private float _currentLerpTime;
    private bool _isLerping;

    private Vector2 _firePointDistance;
    private Vector2 _targetPosition;

    private void FixedUpdate()
    {
        if (_isLerping)
        {
            _currentLerpTime += Time.deltaTime;

            if (_currentLerpTime > _lerpingTime)
            {
                _currentLerpTime = _lerpingTime;
                _isLerping = false;
            }

            float percentComplete = _currentLerpTime / _lerpingTime;
            Rigidbody.MovePosition(Vector3.Lerp(HookHolder.position, _targetPosition, percentComplete));
        }
    }

    public override void Grapple()
    {
        Rigidbody.gravityScale = 0;
        Rigidbody.velocity = Vector2.zero;
    }

    protected override void MoveHookHolderAtLaunch()
    {
        _isReadyToJerk = true;

        _firePointDistance = ShotPoint.position - HookHolder.localPosition;
        _targetPosition = GrapplePoint - _firePointDistance;

        _currentLerpTime = 0f;
        _isLerping = true;


        //Vector2 firePointDistnace = ShotPoint.position - HookHolder.localPosition;
        //Vector2 targetPos = GrapplePoint - firePointDistnace;
        //HookHolder.position = Vector2.MoveTowards(HookHolder.position, targetPos, Time.deltaTime * _launchSpeed);
    }

    protected override void MoveHookHolderAfterLaunch()
    {
        _isLerping = false;

        if (_isReadyToJerk)
        {
            Rigidbody.velocity = GrappleDistanceVector;
        }

        _isReadyToJerk = false;
    }

    protected override void MoveHookHolderAfterLaunchWithEffect()
    {
        
    }

    private void Move()
    {

    }
}
