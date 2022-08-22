using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransporterHookType : HookEngine
{
    [SerializeField] private float _launchSpeed;
    [SerializeField] private float _lerpingTime;
    [SerializeField] private float _transportSpeed;

    private float _currentLerpTime;
    private bool _isLerping;

    private Vector2 _firePointDistance;
    private Vector2 _targetPosition;

    private TransporterPlatform _transporterPlatform;
    private Transform _targetPoint;

    private bool _isReadyToJerk;
    private bool _isReadyToTransport = false;

    public override void Grapple()
    {
        if (TargetPlatform.TryGetComponent<TransporterPlatform>(out TransporterPlatform platform))
        {
            _transporterPlatform = platform;
        }

        Rigidbody.gravityScale = 0;
        Rigidbody.velocity = Vector2.zero;
    }

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

            if (!_isReadyToTransport)
            {
                Rigidbody.MovePosition(Vector3.Lerp(HookHolder.position, _targetPosition, percentComplete));
            }
        }
    }

    protected override void MoveHookHolderAtLaunch()
    {
        _isReadyToJerk = true;

        _firePointDistance = ShotPoint.position - HookHolder.localPosition;
        _targetPosition = GrapplePoint - _firePointDistance;

        _currentLerpTime = 0f;
        _isLerping = true;

        Debug.Log($"{HookHolder.position}, {_targetPosition}");

        if (new Vector2(Convert.ToInt32(HookHolder.position.x), Convert.ToInt32(HookHolder.position.y)) == new Vector2(Convert.ToInt32(_targetPosition.x), Convert.ToInt32(_targetPosition.y))) //candidate to fix
        {
            _isReadyToTransport = true;
            Rigidbody.gravityScale = 0;
            _targetPoint = _transporterPlatform.TargetPoint;
        }

        if (_isReadyToTransport)
        {
            GrapplePoint = new Vector2(HookHolder.position.x, GrapplePoint.y);
            HookHolder.position = Vector2.MoveTowards(HookHolder.position, _targetPoint.position, Time.deltaTime * _transportSpeed);
        }
    }

    protected override void MoveHookHolderAfterLaunch()
    {
        _isReadyToJerk = false;
        _isReadyToTransport = false;
    }

    protected override void MoveHookHolderAfterLaunchWithEffect()
    {
        if (_isReadyToJerk)
        {
            Rigidbody.velocity = -GrappleDistanceVector;
        }

        Rigidbody.gravityScale = 1;
        _isReadyToJerk = false;
        _isReadyToTransport = false;
    }
}
