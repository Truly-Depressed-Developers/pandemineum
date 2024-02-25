using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class PlayerAnimaManager : MonoBehaviour {
  [SerializeField] private Transform bodyAnimTransform;
  [SerializeField] private Transform weaponTransform;

  private Animator bodyAnim;
  private Animator weaponAnim;
  private Movement movement;

  private Vector3 defaultBodyAnimScale;
  private Vector3 defaultWeaponAnimScale;

  public bool MouseOnRight {
    get {
      return Input.mousePosition.x > Screen.width / 2;
    }
  }

  private void Start() {
    transform.TryGetComponent(out movement);
    bodyAnimTransform.TryGetComponent(out bodyAnim);
    weaponTransform.GetChild(0).TryGetComponent(out weaponAnim);

    defaultBodyAnimScale = bodyAnimTransform.localScale;
    defaultWeaponAnimScale = weaponTransform.localScale;
  }

  private void Update() {
    if (movement.IsRunning) {
      bodyAnim.SetBool("isRunning", true);
      weaponAnim.SetBool("isRunning", true);
    } else {
      bodyAnim.SetBool("isRunning", false);
      weaponAnim.SetBool("isRunning", false);
    }

    if (MouseOnRight) {
      bodyAnimTransform.localScale = new Vector3(defaultBodyAnimScale.x, defaultBodyAnimScale.y, defaultBodyAnimScale.z);
      weaponTransform.localScale = new Vector3(defaultWeaponAnimScale.x, defaultWeaponAnimScale.y, defaultWeaponAnimScale.z);
    } else {
      bodyAnimTransform.localScale = new Vector3(-defaultBodyAnimScale.x, defaultBodyAnimScale.y, defaultBodyAnimScale.z);
      weaponTransform.localScale = new Vector3(-defaultWeaponAnimScale.x, defaultWeaponAnimScale.y, defaultWeaponAnimScale.z);
    }

    Vector3 dir = new(
        MouseLocation.I.Position.x - transform.position.x,
        MouseLocation.I.Position.y - transform.position.y,
        0
      );

    weaponTransform.transform.right = MouseOnRight ? dir.normalized : -dir.normalized;
  }
}
