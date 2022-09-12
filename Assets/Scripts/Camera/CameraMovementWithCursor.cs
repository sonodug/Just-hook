using Entities.Player;
using UnityEngine;
using Zenject;

namespace CameraScripts
{
    public class CameraMovementWithCursor : MonoBehaviour
    {
        [Inject] private Player _player;

        [SerializeField] private float _dragSpeed;

        [SerializeField] private float _leftBound;
        [SerializeField] private float _rightBound;
        [SerializeField] private float _bottomBound;
        [SerializeField] private float _topBound;    

        private Vector3 _dragOrigin;
        private Camera _camera;

        public float CurrentLeftBound => _leftBound + _player.transform.position.x;
        public float CurrentRightBound => _rightBound + _player.transform.position.x;
        public float CurrentBottomBound => _bottomBound + _player.transform.position.y;
        public float CurrentTopBound => _topBound + _player.transform.position.y;

        private void Start()
        {
            _camera = GetComponent<Camera>();
        }

        private void Update()
        {
            ListenCameraDrag();
        }

        private void ListenCameraDrag()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _dragOrigin = _camera.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 difference = _dragOrigin - _camera.ScreenToWorldPoint(Input.mousePosition);
                //_camera.transform.position += difference;

                var position = transform.position;
                var cameraPosition = _camera.transform.position;
                
                cameraPosition = Vector3.Lerp(position, position + difference, Time.deltaTime * _dragSpeed);

                cameraPosition = 
                    new Vector3
                    (
                        Mathf.Clamp(cameraPosition.x, CurrentLeftBound, CurrentRightBound),
                        Mathf.Clamp(cameraPosition.y, CurrentBottomBound, CurrentTopBound),
                        position.z
                    );
                _camera.transform.position = cameraPosition;
            }
        }

        private void OnDrawGizmos()
        {
            if (_player != null)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(new Vector2(CurrentLeftBound, CurrentTopBound), new Vector2(CurrentRightBound, CurrentTopBound));
                Gizmos.DrawLine(new Vector2(CurrentLeftBound, CurrentBottomBound), new Vector2(CurrentRightBound, CurrentBottomBound));
                Gizmos.DrawLine(new Vector2(CurrentLeftBound, CurrentTopBound), new Vector2(CurrentLeftBound, CurrentBottomBound));
                Gizmos.DrawLine(new Vector2(CurrentRightBound, CurrentTopBound), new Vector2(CurrentRightBound, CurrentBottomBound));
            }
        }
    }
}
