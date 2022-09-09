using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovementController _controller;
    [SerializeField] private float _runSpeed = 10.0f;

    private Vector3 _movementVector
    {
        get
        {
            float directionX = Input.GetAxisRaw("Horizontal");
            float directionY = Input.GetAxisRaw("Vertical");

            return new Vector3(directionX, 0.0f, 0.0f);
        }
    }

    private void FixedUpdate()
    {
        _controller.Move(_runSpeed, (Vector2)_movementVector);
    }
}
