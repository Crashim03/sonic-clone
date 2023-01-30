using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jumping : State
{
    public Player _player;

    // Stats
    private float _maxSpeed = 7;
    private float _acceleration = 0.4f;
    private float _deceleration = 0.15f;
    private float _jumpDeceleration = 0.5f;

    private bool _jumpedCanceled = false;

    public int GetState()
    {
        return 1;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.canceled && _player._rb.velocity.y > 0 && !_jumpedCanceled)
        {
            _jumpedCanceled = true;
            _player._rb.velocity = new Vector2(_player._rb.velocity.x, _player._rb.velocity.y * _jumpDeceleration);
        }
    }

    public void Move()
    {
        _player.Move(_player._direction, _acceleration, _deceleration, _maxSpeed, false);
    }

    public void Ground()
    {
        _player.ChangeState(new Running()
        {
            _player = _player,
        });
    }

    public void Crouch(InputAction.CallbackContext context) { }
    public void LookUp(InputAction.CallbackContext context) { }
    public void Fall() { }
}