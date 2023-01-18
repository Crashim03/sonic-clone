using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface State
{
    void Jump();
    void Move(InputAction.CallbackContext context);
    void Crouch();
}

public class Idle : State
{
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

public class Spindash : State
{
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

public class Running : State
{
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

public class Jumping : State
{
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

public class Ball : State
{
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
