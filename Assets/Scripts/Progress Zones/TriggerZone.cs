using System;
using Entities.Player;
using UnityEngine;

namespace Progress_Zones
{
    //zone affected barriers)
    public class TriggerZone : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _optionallyTriggerColider;

        private void Start()
        {
            if (_optionallyTriggerColider != null)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            _optionallyTriggerColider.enabled = false;
            LockPass();
        }

        public void OpenPass()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        private void LockPass()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
