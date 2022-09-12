using CameraScripts;
using Entities.Player;
using UnityEngine;
using Zenject;

namespace Progress_Zones
{
    public class CameraScalerZone : MonoBehaviour
    {
        [Inject] private Camera camera;
        [Inject] private Player player;

        [SerializeField] private float _observeRadius;
        [SerializeField] private float _moveX;
        [SerializeField] private float _moveY;

        private PlayerFollower _playerFollower;
        private bool _flag = true;
    
        private void Start()
        {
            _playerFollower = camera.GetComponent<PlayerFollower>();
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, player.gameObject.transform.position) < _observeRadius)
            {
                Move();
                _flag = false;
            }
            else
            {
                Reset();
                _flag = true;
            }
        }

        private void Move()
        {
            if (!_flag) return;
            _playerFollower.MoveX(_moveX);
            _playerFollower.MoveY(_moveY);
        }

        private void Reset()
        {
            if (_flag) return;
            _playerFollower.ResetPosition();
        }
    
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _observeRadius);
        }
    }
}
