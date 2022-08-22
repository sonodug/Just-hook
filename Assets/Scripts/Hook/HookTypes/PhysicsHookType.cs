using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsHookType : HookEngine
{
    [SerializeField] private float _frequency;
    [SerializeField] private float _distance;

    public override void Grapple()
    {
        SpringJoint2D.connectedAnchor = GrapplePoint;

        Vector2 distanceVector = ShotPoint.position - HookHolder.position;

        //SpringJoint2D.distance = distanceVector.magnitude;
        SpringJoint2D.frequency = _frequency;
        SpringJoint2D.distance = _distance;
        SpringJoint2D.enabled = true;
    }

    protected override void MoveHookHolderAfterLaunch() {}

    protected override void MoveHookHolderAfterLaunchWithEffect() {}

    protected override void MoveHookHolderAtLaunch() {}
}
