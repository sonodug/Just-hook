using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractingHookType : HookEngine
{
    [SerializeField] private float _launchSpeed;

    private bool _isReadyToJerk;

    public override void Grapple()
    {
        Rigidbody.gravityScale = 0;
        Rigidbody.velocity = Vector2.zero;
    }

    public override void MoveHookHolderAtLaunch()
    {
        if (GrapplingRope.IsGrappling)
        {
            _isReadyToJerk = true;

            Vector2 firePointDistnace = ShotPoint.position - HookHolder.localPosition;
            Vector2 targetPos = GrapplePoint - firePointDistnace;
            HookHolder.position = Vector2.MoveTowards(HookHolder.position, targetPos, Time.deltaTime * _launchSpeed);
        }
    }

    public override void MoveHookHolderAfterLaunch()
    {
        if (_isReadyToJerk)
        {
            Rigidbody.velocity = GrappleDistanceVector;
        }

        _isReadyToJerk = false;
    }
}
