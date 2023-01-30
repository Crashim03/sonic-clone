using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Falling : State
{
    public Player _player;

    // Stats
    public float _maxSpeed = 7;
    public float _acceleration = 0.4f;
    public float _deceleration = 0.3f;

    public void Jump(InputAction.CallbackContext context) { }

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
        return 4;
    }
}