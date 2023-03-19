
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spindash : State
{
    public Player _player;

    private float _releaseSpeed;
    private bool _isSpindashing = false;

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!_isSpindashing) {
                _isSpindashing = true;
                _releaseSpeed = _player._releaseSpeedSpindash;
                _player._animator.Play("Spindash");
            }

            else if (_releaseSpeed < 40)
                _releaseSpeed += 3;
        }
    }


    public void Crouch(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            if (_isSpindashing)
            {
                _player._rb.velocity = new Vector2(_releaseSpeed * _player._lastDirection, _player._rb.velocity.y);
                _player._animator.Play("Ball (Slow)");
                _player.BallColliders();
                _player.ChangeState(new Ball()
                {
                    _player = _player,
                });
            }
            else
            {
                _player.IdleColliders();
                _player._animator.Play("Idle");
                _player.ChangeState(new Running()
                {
                    _player = _player,
                });
            }
        }
    }

    public void LookUp(InputAction.CallbackContext context) { }
    public void Decelerate() { }
    public void Move() { }
    public void Ground() { }
    public void Fall() { }
}