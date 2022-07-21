using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTracker : MonoBehaviour
{
    public enum PlatformType
    {
        AttractingPlatform_Type,
        WaveringPlatform_Type,
        BouncePlatform_Type,
        TransporterPlatform_Type,
        None
    }

    public PlatformType Track(RaycastHit2D hit)
    {
        if (hit.collider.TryGetComponent<AttractingPlatform>(out AttractingPlatform attractingPlatform))
        {
            return PlatformType.AttractingPlatform_Type;
        }
        else if (hit.collider.TryGetComponent<WaveringPlatform>(out WaveringPlatform waveringPlatform))
        {
            return PlatformType.WaveringPlatform_Type;
        }
        else if (hit.collider.TryGetComponent<BouncePlatform>(out BouncePlatform bouncePlatform))
        {
            return PlatformType.BouncePlatform_Type;
        }
        else if (hit.collider.TryGetComponent<TransporterPlatform>(out TransporterPlatform transporterPlatform))
        {
            return PlatformType.TransporterPlatform_Type;
        }
        else
        {
            return PlatformType.None;
        }
    }
}
