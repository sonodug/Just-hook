using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransporterPlatform : Platform
{
    public override void Accept(IPlatformVisitor visitor)
    {
        visitor.Visit(this);
    }
}
