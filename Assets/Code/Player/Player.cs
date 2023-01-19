using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public State _state;
    public Rigidbody2D _rb;
    public float _speed;

    private void Start()
    {
        _state = new Idle() { _speed = 0 };
    }

    private void FixedUpdate()
    {
        _state.Move(_rb);
        _speed = _state.GetSpeed();
    }

    public void Jump() { _state.Jump(); }

    public void Accelerate(InputAction.CallbackContext context) { _state.Accelerate(context); }

    public void Crouch() { _state.Crouch(); }

    public void ChangeState(State state) { _state = state; }
}