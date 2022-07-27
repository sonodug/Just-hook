using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformTracker : MonoBehaviour
{
    //public enum platform_type
    //{
    //    attractingplatform_type,
    //    physicsplatform_type,
    //    bounceplatform_type,
    //    transporterplatform
    //}

    private Platform currentPlatform = null;
    private Platform tempPlatform = null;

    public event UnityAction<Platform> PlatformFocusChanged;

    public void Track(RaycastHit2D hit) //Решить проблемку
    {
        if (hit.collider.TryGetComponent<Platform>(out Platform target))
        {
            print(target.gameObject.name);

            switch (target) //??
            {
                case AttractingPlatform attractingPlatform:
                    tempPlatform = attractingPlatform;

                    break;
                case PhysicsPlatform physicsPlatform:
                    tempPlatform = physicsPlatform;

                    break;
                case BouncePlatform bouncePlatform:
                    tempPlatform = bouncePlatform;

                    break;
                case TransporterPlatform transporterPlatform:
                    tempPlatform = transporterPlatform;

                    break;

                default:
                    break;
            }
        }

        CheckFocusChange();
    }

    private void CheckFocusChange()
    {
        if (tempPlatform != null)
        {
            if (currentPlatform == null)
            {
                currentPlatform = tempPlatform;
                return;
            }
            if (!tempPlatform.Equals(currentPlatform))
            {
                print("б");
                currentPlatform = tempPlatform;

                PlatformFocusChanged?.Invoke(currentPlatform);
            }
        }
    }
}
