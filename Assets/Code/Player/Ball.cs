using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ball : State
{
    public Player _player;

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

            _player.Jump();
            _player._animator.Play("Ball (Jump)");
            _player.ChangeState(new Jumping()
            {
                _player = _player,
            });
        }
    }

    public void Move()
    {
        _player.Move(0, 0, _player._decelerationBall, 0, false);

        if (_player._rb.velocity.x == 0)
        {
            _player.IdleColliders();
            _player._animator.Play("Idle");
            _player.ChangeState(new Running()
            {
                _player = _player,
            });
        }
    }

    public void Fall()
    {
        _player._animator.Play("Ball (Jump)");
        _player.ChangeState(new Jumping()
        {
            _player = _player,
        });
    }

    public void Crouch(InputAction.CallbackContext context) { }
    public void Ground() { }
    public void LookUp(InputAction.CallbackContext context) { }
    public void Push() { }
}
