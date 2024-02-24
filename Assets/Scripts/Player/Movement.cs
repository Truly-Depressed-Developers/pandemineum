using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Vector2 direction = new(0, 0);

    [SerializeField] private Rigidbody2D rb;

    public void FixedUpdate()
    {
        MovePlayer();
    }

    public void OnIndicateMovement(InputAction.CallbackContext ctx)
    {
        direction = ctx.ReadValue<Vector2>().normalized;
    }

    private void MovePlayer()
    {
        rb.velocity = moveSpeed * direction;
    }
}
