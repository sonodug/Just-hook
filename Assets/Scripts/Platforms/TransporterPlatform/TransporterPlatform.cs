using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransporterPlatform : Platform
{
    [SerializeField] private Transform _targetPoint;

    public Transform TargetPoint => _targetPoint;

    public override void Accept(IPlatformVisitor visitor)
    {
        visitor.Visit(this);
    }
}
