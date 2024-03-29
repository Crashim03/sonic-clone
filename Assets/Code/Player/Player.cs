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
    public CapsuleCollider2D _colliderBall;
    public CapsuleCollider2D _colliderIdle;

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

    public int _currentCollisions = 0;

    // Animation states
    public bool _isBreaking = false;
    public bool _triedToSpeed = false;
    public bool _isPushing = false;

    public Vector2 _normal;

    private void Start()
    {
        IdleColliders();
        _state = new Running() { _player = this }; 
    }

    private void Update()
    {   
        Debug.Log(_rb.velocity.magnitude);
        _animator.SetFloat("Speed", Math.Abs(_rb.velocity.magnitude));
        _animator.SetBool("Breaking", _isBreaking);
        _animator.SetBool("Pushing", _isPushing);
    }

    public void IdleColliders()
    {
        _colliderBall.enabled = false;

        _colliderIdle.enabled = true;
    }

    public void CrouchColliders()
    {
        _colliderBall.enabled = false;

        _colliderIdle.enabled = false;
    }

    public void BallColliders()
    {
        _colliderBall.enabled = true;

        _colliderIdle.enabled = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.contacts[0].normal);

        _currentCollisions++;

        // GroundCheck
        if (collision.contacts[0].normal.y > 0f)
            _state.Ground();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        _normal = new Vector2(collision.contacts[0].normal.x, collision.contacts[0].normal.y);

        if ((collision.contacts[0].normal.x == 1  && _direction == -1) || (collision.contacts[0].normal.x == -1 && _direction == 1)) {
            _state.Push();
        }
        else if ((collision.contacts[0].normal.x == 1  && _direction == 1) || (collision.contacts[0].normal.x == -1 && _direction == -1)) {
            _isPushing = false;
        }

        _state.Balance(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _currentCollisions--;

        Debug.Log(_currentCollisions);
    }

    private void FixedUpdate() { _state.Move(); 
        if (_currentCollisions == 0)
        {
            _state.Fall();
        }
    }
    public void Move(float direction, float acceleration, float deceleration, float max_speed, bool canBreak)
    {
        float speed = _rb.velocity.x;

        if (direction > 0)
        {
            _spriteRenderer.flipX = false;
            _lastDirection = 1;

            if (_isPushing) 
            {
                _animator.Play("Pushing");
            }
        }
        else if (direction < 0)
        {
            _spriteRenderer.flipX = true;
            _lastDirection = -1;

            if (_isPushing) 
            {
                _animator.Play("Pushing");
            }
        }

        if (!canBreak)
        {
            _isPushing = false;
            _isBreaking = false;
        }

        if (direction > 0)
        {
            if (speed < 0)
            {
                if (canBreak)
                {
                    speed += deceleration * 6;
                    _isBreaking = true;
                }
                else
                    speed += deceleration * 2;

                if (speed >= 0)
                {
                    speed = 0;
                }
            }
            else
            {
                _isBreaking = false;
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

                if (speed <= 0)
                {
                    speed = 0;
                    _isBreaking = false;
                }
            }
            else
            {
                _isBreaking = false;
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

    public void Accelerate(InputAction.CallbackContext context) { _direction = context.ReadValue<Vector2>().x; }
    public void Jump() { _rb.velocity = new Vector2(_rb.velocity.x + _normal.x * _jump, _normal.y*_jump); _isPushing = false; _isBreaking = false; }
    public void ChangeState(State state) { _state = state; }
    public void JumpAction(InputAction.CallbackContext context) { _state.Jump(context); }
    public void Crouch(InputAction.CallbackContext context) { _state.Crouch(context); }
    public void LookUp(InputAction.CallbackContext context) { _state.LookUp(context); }
}