using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public State _state;

    private void Start()
    {
        _state = new Idle();
    }

    private void Update()
    {
        _state.Decelerate();
    }

    public void Jump() { _state.Jump(); }

    public void Move(InputAction.CallbackContext context) { _state.Move(context); }

    public void Crouch() { _state.Crouch(); }
}