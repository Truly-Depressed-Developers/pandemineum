using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace FlowManagement {
  public class Flow : MonoSingleton<Flow> {
    public IEnumerator LoadIntro1() {
      var mainMenu = SceneManager.UnloadSceneAsync("MainMenu");
      while (!mainMenu.isDone) yield return null;

      var intro = SceneManager.LoadSceneAsync("Intro", LoadSceneMode.Additive);
      while (!intro.isDone) yield return null;
    }

    public IEnumerator LoadTheMineFake() {
      var intro = SceneManager.UnloadSceneAsync("Intro");
      while (!intro.isDone) yield return null;

      var theMineFake = SceneManager.LoadSceneAsync("TheMineFake", LoadSceneMode.Additive);
      while (!theMineFake.isDone) yield return null;
    }

    public IEnumerator LoadIntro2() {
      var theMineFake = SceneManager.UnloadSceneAsync("TheMineFake");
      while (!theMineFake.isDone) yield return null;

      var intro2 = SceneManager.LoadSceneAsync("Intro2", LoadSceneMode.Additive);
      while (!intro2.isDone) yield return null;
    }

    public IEnumerator LoadBuyCEO() {
      AsyncOperation intro2 = null;
      AsyncOperation theMine = null;

      try {
        intro2 = SceneManager.UnloadSceneAsync("Intro2");
      } catch { }

      if (intro2 != null) {
        while (!intro2.isDone) yield return null;
      }
      
      try {
        theMine = SceneManager.UnloadSceneAsync("TheMine");
      } catch { }

      if (theMine != null) {
        while (!theMine.isDone) yield return null;
      }

      var buyCEO = SceneManager.LoadSceneAsync("BuyCEO", LoadSceneMode.Additive);
      while (!buyCEO.isDone) yield return null;
    }

    public IEnumerator LoadBuyPlayer() {
      var buyCEO = SceneManager.UnloadSceneAsync("BuyCEO");
      while (!buyCEO.isDone) yield return null;

      var buyPlayer = SceneManager.LoadSceneAsync("BuyPlayer", LoadSceneMode.Additive);
      while (!buyPlayer.isDone) yield return null;
    }

    public IEnumerator LoadTheMine() {
      var buyPlayer = SceneManager.UnloadSceneAsync("BuyPlayer");
      while (!buyPlayer.isDone) yield return null;

      var theMine = SceneManager.LoadSceneAsync("TheMine", LoadSceneMode.Additive);
      while (!theMine.isDone) yield return null;
    }

    public IEnumerator LoadLose() {
      var theMine = SceneManager.UnloadSceneAsync("TheMine");
      while (!theMine.isDone) yield return null;

      var lose = SceneManager.LoadSceneAsync("Lose", LoadSceneMode.Additive);
      while (!lose.isDone) yield return null;
    }

    public IEnumerator LoadMenu() {
      var loseScene = SceneManager.UnloadSceneAsync("Lose");
      while (!loseScene.isDone) yield return null;

      var menu = SceneManager.LoadSceneAsync("MainMenu");
      while (!menu.isDone) yield return null;
    }
  }
}
