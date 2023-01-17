using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public State _state = new IdleState();

    public void Jump() { _state.Jump(); }

    public void Move() { _state.Move(); }

    public void Crouch() { _state.Crouch(); }
}

public interface State
{
    void Jump();
    void Move();
    void Crouch();
}

public class IdleState : State
{
    public void Jump()
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
}