using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pickups {
  public class Pickup : MonoBehaviour {
    private Vector3 attractionDir;
    private bool isBeingAttracted;
    private float attractionSpeed;
    [SerializeField] private float attractionSpeedDelta = 15f;
    private static readonly float maxAttractionSpeed = 25f;

    private void Update() {
      int sign = isBeingAttracted ? 1 : -1;
      attractionSpeed = Mathf.Clamp(attractionSpeed + attractionSpeedDelta * Time.deltaTime * sign, 0, maxAttractionSpeed);

      transform.position += attractionSpeed * Time.deltaTime * attractionDir;
    }

    private void OnTriggerEnter2D(Collider2D other) {
      if (!IsColliderEligible(other)) return;
 
      isBeingAttracted = true;
    }

    private void OnTriggerStay2D(Collider2D other) {
      if (!IsColliderEligible(other)) return;

      attractionDir = (other.transform.position - transform.position).normalized;
    }

    private void OnTriggerExit2D(Collider2D other) {
      if (!IsColliderEligible(other)) return;

      isBeingAttracted = false;
    }

    private bool IsColliderEligible(Collider2D other) {
      return other.TryGetComponent<Collector>(out _);
    }
  }
}
