using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterController : MonoBehaviour
{
    #region Privates
    private float f_moveSpeed, f_jumpForce;
    private bool jumped = false;

    private Vector3 V3_moveDirection;

    private Rigidbody rb;
    #endregion

    #region Serialised
    [SerializeField] [Range(0f, 1f)] private float f_xDrag, f_yDrag;
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {

        GetMoveDirection();

        GetIfJumped();

        MovePlayer();

        DragPlayer();

    }

    /// <summary>
    /// A function to apply separate drag to the x plane and y direction of the rigidbody velocity
    /// </summary>
    private void DragPlayer()
    {
        Vector3 v = rb.velocity;
        float y = v.y;

        v *= 1 - f_xDrag;
        y *= 1 - f_yDrag;

        v.y = y;
        rb.velocity = v;
    }

    private void MovePlayer()
    {
        rb.AddForce(V3_moveDirection * f_moveSpeed * Time.deltaTime, ForceMode.Impulse);
    }

    private void GetMoveDirection()
    {
        //V3_moveDirection = InputManager.x.GetXZInputs();
    }

    private void GetIfJumped()
    {
        jumped = InputManager.x.GetJump();
    }

    public void Move(Vector3 dir)
    {
        V3_moveDirection = dir;
    }

}