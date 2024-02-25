using System.Collections;
using Statistics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlowManagement {
  public class BuyCEOManager : MonoBehaviour {
    [SerializeField] private CardGenerator cg;
    
    private void Start() {
      cg.Show();
    }
    
    public void Next() {
      Flow.I.StartCoroutine(Flow.I.LoadBuyPlayer());
    }
  }
}
