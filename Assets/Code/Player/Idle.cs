
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Idle : State
{
    public RigidBody2D _rb;

    public void Jump()
    {
        Debug.Log("Jumping");
    }

    public void Move(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>().x);
    }

    public void Crouch()
    {
        Debug.Log("Crouching");
    }
}
