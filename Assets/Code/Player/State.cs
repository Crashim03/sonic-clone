using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface State
{
    void Jump(InputAction.CallbackContext context);
    void Move();
    void Crouch(InputAction.CallbackContext context);
    void Ground();
    void Accelerate(InputAction.CallbackContext context);
    void Fall();
    int GetState();
}