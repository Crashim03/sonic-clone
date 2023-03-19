using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Player : MonoBehaviour
{
    public State _state;

    // Components
    public Rigidbody2D _rb;
    public Animator _animator;
    public SpriteRenderer _spriteRenderer;

    // Colliders
    public CircleCollider2D _boxBall;
    public BoxCollider2D _boxIdle;
    public BoxCollider2D _boxCrouch;

    public float _direction = 0;
    public float _lastDirection = 1;

    // Stats
    public float _jump = 15.5f;
    public float _maxSpeedJumping = 7;
    public float _accelerationJumping = 0.4f;
    public float _decelerationJumping = 0.15f;
    public float _jumpDeceleration = 0.5f;

    public float _decelerationBall = 0.1f;

    public float _releaseSuperSpeed = 50;
    public float _timeToSuperSpeed = 0.5f;

    public float _maxSpeedRunning = 30f;
    public float _accelerationRunning = 0.2f;
    public float _decelerationRunning = 0.2f;

    public float _releaseSpeedSpindash = 25;

    // Animation states
    public bool _isBreaking = false;
    public bool _triedToSpeed = false;
    public bool _isPushing = false;

    private void Start()
    {
        IdleColliders();
        _state = new Running() { _player = this }; 
    }

    private void Update()
    {   
        _animator.SetFloat("Speed", Math.Abs(_rb.velocity.x));
        _animator.SetBool("Breaking", _isBreaking);
        _animator.SetBool("Pushing", _isPushing);
    }

    public void Move(float direction, float acceleration, float deceleration, float max_speed, bool canBreak)
    {
        float speed = _rb.velocity.x;

        if (speed == 0 && _triedToSpeed && !_isBreaking && direction != 0 && canBreak) {
            _animator.Play("Pushing");
            _triedToSpeed = false;
            _isPushing = true;
        }

        if (speed > 0)
        {
            _spriteRenderer.flipX = false;
            _lastDirection = 1;
        }
        else if (speed < 0)
        {
            _spriteRenderer.flipX = true;
            _lastDirection = -1;
        }

        if (!canBreak)
        {
            _isBreaking = false;
        }

        if (direction > 0)
        {
            if (speed == 0 && canBreak && !_isBreaking) { _triedToSpeed = true; }
            
            if (speed < 0)
            {
                if (canBreak)
                {
                    speed += deceleration * 6;
                    _isBreaking = true;
                }
                else
                    speed += deceleration * 2;

                if (speed > 0)
                {
                    speed = 0;
                    _isBreaking = false;
                }
            }
            else
            {
                if (speed == 0 && canBreak && !_isBreaking) { _triedToSpeed = true; }

                if (speed < max_speed)
                {
                    speed += acceleration;

                    if (speed > max_speed)
                    {
                        speed = max_speed;
                    }
                }
            }
        }
        else if (direction < 0)
        {
            if (speed > 0)
            {
                if (canBreak)
                {
                    speed -= deceleration * 6;
                    _isBreaking = true;
                }
                else
                    speed -= deceleration * 2;

                if (speed < 0)
                {
                    speed = 0;
                    _isBreaking = false;
                }
            }
            else
            {
                if (speed == 0 && canBreak && !_isBreaking) { _triedToSpeed = true; }

                if (speed > -max_speed)
                {
                    speed -= acceleration;

                    if (speed < -max_speed)
                    {
                        speed = -max_speed;
                    }
                }
            }
        }
        else
        {
            _triedToSpeed = false;
            _isPushing = false;
            _isBreaking = false;

            if (speed > 0)
            {
                speed -= deceleration;

                if (speed < 0)
                {
                    speed = 0;
                    _isBreaking = false;
                }
            }
            else if (speed < 0)
            {
                speed += deceleration;

                if (speed > 0)
                {
                    speed = 0;
                    _isBreaking = false;
                }
            }
        }

        if (_isBreaking) { _animator.Play("Breaking"); }

        _rb.velocity = new Vector2(speed, _rb.velocity.y);
    }

    public void IdleColliders()
    {
        _boxBall.enabled = false;

        _boxIdle.enabled = true;

        _boxCrouch.enabled = false;
    }

    public void CrouchColliders()
    {
        _boxBall.enabled = false;

        _boxIdle.enabled = false;

        _boxCrouch.enabled = true;
    }

    public void BallColliders()
    {
        _boxBall.enabled = true;

        _boxIdle.enabled = false;

        _boxCrouch.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.contacts[0].normal);

        // GroundCheck
        if (collision.contacts[0].normal.y > 0.1f)
            _state.Ground();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(collision.contactCount);
    }

    private void FixedUpdate() { _state.Move(); }
    public void Accelerate(InputAction.CallbackContext context) { _direction = context.ReadValue<Vector2>().x; }
    public void Jump() { _rb.velocity = new Vector2(_rb.velocity.x, _jump); }
    public void ChangeState(State state) { _state = state; }
    public void JumpAction(InputAction.CallbackContext context) { _state.Jump(context); }
    public void Crouch(InputAction.CallbackContext context) { _state.Crouch(context); }
    public void LookUp(InputAction.CallbackContext context) { _state.LookUp(context); }
}