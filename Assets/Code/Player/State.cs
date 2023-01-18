using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface State
{
    public RigidBody2D _rb;
    void Jump();
    void Move(InputAction.CallbackContext context);
    void Crouch();
    void Decelerate();
}