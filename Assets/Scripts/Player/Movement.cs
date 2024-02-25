using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class Movement : MonoBehaviour {
  [SerializeField] public float moveSpeed = 2f;
  [SerializeField] private float dashCooldown = 1.4f;
  [SerializeField] private float dashTime = 0.4f;
  [SerializeField] private float dashMultiplayer = 5f;
  
  private readonly float dashStartTime = 0.5f;
  
  private Vector2 lastDashDirection = new(0, 1);
  private Vector2 dashDirection = new(0, 1);
  private float lastDashTime = float.NegativeInfinity;

  private bool inDashMove;

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
    
    if (direction.y != 0 || direction.x != 0) {
      lastDashDirection = direction;
    }
  }

  private void MovePlayer() {
    float constCalculated = moveSpeed * CalculateMovementPenalty();

    if (Time.time - lastDashTime < dashTime && Time.time > dashStartTime) {
      rb.velocity = dashMultiplayer * constCalculated * dashDirection;
      
      inDashMove = true;
      return;
    }
            
    inDashMove = false;
    rb.velocity = constCalculated * direction;
  }
  
  public void OnIndicateDash(InputAction.CallbackContext ctx) {
    if (ctx.canceled || Time.time - lastDashTime < dashCooldown + dashTime) return;

    dashDirection = lastDashDirection;
    inDashMove = true;
    lastDashTime = Time.time;
  }

  private float CalculateMovementPenalty() {
    Vector3 lookDirection = MouseLocation.I.Position - transform.position;
    Vector3 moveDir = direction;

    float similarity = Vector3.Dot(lookDirection.normalized, moveDir.normalized);
    float movementPenalty = similarity > 0.2f ? 1 : (similarity - 0.2f) / 3f + 1f;

    return movementPenalty;
  }
  
  public bool isInDashMove() {
    return inDashMove;
  }
}
