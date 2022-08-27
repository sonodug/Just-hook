using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformTracker : EnvironmentTracker
{
    public event UnityAction<Environment> EnvironmentFocusChanged;
    public event UnityAction<Environment> EnvironmentFocusChangedWithChangable;

    public override bool TryTrack(RaycastHit2D hit, bool isChangeable)
    {
        if (hit.collider.TryGetComponent<Platform>(out Platform target))
        {
            if (isChangeable)
            {
                EnvironmentFocusChangedWithChangable?.Invoke(LastTarget);
            }

            LastTarget = target;
            EnvironmentFocusChanged?.Invoke(target);

            return true;
        }
        else
            return false;
    }
}
