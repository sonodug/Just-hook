using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformTracker : EnvironmentTracker
{
    public event UnityAction<Platform> PlatformFocusChanged;
    public event UnityAction<Platform> PlatformFocusChangedWithChangable;

    private Platform _lastTarget;

    public override bool TryTrack(RaycastHit2D hit, bool isChangeable)
    {
        if (hit.collider.TryGetComponent<Platform>(out Platform target))
        {
            if (isChangeable)
            {
                PlatformFocusChangedWithChangable?.Invoke(_lastTarget);
            }

            _lastTarget = target;
            PlatformFocusChanged?.Invoke(target);

            return true;
        }
        else
        {
            return false;
        }
    }
}
