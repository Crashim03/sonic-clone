using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Running : State
{
    public float GetSpeed()
    {
        return 0f;
    }

    public void Jump()
    {
        Debug.Log("Jumping");
    }

    public void Move(Rigidbody2D rb)
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
}