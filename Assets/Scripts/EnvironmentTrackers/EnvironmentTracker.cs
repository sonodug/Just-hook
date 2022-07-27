using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnvironmentTracker
{
    public abstract void Track(RaycastHit2D hit);
}
