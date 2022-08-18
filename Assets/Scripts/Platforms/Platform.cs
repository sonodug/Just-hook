using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Platform : MonoBehaviour
{
    public abstract void Accept(IPlatformVisitor visitor);

    protected GrapplingRope _connectedRope { get; private set; }

    public void InitializeRopeConnection(GrapplingRope grapplingRope)
    {
        print("a");
        _connectedRope = grapplingRope;
    }

    protected bool TryBreakConnection()
    {
        if (_connectedRope)
        {
            if (_connectedRope.IsGrappling)
            {
                print("b");
                _connectedRope.enabled = false;
                _connectedRope.Affectable = true;
                return true;
            }
        }

        return false;
    }
}
