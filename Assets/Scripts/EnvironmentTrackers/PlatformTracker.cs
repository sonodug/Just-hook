using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformTracker : EnvironmentTracker
{
    public event UnityAction<Platform> PlatformFocusChanged;
    public event UnityAction<Platform> PlatformFocusChangedWithChangable;

    public Platform LastTarget { get; private set; }

    public override bool TryTrack(RaycastHit2D hit, bool isChangeable)
    {
        if (hit.collider.TryGetComponent<Platform>(out Platform target))
        {
            if (isChangeable)
            {
                PlatformFocusChangedWithChangable?.Invoke(LastTarget);
            }

            LastTarget = target;
            PlatformFocusChanged?.Invoke(target);

            return true;
        }
        else
        {
            return false;
        }
    }
}
