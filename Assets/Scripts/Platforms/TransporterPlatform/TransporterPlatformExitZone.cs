using Entities.Player;
using UnityEngine;

public class TransporterPlatformExitZone : MonoBehaviour
{
    private TransporterPlatform _transporterPlatform;

    private void Start()
    {
        _transporterPlatform = GetComponentInParent<TransporterPlatform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            _transporterPlatform.TryBreakConnection();
        }
    }
}
