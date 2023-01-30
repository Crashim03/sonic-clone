
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Running : State
{
    public Player _player;

    // Stats
    public float _maxSpeed = 30f;
    public float _acceleration = 0.2f;
    public float _deceleration = 0.2f;

    public int GetState()
    {
        return 0;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _player.Jump();

            _player.ChangeState(new Jumping()
            {
                _player = _player,
            });
        }
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (Math.Abs(_player._speed) > 5)
            {
                _player.ChangeState(new Ball()
                {
                    _player = _player,
                });
            }
            else
            {
                _player._speed = 0;
                _player.Move(_player._direction, _acceleration, _deceleration, _maxSpeed, false);
                _player.ChangeState(new Spindash()
                {
                    _player = _player,
                });
            }
        }
    }

    public void Move()
    {
        _player.Move(_player._direction, _acceleration, _deceleration, _maxSpeed, true);
    }

    public void LookUp(InputAction.CallbackContext context)
    {
        _player.ChangeState(new LookingUp()
        {
            _player = _player,
        });
    }

    public void Fall()
    {
        _player.ChangeState(new Falling()
        {
            _player = _player,
        });
    }

    public void Ground() { }
}
