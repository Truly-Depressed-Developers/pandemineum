using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CardPrefabGenerator : MonoBehaviour
{
    void Start()
    {
        this.GenerateCardPrefab();
    }
    public void GenerateCardPrefab()
    {
        // Get the child and draw stats
        Debug.Log(this.transform.childCount);
        for (int i = 0; i < this.transform.childCount; i++)
        {
            // Get the card
            var card = this.transform.GetChild(i);
            Debug.Log(card);
            Debug.Log(card.childCount);
            
            // Display each statistic
            for (int j = 0; j < card.childCount; j++)
            {
                // Draw statistics for it
                var cardStatisticsScriptableObjects = ScriptableObject.CreateInstance<CardStatistics>();
                var displayStatistics = card.transform.GetChild(j);

                // Fetch the script with text fields to set their values
                card5 card5 = displayStatistics.GetComponent<card5>();

                // Fill out text fields' values
                card5.chosenStatistic.text = cardStatisticsScriptableObjects.chosenStatistic.ToString();
                card5.entityType.text = cardStatisticsScriptableObjects.entityType.ToString();
                if (cardStatisticsScriptableObjects.buffType == BuffType.Buff)
                {
                    card5.value.text = "+" + cardStatisticsScriptableObjects.value.ToString();
                } 
                else
                {
                    card5.value.text = "-" + cardStatisticsScriptableObjects.value.ToString();
                }
            }
        }
    }
}
