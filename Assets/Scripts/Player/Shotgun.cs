using System;
using System.Collections;
using System.Collections.Generic;
using DamageSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player {
  public class Shotgun : MonoBehaviour {
    [SerializeField] private Transform tip;
    [SerializeField] private GameObject lineRendererInstance;

    [SerializeField] private float spread;
    [SerializeField] private int pellets;
    [SerializeField] private float range;
    [SerializeField] private int clipSize;

    [SerializeField] private float shotCooldown;
    [SerializeField] private float reloadTime;

    [SerializeField] private LayerMask hitColliderLayers;

    private CursorManager cursorManager;

    private float lastShot;
    private float lastReload;
    private int ammo;

    public float ReloadProgress {
      get {
        return Mathf.Clamp((Time.time - lastReload) / reloadTime, 0f, 1f);
      }
    }

    public float ShotCooldownProgress {
      get {
        return Mathf.Clamp((Time.time - lastShot) / shotCooldown, 0f, 1f);
      }
    }

    public int CurrentAmmo {
      get {
        return ammo;
      }
    }

    public bool IsReloading {
      get {
        return ReloadProgress > Mathf.Epsilon;
      }
    }

    public bool IsCoolingDown {
      get {
        return ShotCooldownProgress > Mathf.Epsilon;
      }
    }

    public void Start() {
      lastReload = -reloadTime;
      lastShot = -shotCooldown;
      ammo = clipSize;

      GameObject.Find("Crosshair").TryGetComponent(out cursorManager);


      if (cursorManager) {
        Debug.Log("CursorManager found");
      } else {
        Debug.Log("CursorManager not found");
      }

      if (cursorManager) cursorManager.setShellsCount(clipSize);
    }

    public void Fire() {
      if (!CanShoot()) return;

      Vector3 shotgunDir = transform.right;

      for (int i = 0; i < pellets; i++) {
        float randomAngle = Random.Range(-spread, spread);
        var randomDir = (Quaternion.AngleAxis(randomAngle, transform.forward) * shotgunDir).normalized;
        var pelletPoint = tip.position + randomDir * range;

        Vector3[] arr = { tip.position, pelletPoint };
        var lri = Instantiate(lineRendererInstance);

        Raycast(tip.position, randomDir, out float newRange);

        if(newRange < range) {
          arr[1] = tip.position + randomDir * newRange;
        }
        SetLrProperties(lri, arr);

      }

      // lr.SetPositions(pelletPoints);
      if (cursorManager) cursorManager.onShoot(--ammo);

      if (ammo == 0) {
        lastReload = Time.time;
        StartCoroutine(DoReload());
      } else {
        if (cursorManager) cursorManager.startBarAnimation(shotCooldown);
      }

      lastShot = Time.time;
    }

    private IEnumerator DoReload() {
      if (cursorManager) cursorManager.startBarAnimation(reloadTime);
      yield return new WaitForSeconds(reloadTime);
      ammo = clipSize;
      if (cursorManager) cursorManager.onReload();
    }

    private bool CanShoot() {
      return ammo > 0 && Mathf.Abs(ReloadProgress - 1f) < Mathf.Epsilon && Mathf.Abs(ShotCooldownProgress - 1f) < Mathf.Epsilon;
    }

    private void SetLrProperties(GameObject lri, Vector3[] points) {
      lri.TryGetComponent(out LineRendererFade lrf);
      lri.TryGetComponent(out LineRenderer lr);

      if (!lrf || !lr) return;

      lrf.points = points;
      lrf.fadeOutTime = 0.5f;

      lr.startWidth = 0.05f;
      lr.endWidth = 0.05f;

      Gradient gradient = new();
      gradient.SetKeys(
        new GradientColorKey[] { new(Color.white, 0.0f), new(Color.grey, 1.0f) },
        new GradientAlphaKey[] { new(1.0f, 0f), new(0.3f, 1.0f) }
      );
      lr.colorGradient = gradient;
    }

    private void Raycast(Vector3 start, Vector3 dir, out float newRange) {
      newRange = range;

      var hit = Physics2D.Raycast(start, dir, range, hitColliderLayers);

      if (!hit) return;
      newRange = hit.distance;
      if (!hit.transform.TryGetComponent(out Receiver dmgRec)) return;

      dmgRec.TakeDamage(20f);
    }
  }
}
