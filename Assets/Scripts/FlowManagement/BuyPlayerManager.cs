using System;
using Statistics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlowManagement {
  public class BuyPlayerManager : MonoBehaviour {
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

    public void NextDay() {
      cg.OnCardBought.RemoveListener(OnCardBought);
      Flow.I.StartCoroutine(Flow.I.LoadTheMine());
    }
  }
}
