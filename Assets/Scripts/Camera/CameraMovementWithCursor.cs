using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

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

            _camera.transform.position = Vector3.Lerp(transform.position, transform.position + difference, Time.deltaTime * _dragSpeed);

            _camera.transform.position = 
                new Vector3
                (
                    Mathf.Clamp(_camera.transform.position.x, CurrentLeftBound, CurrentRightBound),
                    Mathf.Clamp(_camera.transform.position.y, CurrentBottomBound, CurrentTopBound),
                    transform.position.z
                );
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
