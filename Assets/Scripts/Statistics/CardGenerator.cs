using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CardPrefabGenerator : MonoBehaviour {
  void Start() {
    this.GenerateCardPrefab();
  }
  public void GenerateCardPrefab() {
    // Get the child and draw stats
    for (int i = 0; i < this.transform.childCount; i++) {
      // Get the card
      var card = this.transform.GetChild(i);

      // Display each statistic
      for (int j = 0; j < card.childCount; j++) {
        // Draw statistics for it
        var cardStatisticsScriptableObjects = ScriptableObject.CreateInstance<CardStatistics>();
        
        var displayStatistics = card.transform.GetChild(j);
        var buff = cardStatisticsScriptableObjects.buffType;

        // Pass them onto realization
        var displayScript = displayStatistics.parent.GetComponent<CardSelectionHandler>();
        displayScript.statistics = cardStatisticsScriptableObjects;

        // Fetch the script with text fields to set their values
        CardStatisticDisplay card5 = displayStatistics.GetComponent<CardStatisticDisplay>();

        // Fill out text fields' values
        string sign = cardStatisticsScriptableObjects.value > 0 ? "+" : "";
        card5.value.text = sign + cardStatisticsScriptableObjects.value.ToString();

        card5.entityType.text = cardStatisticsScriptableObjects.entityType.ToString();
        if (cardStatisticsScriptableObjects.entityType == EntityType.Player) {
          card5.chosenStatistic.text = cardStatisticsScriptableObjects.playerStatistic.ToString();
        }

        card5.transform.parent.GetComponent<Image>().sprite = cardStatisticsScriptableObjects.sprite;

        card5.chosenStatistic.text = cardStatisticsScriptableObjects.enemyStatistic.ToString();
        if (buff == BuffType.Buff) {
          card5.value.color = Color.green;
          card5.entityType.color = Color.green;
          card5.chosenStatistic.color = Color.green;
        } else {
          card5.value.color = Color.red;
          card5.entityType.color = Color.red;
          card5.chosenStatistic.color = Color.red;
        }
      }
    }
  }
}
