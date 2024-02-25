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
      SceneManager.LoadSceneAsync("BuyPlayer", LoadSceneMode.Additive);
      SceneManager.UnloadSceneAsync("BuyCEO");
    }
  }
}
