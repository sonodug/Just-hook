using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceHookType : HookEngine
{
    [SerializeField] private float _launchSpeed;

    public override void Grapple()
    {
        Rigidbody.gravityScale = 0;
        Rigidbody.velocity = Vector2.zero;
    }

    public override void MoveHookHolderAfterLaunch()
    {
        if (GrapplingRope.IsGrappling)
        {
            Vector2 firePointDistnace = ShotPoint.position - HookHolder.localPosition;
            Vector2 targetPos = GrapplePoint - firePointDistnace;
            HookHolder.position = Vector2.MoveTowards(HookHolder.position, targetPos, Time.deltaTime * _launchSpeed);
        }
    }

    public override void MoveHookHolderAtLaunch()
    {
        Rigidbody.velocity = GrappleDistanceVector;
    }
}
