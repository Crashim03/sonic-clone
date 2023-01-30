using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookingUp : State
{
    public Player _player;

    public float _releaseSpeed = 50;

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Super");
            _player._isSuperSpeeding = true;
        }
    }
    public void Move() { }
    public void Crouch(InputAction.CallbackContext context) { }
    public void Ground() { }
    public void Fall() { }
    public int GetState()
    {
        return 5;
    }
    public void LookUp(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            if (_player._isSuperSpeeding)
            {
                _player._speed = _releaseSpeed * _player._lastDirection;
                _player._isSuperSpeeding = false;
            }
            _player.ChangeState(new Running()
            {
                _player = _player,
            });
        }
    }
}