using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractingPlatform : PlatformEnvironment
{
    public override void Accept(IEnvironmentVisitor visitor)
    {
        visitor.Visit(this);
    }
}
