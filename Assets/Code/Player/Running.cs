
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Running : State
{
    public float _maxSpeed = 10;
    public float _acceleration = 0.4f;
    public float _deceleration = 0.6f;
    public float _direction = 0;
    public Player _player;
    public float _jump = 10f;

    public float GetSpeed()
    {
        return 1;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
            _player.Jump(_jump);
    }

    public void Accelerate(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector2>().x;
    }

    public void Crouch()
    {
        Debug.Log("Crouching");
    }

    public void Move()
    {
        _player.Move(_direction, _acceleration, _deceleration, _maxSpeed);
    }
}
