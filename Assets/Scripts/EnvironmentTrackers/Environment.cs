using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Environment : MonoBehaviour
{
    public abstract void InitializeRopeConnection(GrapplingRope grapplingRope);

    public abstract bool TryBreakConnection();

    public abstract void Accept(IEnvironmentVisitor visitor);
}
