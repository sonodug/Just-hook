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
        if (hit.collider.TryGetComponent<Environment>(out Environment environment))
        {
            if (isChangeable)
            {
                EnvironmentFocusChangedWithChangable?.Invoke(LastTarget);
            }

            LastTarget = environment;
            EnvironmentFocusChanged?.Invoke(environment);

            return true;
        }
        else
            return false;
    }
}
