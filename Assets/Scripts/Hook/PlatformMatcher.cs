using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMatcher : MonoBehaviour
{
    public HookType Match(Platform platform, List<HookType> hookTypes)
    {
        print("kkk");
        switch (platform) //??
        {
            case AttractingPlatform attractingPlatform:
                print("atr");
                return hookTypes[0];

            case PhysicsPlatform physicsPlatform:
                print("phys");
                return hookTypes[1];

            case BouncePlatform bouncePlatform:
                return hookTypes[2];

            case TransporterPlatform transporterPlatform:
                return hookTypes[3];

            default:
                break;
        }

        return null;
    }
}
