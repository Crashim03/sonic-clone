using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface State
{
    float GetSpeed();
    void Jump();
    void Move(Rigidbody2D rb);
    void Crouch();
    void Accelerate(InputAction.CallbackContext context);
}