using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utils;
using Random = UnityEngine.Random;

namespace Statistics {
  public class CardGenerator : MonoSingleton<CardGenerator> {
    private List<CardStatistics> generated;
    public UnityEvent OnCardBought;
    [SerializeField] private Transform cardContainer;

    private void Start() {
      GenerateCards(Random.value < 0.5f ? BuffType.Buff : BuffType.Debuff);
    }

    public void Show() {
      cardContainer.gameObject.SetActive(true);
    }

    public void Hide() {
      cardContainer.gameObject.SetActive(false);
    }

    public void GenerateCards(BuffType flavour) {
      generated = new List<CardStatistics>();

      // Get the child and draw stats
      for (int i = 0; i < cardContainer.transform.childCount; i++) {
        // Draw statistics for it
        var cardStatistics = ScriptableObject.CreateInstance<CardStatistics>();

        do {
          cardStatistics.Randomize(flavour);
        } while (generated.Exists(t =>
                   (t.enemyStatistic == cardStatistics.enemyStatistic && cardStatistics.entityType == EntityType.Enemy)
                   || (t.playerStatistic == cardStatistics.playerStatistic &&
                       cardStatistics.entityType == EntityType.Player
                   )));

        generated.Add(cardStatistics);

        // Get the card
        var cardObject = cardContainer.transform.GetChild(i);
        if(!cardObject.TryGetComponent(out CardSelectionHandler csh)) continue;
        
        csh.OnCardBought.AddListener(() => OnCardBought.Invoke());

        // Pass them onto realization
        csh.SetStats(cardStatistics);
      }
    }

#if UNITY_EDITOR
    [ContextMenu("Regenerate cards")]
    private void GenCards() {
      GenerateCards(BuffType.Buff);
    }
#endif
  }
}
