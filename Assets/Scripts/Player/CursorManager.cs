using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Utils;

public class CursorManager : MonoBehaviour {
  [SerializeField] private float smoothSpeed = 5f;

  [SerializeField] private GameObject shellPrefab;
  [SerializeField] private Transform shellSpawnPoint;
  [SerializeField] private Slider reloadBar;

  [Header("Shotgun audio")]
  [Space(5)]
  [SerializeField] private AudioSource audio_src;
  [SerializeField] private AudioClip shell_load_seff;
  [SerializeField] private AudioClip shotgun_reload_seff;

  private Vector3 targetPosition;

  private Camera mainCamera;

  private int shellsCount;
  private int currentShellsCount;

  void Start() {
    UnityEngine.Cursor.visible = false;

    mainCamera = Camera.main;
  }

  private void UpdateTargetPosition() {
    if (mainCamera == null) return;

    Vector2 mousePos = new Vector2(MouseLocation.I.Position.x, MouseLocation.I.Position.y);

    targetPosition = mousePos;
  }

  private void Update() {
    UpdateTargetPosition();

    transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
  }

  public void setShellsCount(int count) {
    shellsCount = count;
    currentShellsCount = count;
    displayShells();
  }

  public void displayShells() {
    if (shellSpawnPoint == null) return;

    for (int i = 0; i < shellsCount; i++) {
      GameObject newShell = Instantiate(shellPrefab, shellSpawnPoint);
    }

    LayoutRebuilder.ForceRebuildLayoutImmediate(shellSpawnPoint.GetComponent<RectTransform>());
  }

  public void updateActiveShells() {

    if(currentShellsCount == shellsCount) {
      for(int i = 0; i < shellsCount; i++) {
        GameObject child = shellSpawnPoint.GetChild(i).gameObject;
        UnityEngine.UI.Image imageComponent = child.GetComponent<UnityEngine.UI.Image>();
        if (imageComponent != null) {
          Color color = imageComponent.color;
          color.a = 1f; 
          imageComponent.color = color;
        }
      }
      return;
    }

    for(int i = currentShellsCount; i < shellsCount; i++) {

      GameObject child = shellSpawnPoint.GetChild(i).gameObject;
      UnityEngine.UI.Image imageComponent = child.GetComponent<UnityEngine.UI.Image>();
      if (imageComponent != null) {
        Color color = imageComponent.color;
        color.a = 0.2f; 
        imageComponent.color = color;
      }

    }

  }

  public void onShoot(int currentAmmo) {
    currentShellsCount = currentAmmo;
    updateActiveShells();
  }

  public void onReload() {
    currentShellsCount = shellsCount;
    updateActiveShells();
  }

  public void startBarAnimation(float time) {
    StartCoroutine(AnimateBar(time));
  }

  private IEnumerator AnimateBar(float animTime) {
    float elapsedTime = 0f;
    float startValue = 0f;
    float endValue = 1f;

    bool play_sound = true;
    int shell_sound = shellsCount;
    float shell_sound_treshold = animTime / shell_sound;
    float shell_time = shell_sound_treshold;
    if (currentShellsCount != 0)
      play_sound = false;

    while (elapsedTime < animTime) {
      float progress = Mathf.Clamp01(elapsedTime / animTime);

      float animatedValue = Mathf.Lerp(startValue, endValue, progress);

      reloadBar.value = animatedValue;

      elapsedTime += Time.deltaTime;

      // reload sounds
      if (play_sound) {
        shell_time -= Time.deltaTime;
        if (shell_sound > 0) {
          if (shell_time <= 0) {
            shell_sound--;
            shell_time = shell_sound_treshold;
            audio_src.PlayOneShot(shell_load_seff);
          }
        }
      }

      yield return null;
    }

    if(play_sound)
      audio_src.PlayOneShot(shotgun_reload_seff);
    reloadBar.value = endValue;
  }




}
