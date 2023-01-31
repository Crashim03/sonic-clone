using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

/* 
States:
    0: Running
    1: Jumping
    2: Ball
    3: Spindash
    4: Falling
*/

public class Player : MonoBehaviour
{
    public State _state = new Running() { _player = this };

    // Components
    public Rigidbody2D _rb;
    public Animator _animator;
    public SpriteRenderer _spriteRenderer;

    // Colliders
    public CapsuleCollider2D _capsuleBall;
    public BoxCollider2D _boxBall;
    public CapsuleCollider2D _capsuleIdle;
    public BoxCollider2D _boxIdle;
    public BoxCollider2D _boxCrouch;

    // Stats
    public float _jump = 15.5f;
    public float _direction = 0;
    public float _lastDirection = 1;

    // Animation states
    public bool _isSpindashing = false;
    public bool _isSuperSpeeding = false;
    public bool _isBreaking = false;


    private void Update()
    {
        _animator.SetInteger("State", _state.GetState());
        _animator.SetFloat("Speed", Math.Abs(_rb.velocity.x));
        _animator.SetBool("Spindash", _isSpindashing);
        _animator.SetBool("SuperSpeed", _isSuperSpeeding);
        _animator.SetBool("Breaking", _isBreaking);
    }


    public void Move(float direction, float acceleration, float deceleration, float max_speed, bool canBreak)
    {
        float speed = _rb.velocity.x;

        if (direction > 0)
        {
            _spriteRenderer.flipX = false;
            _lastDirection = 1;
        }
        else if (direction < 0)
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

        _rb.velocity = new Vector2(speed, _rb.velocity.y);
    }

    public void IdleColliders()
    {
        _capsuleBall.enabled = false;
        _boxBall.enabled = false;

        _capsuleIdle.enabled = true;
        _boxIdle.enabled = true;

        _boxCrouch.enabled = false;
    }

    public void CrouchColliders()
    {
        _capsuleBall.enabled = false;
        _boxBall.enabled = false;

        _capsuleIdle.enabled = false;
        _boxIdle.enabled = false;

        _boxCrouch.enabled = true;
    }

    public void BallColliders()
    {
        _capsuleBall.enabled = true;
        _boxBall.enabled = true;

        _capsuleIdle.enabled = false;
        _boxIdle.enabled = false;

        _boxCrouch.enabled = false;
    }

    public void Accelerate(InputAction.CallbackContext context) { _direction = context.ReadValue<Vector2>().x; }
    public void Jump() { _rb.velocity = new Vector2(_rb.velocity.x, _jump); }
    private void FixedUpdate() { _state.Move(); }
    private void Start() { IdleColliders(); }
    private void OnTriggerEnter2D(Collider2D other) { _state.Ground(other); }
    private void OnCollisionExit2D(Collision2D other) { _state.Fall(); }
    public void ChangeState(State state) { _state = state; }
    public void JumpAction(InputAction.CallbackContext context) { _state.Jump(context); }
    public void Crouch(InputAction.CallbackContext context) { _state.Crouch(context); }
    public void LookUp(InputAction.CallbackContext context) { _state.LookUp(context); }
}