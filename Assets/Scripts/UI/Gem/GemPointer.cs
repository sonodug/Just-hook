using System.Collections;
using System.Collections.Generic;
using Entities.Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GemPointer : MonoBehaviour
{
    [Inject] private Player _player;

    [SerializeField] private Gem _target;

    [SerializeField] private Sprite _arrowSprite;
    [SerializeField] private float _borderSize = 100f;

    private Vector3 _targetPosition;
    private Camera _mainCamera;

    private RectTransform _pointerRectTransform;
    private Image _pointerImage;

    private void Awake()
    {
        _pointerRectTransform = GetComponent<RectTransform>();
        _pointerImage = GetComponent<Image>();

        Show(_target);
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (_target.gameObject.activeSelf == false)
        {
            Hide();
        }

        Vector3 targetPositionScreenPoint = _mainCamera.WorldToScreenPoint(_targetPosition);
        bool isOffScreen = targetPositionScreenPoint.x <= _borderSize || targetPositionScreenPoint.x  >= Screen.width - _borderSize || targetPositionScreenPoint.y <= _borderSize || targetPositionScreenPoint.y >= Screen.height - _borderSize;

        if (isOffScreen)
        {
            EnableImage();

            RotatePointerTowardsTargetPosition();

            _pointerImage.sprite = _arrowSprite;

            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;

            if (cappedTargetScreenPosition.x <= _borderSize) cappedTargetScreenPosition.x = _borderSize;
            if (cappedTargetScreenPosition.x >= Screen.width - _borderSize) cappedTargetScreenPosition.x = Screen.width - _borderSize;
            if (cappedTargetScreenPosition.y <= _borderSize) cappedTargetScreenPosition.y = _borderSize;
            if (cappedTargetScreenPosition.y >= Screen.height - _borderSize) cappedTargetScreenPosition.y = Screen.height - _borderSize;

            //Vector3 pointerWorldPosition = _mainCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
            _pointerRectTransform.position = cappedTargetScreenPosition;
            
            _pointerRectTransform.localPosition = new Vector3(_pointerRectTransform.localPosition.x, _pointerRectTransform.localPosition.y, 0f);
        }
        else
        {
            DisableImage();
        }
    }

    private void RotatePointerTowardsTargetPosition()
    {
        Vector3 toPosition = _targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;

        fromPosition.z = 0f;

        Vector3 direction = (toPosition - fromPosition).normalized;
        float angle = 40f + Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //float angle = -180f + Vector3.Angle(fromPosition, toPosition);
        _pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void DisableImage()
    {
        _pointerImage.enabled = false;
    }

    private void EnableImage()
    {
        _pointerImage.enabled = true;
    }

    private void Show(Gem gem)
    {
        _targetPosition = gem.gameObject.transform.position;

        gameObject.SetActive(true);
    }
}
