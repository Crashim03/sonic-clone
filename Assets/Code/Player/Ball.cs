using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ball : State
{
    public RigidBody2D _rb;

    public void Jump()
    {
        Debug.Log("Jumping");
    }

    public void Move(InputAction.CallbackContext context)
    {
        Debug.Log("Moving");
    }

    public void Crouch()
    {
        Debug.Log("Crouching");
    }
}
