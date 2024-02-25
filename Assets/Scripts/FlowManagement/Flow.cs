using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace FlowManagement {
  public class Flow : MonoSingleton<Flow> {
    public IEnumerator LoadIntro1() {
      yield return SceneFader.I.FadeAndLoadScene(SceneFader.FadeDirection.In);
      
      var mainMenu = SceneManager.UnloadSceneAsync("MainMenu");
      while (!mainMenu.isDone) yield return null;

      var intro = SceneManager.LoadSceneAsync("Intro", LoadSceneMode.Additive);
      while (!intro.isDone) yield return null;
      
      yield return SceneFader.I.FadeAndLoadScene(SceneFader.FadeDirection.Out);
    }

    public IEnumerator LoadTheMineFake() {
      yield return SceneFader.I.FadeAndLoadScene(SceneFader.FadeDirection.In);
      
      var intro = SceneManager.UnloadSceneAsync("Intro");
      while (!intro.isDone) yield return null;

      var theMineFake = SceneManager.LoadSceneAsync("TheMineFake", LoadSceneMode.Additive);
      while (!theMineFake.isDone) yield return null;

      yield return SceneFader.I.FadeAndLoadScene(SceneFader.FadeDirection.Out);
    }

    public IEnumerator LoadIntro2() {
      yield return SceneFader.I.FadeAndLoadScene(SceneFader.FadeDirection.In);
      
      var theMineFake = SceneManager.UnloadSceneAsync("TheMineFake");
      while (!theMineFake.isDone) yield return null;

      var intro2 = SceneManager.LoadSceneAsync("Intro2", LoadSceneMode.Additive);
      while (!intro2.isDone) yield return null;
      
      yield return SceneFader.I.FadeAndLoadScene(SceneFader.FadeDirection.Out);
    }

    public IEnumerator LoadBuyCEO() {
      yield return SceneFader.I.FadeAndLoadScene(SceneFader.FadeDirection.In);
      
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
      
      yield return SceneFader.I.FadeAndLoadScene(SceneFader.FadeDirection.Out);
    }

    public IEnumerator LoadBuyPlayer() {
      yield return SceneFader.I.FadeAndLoadScene(SceneFader.FadeDirection.In);
      
      var buyCEO = SceneManager.UnloadSceneAsync("BuyCEO");
      while (!buyCEO.isDone) yield return null;

      var buyPlayer = SceneManager.LoadSceneAsync("BuyPlayer", LoadSceneMode.Additive);
      while (!buyPlayer.isDone) yield return null;
      
      yield return SceneFader.I.FadeAndLoadScene(SceneFader.FadeDirection.Out);
    }

    public IEnumerator LoadTheMine() {
      yield return SceneFader.I.FadeAndLoadScene(SceneFader.FadeDirection.In);
      
      var buyPlayer = SceneManager.UnloadSceneAsync("BuyPlayer");
      while (!buyPlayer.isDone) yield return null;

      var theMine = SceneManager.LoadSceneAsync("TheMine", LoadSceneMode.Additive);
      while (!theMine.isDone) yield return null;
      
      yield return SceneFader.I.FadeAndLoadScene(SceneFader.FadeDirection.Out);
    }

    public IEnumerator LoadLose() {
      yield return SceneFader.I.FadeAndLoadScene(SceneFader.FadeDirection.In);
      
      var theMine = SceneManager.UnloadSceneAsync("TheMine");
      while (!theMine.isDone) yield return null;

      var lose = SceneManager.LoadSceneAsync("Lose", LoadSceneMode.Additive);
      while (!lose.isDone) yield return null;
      
      yield return SceneFader.I.FadeAndLoadScene(SceneFader.FadeDirection.Out);
    }
  }
}
