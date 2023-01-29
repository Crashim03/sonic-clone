using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ball : State
{
    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jumping");
    }

    public void Move()
    {
        Debug.Log("Moving");
    }

    public void Crouch()
    {
        Debug.Log("Crouching");
    }
    public void Accelerate(InputAction.CallbackContext context)
    {
        Debug.Log("Accelerating");
    }

    public void Ground() { }

    public int GetState()
    {
        return 2;
    }
}
