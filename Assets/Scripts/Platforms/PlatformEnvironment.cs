using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlatformEnvironment : Environment
{
    protected GrapplingRope _connectedRope { get; private set; }

    public override void InitializeRopeConnection(GrapplingRope grapplingRope)
    {
        if (_connectedRope == null)
            _connectedRope = grapplingRope;
    }

    public override void DropRopeConnection()
    {
        _connectedRope = null;
    }

    public override bool TryBreakConnection()
    {
        if (_connectedRope)
        {
            if (_connectedRope.IsGrappling)
            {
                _connectedRope.Affectable = true;
                _connectedRope.enabled = false;
                return true;
            }
        }

        return false;
    }
}
