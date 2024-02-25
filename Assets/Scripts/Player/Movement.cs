using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class Movement : MonoBehaviour {
  [SerializeField] public float moveSpeed = 2f;

  private Vector2 direction = new(0, 0);

  [SerializeField] private Rigidbody2D rb;

  public bool IsRunning {
    get {
      return direction.x != 0 || direction.y != 0;
    }
  }

  public void FixedUpdate() {
    MovePlayer();
  }

  public void OnIndicateMovement(InputAction.CallbackContext ctx) {
    direction = ctx.ReadValue<Vector2>().normalized;
  }

  private void MovePlayer() {
    rb.velocity = moveSpeed * CalculateMovementPenalty() * direction;
  }

  private float CalculateMovementPenalty() {
    Vector3 lookDirection = MouseLocation.I.Position - transform.position;
    Vector3 moveDir = direction;

    float similarity = Vector3.Dot(lookDirection.normalized, moveDir.normalized);
    float movementPenalty = similarity > 0.2f ? 1 : (similarity - 0.2f) / 3f + 1f;
    
    return movementPenalty;
  }
}
