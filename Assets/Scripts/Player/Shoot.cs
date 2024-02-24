using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
  public class Shoot : MonoBehaviour {
    [SerializeField] private Shotgun shotgun;
    [SerializeField] private bool isShooting;

    private void Update() {
      if (isShooting) shotgun.Fire();
    }
    
    public void OnIndicateShoot(InputAction.CallbackContext ctx) {
      if (ctx.started) {
        isShooting = true;
      } else if (ctx.canceled) {
        isShooting = false;
      }
    }
  }
}
