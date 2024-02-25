using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

  public void Callback_NewGame() {
    SceneManager.LoadScene(1);
  }

}
