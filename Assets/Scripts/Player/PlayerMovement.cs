using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Проблема с колебаниями из-за rb.velocity, попробовать AddForce или movePosition
//для пружинного крюка использовать Spring Joint, для маятникого Distance Joint

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovementController _controller;
    [SerializeField] private float _runSpeed = 40.0f;
    [SerializeField] private float _walkSpeed = 10.0f;

    private bool _isInJump = false;
    private bool _isCrouched = false;
    private float _directionX;
    private float _directionY;

    private float _climbingSpeed;
    private bool _isClimbing;
    private bool _isLadder;

    private void FixedUpdate()
    {
        _controller.Move(_directionX * _runSpeed * Time.fixedDeltaTime, _isCrouched, _isInJump, _isClimbing);
        _isInJump = false;
    }

    private void Update()
    {
        _directionX = Input.GetAxisRaw("Horizontal");
        _directionY = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            _isInJump = true;
        }

        if (Input.GetKey(KeyCode.E) && _isLadder)
        {
            _isClimbing = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            _isCrouched = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            _isCrouched = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
            _isLadder = true;
    }
}
