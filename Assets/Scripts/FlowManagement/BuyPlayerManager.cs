using System;
using Statistics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlowManagement {
  public class BuyPlayerManager : MonoBehaviour {
    int cobaltHeld = 374;
    [SerializeField] private CardGenerator cg;
    [SerializeField] private TMPro.TextMeshProUGUI buy_card_text;

    private void Start() {
      cg.OnCardBought.AddListener(OnCardBought);
    }

    private void Update() {
      buy_card_text.text = $"Buy cards ({cobaltHeld}/300 cobalt)";
    }

    public void BuyCards() {
      if (cobaltHeld < 300) return;

     cobaltHeld -= 300;

      cg.Show();
      cg.GenerateCards(BuffType.Buff);
    }

    public void OnCardBought() {
      cg.Hide();
    }

    public void NextDay() {
      if (cobaltHeld >= 300) return;

      cg.OnCardBought.RemoveListener(OnCardBought);
      Flow.I.StartCoroutine(Flow.I.LoadTheMine());
    }
  }
}
