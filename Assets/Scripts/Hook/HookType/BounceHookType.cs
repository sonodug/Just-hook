using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceHookType : HookType
{
    [SerializeField] private float _breakForce;

    private void OnEnabled()
    {
        _springJoint2D = transform.root.gameObject.GetComponent<SpringJoint2D>();
        _springJoint2D.breakForce = _breakForce;
    }
}
