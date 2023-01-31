using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookingUp : State
{
    public Player _player;

    private float _releaseSpeed = 50;
    private float _currentTime;
    private float _timeToSuperSpeed = 0.5f;

    public int GetState()
    {
        return 5;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started && !_player._isSuperSpeeding)
        {
            Debug.Log("Super");
            _player._isSuperSpeeding = true;
            _currentTime = Time.realtimeSinceStartup;
            Debug.Log(_currentTime);
        }
    }

    public void LookUp(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            if (_player._isSuperSpeeding && Time.realtimeSinceStartup - _currentTime > _timeToSuperSpeed)
            {
                _player._rb.velocity = new Vector2(_releaseSpeed * _player._lastDirection, _player._rb.velocity.y);
            }
            _player.ChangeState(new Running()
            {
                _player = _player,
            });
            _player._isSuperSpeeding = false;
        }
    }

    public void Move() { }
    public void Crouch(InputAction.CallbackContext context) { }
    public void Ground(Collider2D other) { }
    public void Fall() { }
}