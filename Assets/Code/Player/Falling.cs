using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Falling : State
{
    public Player _player;

    public int GetState()
    {
        return 4;
    }

    public void Move()
    {
        _player.Move(_player._direction, _player._accelerationJumping,  _player._decelerationJumping, _player._maxSpeedJumping, false);
    }

    public void Ground(Collider2D other)
    {
        if (_player._rb.velocity.y <= 0)
        {
            _player.IdleColliders();
            _player.ChangeState(new Running()
            {
                _player = _player,
            });
        }
    }

    public void Jump(InputAction.CallbackContext context) { }
    public void Crouch(InputAction.CallbackContext context) { }
    public void LookUp(InputAction.CallbackContext context) { }
    public void Fall() { }
}