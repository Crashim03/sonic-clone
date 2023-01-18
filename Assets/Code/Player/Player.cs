using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public State _state;
    public Rigidbody2D _rb;
    public float _acceleration;
    public float _speed;

    private void Start()
    {
        _state = new Idle();
        _speed = 5;
        _acceleration = 0.5f;
    }

    private void Update()
    {

    }

    public void Jump() { _state.Jump(); }

    public void Move(InputAction.CallbackContext context) { _state.Move(context); }

    public void Crouch() { _state.Crouch(); }
}