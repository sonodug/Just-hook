using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovementController _controller;
    [SerializeField] private float _runSpeed = 10.0f;
    [SerializeField] private float _boostSpeed = 40.0f;

    private Vector3 _movementVector
    {
        get
        {
            float directionX = Input.GetAxisRaw("Horizontal");
            float directionY = Input.GetAxisRaw("Vertical");

            return new Vector3(directionX, directionY, 0.0f);
        }
    }

    private void Update()
    {
        _controller.Move(_runSpeed, (Vector2)_movementVector);
    }
}
