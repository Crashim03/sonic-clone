using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface State
{
    float GetSpeed();
    void Jump(InputAction.CallbackContext context);
    void Move();
    void Crouch();
    void Accelerate(InputAction.CallbackContext context);
}