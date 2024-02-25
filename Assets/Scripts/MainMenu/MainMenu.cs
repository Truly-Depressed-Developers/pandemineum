using System.Threading.Tasks;
using FlowManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
  private void Awake() {
    SceneManager.LoadSceneAsync("Support", LoadSceneMode.Additive);
  }

  public void Callback_NewGame() {
    Flow.I.StartCoroutine(Flow.I.LoadIntro1());
  }
}
