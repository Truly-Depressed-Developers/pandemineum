using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
  public class Shoot : MonoBehaviour {
    [SerializeField] private Shotgun shotgun;

    public void OnIndicateShoot(InputAction.CallbackContext ctx) {
      if(!ctx.started) return;
      
      shotgun.Fire();
    }
  }
}
