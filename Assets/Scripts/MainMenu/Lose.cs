using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlowManagement;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
  public void Callback_NewGame() {
    Flow.I.StartCoroutine(Flow.I.LoadTheMine());
  }

  public void Callback_BackToMenu() {
    Flow.I.StartCoroutine(Flow.I.LoadMenu());
  }
}
