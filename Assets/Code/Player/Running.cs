
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Running : State
{
    public Player _player;

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _player.Jump();
            _player._animator.Play("Ball (Jump)");
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
                _player._animator.Play("Ball (Slow)");
                _player.ChangeState(new Ball()
                {
                    _player = _player,
                });
            }
            else
            {
                _player._rb.velocity = new Vector2(0, _player._rb.velocity.y);
                _player._animator.Play("Crouch");
                _player.CrouchColliders();
                _player.ChangeState(new Spindash()
                {
                    _player = _player,
                });
            }
        }
    }

    public void LookUp(InputAction.CallbackContext context)
    {
        if (context.started && Math.Abs(_player._rb.velocity.x) < 5)
        {
            _player._rb.velocity = new Vector2(0, _player._rb.velocity.y);
            _player._animator.Play("Looking Up");
            _player.ChangeState(new LookingUp()
            {
                _player = _player,
            });
        }
    }

    public void Fall()
    {
        _player.ChangeState(new Falling()
        {
            _player = _player,
        });
    }

    public void Move()
    {
        _player.Move(_player._direction, _player._accelerationRunning, _player._decelerationRunning, _player._maxSpeedRunning, true);
    }

    public void Ground()
    { 
    }
}
