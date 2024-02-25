using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
  private void Awake() {
    SceneManager.LoadSceneAsync("Support", LoadSceneMode.Additive);
  }
  
  public void Callback_NewGame() {
    // SceneManager.LoadSceneAsync("");
  }
}
