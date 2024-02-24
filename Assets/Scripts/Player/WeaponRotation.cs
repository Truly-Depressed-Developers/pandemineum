using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Player {
  public class WeaponRotation : MonoBehaviour {
    [SerializeField] private Transform weapon;

    private void Update() {
      Vector3 dir = new (
        MouseLocation.I.Position.x - transform.position.x,
        MouseLocation.I.Position.y - transform.position.y,
        0
      );

      weapon.transform.right = dir.normalized;
    }
  }
}
