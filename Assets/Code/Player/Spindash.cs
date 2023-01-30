
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spindash : State
{
    public Player _player;

    public float _releaseSpeed = 25;

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
            if (_player._isSpindashing)
            {
                _player._speed = _releaseSpeed * _player._lastDirection;
                _player._isSpindashing = false;
                _player.ChangeState(new Ball()
                {
                    _player = _player,
                });
            }
            else
            {
                _player.ChangeState(new Running()
                {
                    _player = _player,
                });
            }
        }
    }

    public void LookUp(InputAction.CallbackContext context)
    {
        Debug.Log("Looking Up");
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