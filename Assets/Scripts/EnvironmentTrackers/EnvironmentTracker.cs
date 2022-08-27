using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnvironmentTracker : MonoBehaviour
{
    public Environment LastTarget { get; protected set; }

    public abstract bool TryTrack(RaycastHit2D hit, bool isChangeable);
}
