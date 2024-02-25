using System;
using System.Collections;
using System.Collections.Generic;
using DamageSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Player {
  public class Shotgun : MonoBehaviour {
    [SerializeField] private Transform tip;
    [SerializeField] private GameObject lineRendererInstance;

    [SerializeField] private float spread;
    [SerializeField] private int pellets;
    [SerializeField] public float range;
    [SerializeField] public float damage = 20f;
    [SerializeField] private int clipSize;

    [SerializeField] private float shotCooldown;
    [SerializeField] public float reloadTime;

    [SerializeField] private LayerMask hitColliderLayers;
    [SerializeField] private AudioClip shotgun_sound;
    [SerializeField] private AudioSource audio_src;

    private CursorManager cursorManager;
    private PlayerCameraEffects playerCamEff;

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

      GetScriptReferences();

      if (!cursorManager) {
        Debug.LogWarning("CursorManager not found");
      }

      GameObject crosshair = GameObject.Find("Crosshair");
      
      if (crosshair) crosshair.TryGetComponent(out cursorManager);
      if (cursorManager) cursorManager.setShellsCount(clipSize);

      if (!playerCamEff)
        Debug.LogWarning("Player Camera Effects component was not found...");
    }

    public void Fire() {
      if (!CanShoot()) return;

      Vector3 shotgunDir = transform.right;

      for (int i = 0; i < pellets; i++) {
        float randomAngle = Random.Range(-spread, spread);
        var randomDir = (Quaternion.AngleAxis(randomAngle, transform.forward) * shotgunDir).normalized * Mathf.Sign(transform.parent.transform.localScale.x);
        var pelletPoint = tip.position + randomDir * range ;

        Vector3[] arr = { tip.position, pelletPoint };
        var lri = Instantiate(lineRendererInstance);

        Raycast(tip.position, randomDir, out float newRange);

        if (newRange < range) {
          arr[1] = tip.position + randomDir * newRange;
        }

        SetLrProperties(lri, arr);
      }

      // lr.SetPositions(pelletPoints);
      if (cursorManager) cursorManager.onShoot(--ammo);

      // apply slight screenshake
      if (playerCamEff) playerCamEff.ShakeCamera();

      if (ammo == 0) {
        lastReload = Time.time;
        StartCoroutine(DoReload());
      } else {
        if (cursorManager) cursorManager.startBarAnimation(shotCooldown);
      }

      lastShot = Time.time;
      audio_src.PlayOneShot(shotgun_sound);
    }

    private IEnumerator DoReload() {
      if (cursorManager) cursorManager.startBarAnimation(reloadTime);
      yield return new WaitForSeconds(reloadTime);
      ammo = clipSize;
      if (cursorManager) cursorManager.onReload();
    }

    private bool CanShoot() {
      return ammo > 0 && Mathf.Abs(ReloadProgress - 1f) < Mathf.Epsilon &&
             Mathf.Abs(ShotCooldownProgress - 1f) < Mathf.Epsilon;
    }

    private void SetLrProperties(GameObject lri, Vector3[] points) {
      lri.TryGetComponent(out LineRendererFade lrf);
      lri.TryGetComponent(out LineRenderer lr);

      if (!lrf || !lr) return;

      lrf.points = points;
      lrf.fadeOutTime = 0.3f;

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

      dmgRec.TakeDamage(damage);
    }

    private void GetScriptReferences() {
      GameObject find;
      find = GameObject.Find("Crosshair");
      if (find) find.TryGetComponent(out cursorManager); // UI ammo display
      find = GameObject.Find("Player Camera Follow");
      if (find) find.TryGetComponent(out playerCamEff); // Player camera effects
    }

    public void OnIndicateReload(InputAction.CallbackContext ctx) {
      ammo = 0;
      cursorManager.onShoot(ammo);
      lastReload = Time.time;
      StartCoroutine(DoReload());
    }
  }
}
