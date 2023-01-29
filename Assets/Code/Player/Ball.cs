using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ball : State
{
    public Player _player;

    public float _deceleration = 0.1f;
    public float _direction;

    public float _jump = 15f;

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _player.Jump(_jump);

            _player.ChangeState(new Jumping()
            {
                _player = _player,
                _direction = _direction
            });
        }
    }

    public void Move()
    {
        _player.Move(0, 0, _deceleration, 0);

        if (_player._speed == 0)
        {
            _player.ChangeState(new Running()
            {
                _player = _player,
                _direction = _direction
            });
        }
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        Debug.Log("Crouching");
    }
    public void Accelerate(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector2>().x;
    }

    public void Ground() { }

    public int GetState()
    {
        return 2;
    }
}
