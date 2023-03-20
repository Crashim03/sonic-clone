
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
            if (_player._rb.velocity.magnitude > 1)
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
                _player._rb.velocity = new Vector2(0, 0);
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
        if (context.started && _player._rb.velocity.magnitude < 1)
        {
            _player._rb.velocity = new Vector2(0, 0);
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

    public void Push() {
        _player._animator.Play("Pushing");
        _player._isPushing = true;
    }

    public void Balance(Collision2D collision)
    {
        if (_player._rb.velocity.x == 0 && _player._direction == 0 && collision.contacts[0].normal.y == 1) {
            // Left
            if (_player.transform.position.x - _player._colliderIdle.bounds.size.x / 3
                - collision.gameObject.transform.position.x + collision.gameObject.GetComponent<BoxCollider2D>().bounds.size.x / 2 < 0)
            {
                if (_player._lastDirection == 1)
                    _player._animator.Play("Balance left");

                else if (_player._lastDirection == -1)
                    _player._animator.Play("Balance right");
            }

            // Right
            if (-_player.transform.position.x - _player._colliderIdle.bounds.size.x / 3
                + collision.gameObject.transform.position.x + collision.gameObject.GetComponent<BoxCollider2D>().bounds.size.x / 2 < 0)
            {
                if (_player._lastDirection == -1)
                    _player._animator.Play("Balance left");

                else if (_player._lastDirection == 1)
                    _player._animator.Play("Balance right");
            }
        }
    }
}
