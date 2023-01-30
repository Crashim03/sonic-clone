
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spindash : State
{
    public Player _player;

    public float _releaseSpeed = 25;
    public float _direction;

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!_player._isSpindashing)
                _player._isSpindashing = true;

            else if (_releaseSpeed < 40)
                _releaseSpeed += 3;
        }
    }

    public void Move()
    {
        Debug.Log("Moving");
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            _player.ChangeState(new Ball()
            {
                _player = _player,
                _direction = _direction
            });

            if (_player._isSpindashing)
            {
                _player._speed = _releaseSpeed * _player._lastDirection;
                _player._isSpindashing = false;
            }
        }
    }

    public void Accelerate(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector2>().x;
    }

    public void Decelerate()
    {
        Debug.Log("Decelerating");
    }

    public void Ground() { }

    public void Fall() { }

    public int GetState()
    {
        return 3;
    }
}