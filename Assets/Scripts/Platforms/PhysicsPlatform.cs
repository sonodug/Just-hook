using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsPlatform : PlatformEnvironment
{
    public override void Accept(IEnvironmentVisitor visitor)
    {
        visitor.Visit(this);
    }
}
