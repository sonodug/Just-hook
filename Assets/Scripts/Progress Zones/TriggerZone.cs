using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] private GameObject _triggerPass;
    [SerializeField] private GameObject _triggerObject;

    private BoxCollider2D _boxCollider;

    private void Start()
    {
        _triggerPass.SetActive(false);

        if (_triggerObject.TryGetComponent<Enemy>(out Enemy enemy))
        {

        }
    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            _triggerPass.SetActive(true);
            _boxCollider.enabled = false;
        }
    }
}
