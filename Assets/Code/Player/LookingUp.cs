using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookingUp : State
{
    public Player _player;

    private bool _isSuperSpeeding = false;
    private float _currentTime;

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started && !_isSuperSpeeding)
        {
            Debug.Log("Super Speeding");
            _player._animator.Play("Super Speeding");
            _isSuperSpeeding = true;
            _currentTime = Time.realtimeSinceStartup;
            Debug.Log(_currentTime);
        }
    }

    public void LookUp(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            if (_isSuperSpeeding && Time.realtimeSinceStartup - _currentTime > _player._timeToSuperSpeed)
            {
                _player._animator.Play("Super Speed");
                _player._rb.velocity = new Vector2(_player._releaseSuperSpeed * _player._lastDirection, _player._rb.velocity.y);
            }
            else { _player._animator.Play("Idle"); }
            
            _player.ChangeState(new Running()
            {
                _player = _player,
            });
        }
    }

    public void Move() { }
    public void Crouch(InputAction.CallbackContext context) { }
    public void Ground() { }
    public void Push() { }
    public void Balance(Collision2D collision) { }
    public void Fall() { }
}