using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jumping : State
{
    public Player _player;

    private bool _jumpedCanceled = false;

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.canceled && _player._rb.velocity.y > 0 && !_jumpedCanceled)
        {
            _jumpedCanceled = true;
            _player._rb.velocity = new Vector2(_player._rb.velocity.x, _player._rb.velocity.y * _player._jumpDeceleration);
        }
    }

    public void Move()
    {
        _player.Move(_player._direction, _player._accelerationJumping, _player._decelerationJumping, _player._maxSpeedJumping, false);
    }

    public void Ground()
    {
        if (_player._rb.velocity.y <= 0)
        {
            _player._animator.Play("Idle");
            _player.IdleColliders();
            _player.ChangeState(new Running()
            {
                _player = _player,
            });
        }
    }

    public void Crouch(InputAction.CallbackContext context) { }
    public void LookUp(InputAction.CallbackContext context) { }
    public void Fall() { }
}