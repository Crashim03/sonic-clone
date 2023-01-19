using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public State _state;
    public Rigidbody2D _rb;

    [System.NonSerialized]
    public float _speed = 0;

    private void Start()
    {
        _state = new Running() { _player = this };
    }

    private void FixedUpdate()
    {
        _state.Move();
    }

    public void JumpAction(InputAction.CallbackContext context) { _state.Jump(context); }

    public void Accelerate(InputAction.CallbackContext context) { _state.Accelerate(context); }

    public void Crouch() { _state.Crouch(); }

    public void ChangeState(State state) { _state = state; }

    public void Jump(float jump)
    {
        _rb.velocity = new Vector2(_rb.velocity.x, jump);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _state.Ground();
    }

    public void Move(float direction, float acceleration, float deceleration, float max_speed)
    {
        if (direction > 0)
        {
            if (_speed < 0)
            {
                _speed += deceleration * 2;

                if (_speed > 0) { _speed = 0; }
            }
            else
            {
                if (_speed < max_speed)
                {
                    _speed += acceleration;

                    if (_speed > max_speed)
                    {
                        _speed = max_speed;
                    }
                }
            }
        }
        else if (direction < 0)
        {
            if (_speed > 0)
            {
                _speed -= deceleration * 2;

                if (_speed < 0) { _speed = 0; }
            }
            else
            {
                if (_speed > -max_speed)
                {
                    _speed -= acceleration;

                    if (_speed < -max_speed)
                    {
                        _speed = -max_speed;
                    }
                }
            }
        }
        else
        {
            if (_speed > 0)
            {
                _speed -= deceleration;

                if (_speed < 0) { _speed = 0; }
            }
            else if (_speed < 0)
            {
                _speed += deceleration;

                if (_speed > 0) { _speed = 0; }
            }
        }

        _rb.velocity = new Vector2(_speed, _rb.velocity.y);
    }
}