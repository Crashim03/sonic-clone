using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface State
{
    void Jump(InputAction.CallbackContext context);
    void Move();
    void Crouch();
    void Ground();
    void Accelerate(InputAction.CallbackContext context);
}