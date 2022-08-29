using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyEnvironment : Environment
{
    protected GrapplingRope _connectedRope { get; private set; }

    public event UnityAction HookConnected;

    public override void Accept(IEnvironmentVisitor visitor)
    {
        visitor.Visit(this);
    }

    public override void DropRopeConnection()
    {
        _connectedRope = null;
    }

    public override void InitializeRopeConnection(GrapplingRope grapplingRope)
    {
        if (_connectedRope == null)
        {
            _connectedRope = grapplingRope;
            HookConnected?.Invoke();
            _connectedRope.enabled = false;
            //Attack animation
        }
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
