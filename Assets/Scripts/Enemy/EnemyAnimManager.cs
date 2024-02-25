using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EnemyAnimManager : MonoBehaviour {
  [SerializeField] private Transform bodyAnimTransform;
  [SerializeField] private Enemy enemy;

  private Animator bodyAnim;

  private Vector3 defaultBodyAnimScale;

  private void Start() {
    bodyAnimTransform.TryGetComponent(out bodyAnim);

    defaultBodyAnimScale = bodyAnimTransform.localScale;
  }

  private void Update() {
    if (enemy.isRunning) {
      bodyAnim.SetBool("isRunning", true);
    } else {
      bodyAnim.SetBool("isRunning", false);
    }

    if(enemy.isJumping) {
      bodyAnim.SetBool("isJumping", true);
    } else {
      bodyAnim.SetBool("isJumping", false);
    }

    bodyAnimTransform.localScale = enemy.MovementDirection.x > 0
      ? new Vector3(defaultBodyAnimScale.x, defaultBodyAnimScale.y, defaultBodyAnimScale.z)
      : new Vector3(-defaultBodyAnimScale.x, defaultBodyAnimScale.y, defaultBodyAnimScale.z);
  }
}
