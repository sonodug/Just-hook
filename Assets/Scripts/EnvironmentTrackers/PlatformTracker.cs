using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformTracker : EnvironmentTracker
{
    public event UnityAction<Platform> PlatformFocusChanged;

    public override void Track(RaycastHit2D hit)
    {
        if (hit.collider.TryGetComponent<Platform>(out Platform target))
        {
            PlatformFocusChanged?.Invoke(target);
        }
    }
}
