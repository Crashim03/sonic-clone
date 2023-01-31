
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Running : State
{
    public Player _player;

    // Stats
    private float _maxSpeed = 30f;
    private float _acceleration = 0.2f;
    private float _deceleration = 0.2f;

    public int GetState()
    {
        return 0;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _player.Jump();
            _player.BallColliders();
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
            if (Math.Abs(_player._rb.velocity.x) > 5)
            {
                _player.BallColliders();
                _player.ChangeState(new Ball()
                {
                    _player = _player,
                });
            }
            else
            {
                _player._rb.velocity = new Vector2(0, _player._rb.velocity.y);
                _player.CrouchColliders();
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
        if (context.started && Math.Abs(_player._rb.velocity.x) < 5)
        {
            _player._rb.velocity = new Vector2(0, _player._rb.velocity.y);
            _player.ChangeState(new LookingUp()
            {
                _player = _player,
            });
        }
    }

    public void Fall()
    {
        if (_player._rb.velocity.y < 0)
        {
            _player.ChangeState(new Falling()
            {
                _player = _player,
            });
        }
    }

    public void Ground(Collider2D other) { }
}
