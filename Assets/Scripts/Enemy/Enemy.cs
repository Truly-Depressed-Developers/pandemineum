﻿using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour {
  private Transform player; 
  [SerializeField] private float detectionRange = 10f;
  [SerializeField] public float movementSpeed = 5f;
  [SerializeField] private float jumpForce = 50f;
  [SerializeField] private float jumpCooldown = 2f;
  [SerializeField] private float jumpFreezeTime = 1f;
  [SerializeField] public float minJumpDistance = 2f;

  [SerializeField] private float movementDirectionOuterAngle = 30f;
  [SerializeField] private float movementDirectionInnerAngle = 30f;

  private bool canJump = true;
  private bool inJump = false;

  private Rigidbody2D rb;

  private void Start() {
    rb = GetComponent<Rigidbody2D>();

    if (player == null) {
      player = GameObject.FindGameObjectWithTag("Player").transform;
    }
  }

  private void FixedUpdate() {
    if (!player) return;
    float distanceToPlayer = Vector3.Distance(transform.position, player.position);

    if (distanceToPlayer <= detectionRange && !inJump) {

      //LookAt(player);

      Move(player);


      if (distanceToPlayer <= minJumpDistance && canJump) {

        Jump();

        StartCoroutine(JumpCooldown());
      }
    }
  }

  private void Update() {
    
  }

  private void LookAt(Transform target) {
    Vector3 direction = (target.position - transform.position).normalized;
    direction.y = 0f;

    Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
    transform.rotation = targetRotation;

  }

  private void Move(Transform target) {
    Vector2 movementDirection = (target.position - transform.position).normalized;

    rb.velocity = movementDirection* movementSpeed;
  }

  private void Jump() {
    Vector3 jumpDirection = (player.position - transform.position).normalized;
    rb.AddForce(jumpDirection * jumpForce);
  }

  private IEnumerator JumpCooldown() {
    canJump = false;
    inJump = true;
    yield return new WaitForSeconds(jumpFreezeTime);
    inJump = false;
    yield return new WaitForSeconds(jumpCooldown);
    canJump = true;
  }
}
