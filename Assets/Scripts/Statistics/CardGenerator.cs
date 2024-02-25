using System.Collections.Generic;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Statistics {
  public class CardGenerator : MonoSingleton<CardGenerator> {
    public List<CardStatistics> tmp;
    
    private void Start() {
      tmp = new List<CardStatistics>();
      
      GenerateCards(Random.value < 0.5f ? BuffType.Buff : BuffType.Debuff);
    }

    public void GenerateCards(BuffType flavour) {
      // Get the child and draw stats
      for (int i = 0; i < transform.childCount; i++) {
        
        // Draw statistics for it
        var cardStatistics = ScriptableObject.CreateInstance<CardStatistics>();
        cardStatistics.Randomize(flavour);
        
        tmp.Add(cardStatistics);
        
        // Get the card
        var cardObject = transform.GetChild(i);
        var csh = cardObject.GetComponent<CardSelectionHandler>();

        // Pass them onto realization
        csh.SetStats(cardStatistics);
      }
    }
  }
}
