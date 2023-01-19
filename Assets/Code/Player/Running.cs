
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Running : State
{
    public float _speed;
    public float _maxSpeed = 10;
    public float _acceleration = 0.4f;
    public float _deceleration = 0.6f;
    public float _direction = 0;

    public float GetSpeed()
    {
        return _speed;
    }

    public void Jump()
    {
        Debug.Log("Jumping");
    }

    public void Accelerate(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector2>().x;
    }

    public void Crouch()
    {
        Debug.Log("Crouching");
    }

    public void Move(Rigidbody2D rb)
    {
        if (_direction > 0)
        {
            if (_speed < 0)
            {
                _speed += _deceleration * 2;

                if (_speed > 0) { _speed = 0; }
            }
            else
            {
                _speed += _acceleration;

                if (_speed > _maxSpeed)
                {
                    _speed = _maxSpeed;
                }
            }
        }
        else if (_direction < 0)
        {
            if (_speed > 0)
            {
                _speed -= _deceleration * 2;

                if (_speed < 0) { _speed = 0; }
            }
            else
            {
                _speed -= _acceleration;

                if (-_speed > _maxSpeed)
                {
                    _speed = -_maxSpeed;
                }
            }
        }
        else
        {
            if (_speed > 0)
            {
                _speed -= _deceleration;

                if (_speed < 0) { _speed = 0; }
            }
            else if (_speed < 0)
            {
                _speed += _deceleration;

                if (_speed > 0) { _speed = 0; }
            }
        }


        rb.velocity = new Vector2(_speed, rb.velocity.y);
    }
}
