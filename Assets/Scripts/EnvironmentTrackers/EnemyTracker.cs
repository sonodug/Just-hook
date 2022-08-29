using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : EnvironmentTracker
{
    public override bool TryTrack(RaycastHit2D hit, bool isChangeable)
    {
        if (hit.collider.TryGetComponent<EnemyEnvironment>(out EnemyEnvironment target))
        {


            return true;
        }
        else
        {
            return false;
        }
    }
}
