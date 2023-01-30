using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jumping : State
{
    public Player _player;

    // Stats
    public float _maxSpeed = 7;
    public float _acceleration = 0.4f;
    public float _deceleration = 0.3f;
    public bool _jumpedCanceled = false;

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.canceled && _player._rb.velocity.y > 0 && !_jumpedCanceled)
        {
            _jumpedCanceled = true;
            _player._rb.velocity = new Vector2(_player._rb.velocity.x, _player._rb.velocity.y * 0.5f);
        }
    }

    public void Move()
    {
        _player.Move(_player._direction, _acceleration, _deceleration, _maxSpeed);
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        Debug.Log("Crouching");
    }

    public void LookUp(InputAction.CallbackContext context)
    {
        Debug.Log("Looking Up");
    }

    public void Ground()
    {
        _player.ChangeState(new Running()
        {
            _player = _player,
        });
    }

    public void Fall() { }

    public int GetState()
    {
        return 1;
    }
}