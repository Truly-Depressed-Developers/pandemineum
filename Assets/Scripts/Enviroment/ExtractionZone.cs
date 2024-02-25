using System.Collections;
using System.Collections.Generic;
using FlowManagement;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ExtractionZone : MonoBehaviour {
  [SerializeField] private TMP_Text accessTxt;
  [SerializeField] private TMP_Text noAccessTxt;

  private bool objectiveCompleted = false;
  private bool hasEscaped = false;

  public void changeObjectiveStatus(bool status) {
    objectiveCompleted = status;
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.CompareTag("Player")) {
      if(objectiveCompleted) {
        accessTxt.gameObject.SetActive(true);
      } else {
        noAccessTxt.gameObject.SetActive(true);
      }
    }
  }

  private void OnTriggerExit2D(Collider2D collision) {
    if (collision.CompareTag("Player")) {
      accessTxt.gameObject.SetActive(false);
      noAccessTxt.gameObject.SetActive(false);
    }
  }

  public void onIndicateExit(InputAction.CallbackContext ctx) {
    if (objectiveCompleted && !hasEscaped) {
      hasEscaped = true;
      Flow.I.StartCoroutine(Flow.I.LoadBuyCEO());
    }
  }
}
