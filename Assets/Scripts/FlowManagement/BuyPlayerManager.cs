using System;
using Statistics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlowManagement {
  public class BuyPlayerManager : MonoBehaviour, IUnloadable {
    [SerializeField] private CardGenerator cg;

    private void Start() {
      cg.OnCardBought.AddListener(OnCardBought);
    }

    public void BuyCards() {
      cg.Show();
      cg.GenerateCards(BuffType.Buff);
    }

    public void OnCardBought() {
      cg.Hide();
    }

    public void OnUnload() {
      cg.OnCardBought.RemoveListener(OnCardBought);
    }

    public void NextDay() {
      SceneManager.LoadSceneAsync("TheMine", LoadSceneMode.Additive);
      SceneManager.UnloadSceneAsync("BuyPlayer");
    }
  }
}
