using FlowManagement;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
  public void onPlayerDeath() {
    Flow.I.StartCoroutine(Flow.I.LoadLose());
  }

  public void OnPlayerDeathFake() {
    Flow.I.StartCoroutine(Flow.I.LoadIntro2());
  }
}
