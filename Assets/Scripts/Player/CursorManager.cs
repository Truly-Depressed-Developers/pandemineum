using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour {
  [SerializeField] private float smoothSpeed = 5f;

  [SerializeField] private GameObject shellPrefab;
  [SerializeField] private Transform shellSpawnPoint;
  [SerializeField] private Slider reloadBar;

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

    Vector2 mousePosition = Mouse.current.position.ReadValue();
    mousePosition.y = Screen.height - mousePosition.y;
    Vector3 mouseScreenPosition = new Vector3(mousePosition.x, mousePosition.y, mainCamera.nearClipPlane);
    targetPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);
    targetPosition.z = 0;
    targetPosition.y = -targetPosition.y;
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
    Debug.Log("Reloading");
    updateActiveShells();
  }

  public void startBarAnimation(float time) {
    Debug.Log("Starting bar animation");
    StartCoroutine(AnimateBar(time));
  }

  private IEnumerator AnimateBar(float animTime) {
    float elapsedTime = 0f;
    float startValue = 0f;
    float endValue = 1f;

    while (elapsedTime < animTime) {
      float progress = Mathf.Clamp01(elapsedTime / animTime);

      float animatedValue = Mathf.Lerp(startValue, endValue, progress);

      reloadBar.value = animatedValue;

      elapsedTime += Time.deltaTime;

      Debug.Log("Elapsed time: " + elapsedTime);

      yield return null;
    }

    reloadBar.value = endValue;
  }




}
