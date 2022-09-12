using Entities.Player;
using UnityEngine;
using Zenject;

namespace CameraScripts
{
    public class PlayerFollower : MonoBehaviour
    {
        [Inject] private Player _target;

        [SerializeField] private float _smoothTime = 0.3f;
        [SerializeField] private float _trackX;
        [SerializeField] private float _trackY;

        private Vector3 _velocity = Vector3.zero;

        private float _tempX;
        private float _tempY;

        private void Start()
        {
            _tempX = _trackX;
            _tempY = _trackY;
        }

        private void Update()
        {
            Vector3 targetPosition = _target.transform.TransformPoint(new Vector3(_trackX, _trackY, -10));

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
        }

        public void MoveX(float x) => _trackX += x;
        public void MoveY(float y) => _trackY += y;

        public void ResetPosition()
        {
            Debug.Log(_trackY);
            _trackX = _tempX;
            _trackY = _tempY;
            Debug.Log(_trackY);
        }
    }
}
