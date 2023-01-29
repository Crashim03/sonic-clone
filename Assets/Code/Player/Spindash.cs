
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spindash : State
{
    public Player _player;

    public float _releaseSpeed = 10;
    public bool _isSpinning = false;
    public float _direction;

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!_isSpinning)
                _isSpinning = true;

            else if (_releaseSpeed > 20)
                _releaseSpeed += 2;
        }
    }

    public void Move()
    {
        Debug.Log("Moving");
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _player._speed = _releaseSpeed * _direction;
            _player.ChangeState(new Ball()
            {
                _player = _player,
                _direction = _direction
            });
        }
    }

    public void Accelerate(InputAction.CallbackContext context)
    {
        Debug.Log("Accelerating");
    }

    public void Decelerate()
    {
        Debug.Log("Decelerating");
    }

    public void Ground() { }

    public int GetState()
    {
        return 3;
    }
}