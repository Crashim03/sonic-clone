using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface State
{
    void Jump(InputAction.CallbackContext context);
    void LookUp(InputAction.CallbackContext context);
    void Crouch(InputAction.CallbackContext context);
    void Move();
    void Ground();
    void Fall();
    void Push();
}