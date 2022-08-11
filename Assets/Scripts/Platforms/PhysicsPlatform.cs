using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsPlatform : Platform
{
    public override void Accept(IPlatformVisitor visitor)
    {
        visitor.Visit(this);
    }
}
