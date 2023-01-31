using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Falling : State
{
    public Player _player;

    // Stats
    private float _maxSpeed = 7;
    private float _acceleration = 0.4f;
    private float _deceleration = 0.15f;

    public int GetState()
    {
        return 4;
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

    public void Jump(InputAction.CallbackContext context) { }
    public void Crouch(InputAction.CallbackContext context) { }
    public void LookUp(InputAction.CallbackContext context) { }
    public void Fall() { }
}