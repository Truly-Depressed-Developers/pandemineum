using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace FlowManagement {
  public class Intro1Manager : MonoBehaviour {
    [SerializeField] private PlayableDirector pd;

    private void Start() {
      pd.stopped += _ => {
        Flow.I.StartCoroutine(Flow.I.LoadTheMineFake());
      };
    }
  }
}
