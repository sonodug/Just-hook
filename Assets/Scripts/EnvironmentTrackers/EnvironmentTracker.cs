using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnvironmentTracker : MonoBehaviour
{
    public abstract void Track(RaycastHit2D hit);
}
